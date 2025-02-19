using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using BF.Utility;
using UnityEngine;

namespace Internet
{
	public class PlayerControl : NetworkBehaviour
	{
        public NetworkVariable<int> onBeHit;
        Rigidbody2D rb;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            render = GetComponent<SpriteRenderer>();

            onBeHit.OnValueChanged += Twinkle;
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
        }
        private void Start()
        {
            if (this.IsOwner)
            {
                CursorManager.Instance().onMove += Move;
                CursorManager.Instance().onShoot += ShootServerRpc;

                GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            CursorManager.Instance().onMove -= Move;
            CursorManager.Instance().onShoot -= ShootServerRpc;
        }

        [Header("Shoot")]
        #region shoot
        public float shootInterval;
        [SerializeField] float lastShootTime;
        public GameObject bulletPrefab;

        [ServerRpc]
        public void ShootServerRpc()
        {
            if (Time.time - lastShootTime > shootInterval)
            {
                lastShootTime = Time.time;

                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.FromToRotation(Vector3.up, transform.up),transform);
                bullet.GetComponent<BulletControl>().Initialize(transform);
                bullet.GetComponent<NetworkObject>().Spawn();
            }
        }
        #endregion
        [Header("Move")]
        #region Move
        public float accelerate;
        public float maxMoveSpeed;
        public float maxRotateSpeed;
        public float rotateAccelerate;
        public float decelerateWhenRotate;
        public float decelerateWhenStop;

        public Vector3 orient;

        public Vector3 aimPos;
        public bool isCompleted;
        public float presentSpeed;
        public float presentRotateSpeed;

        protected void Update()
        {
            Rotate();
            if (!isCompleted)
            {            
                if ((aimPos - transform.position).sqrMagnitude < presentSpeed * presentSpeed * 0.5f / decelerateWhenStop) 
                {
                    Stop();
                }
                else
                {
                    Run();
                }
            }
            transform.position += presentSpeed * orient * Time.deltaTime;
            transform.up = orient;
        }
        public void Move(Vector3 aimPos)
        {
            this.aimPos = aimPos;
            isCompleted = false;
            StopCoroutine("Stopping");
        }
        protected void Run()
        {
            presentSpeed = Mathf.Min(maxMoveSpeed, presentSpeed + Time.deltaTime * accelerate);
        }
        protected void Rotate()
        {
            var aimOrient = (aimPos - transform.position).normalized;
            var angle = Mathf.Acos(Vector3.Dot(orient, aimOrient));
            if (angle * Mathf.Rad2Deg < Time.deltaTime * presentRotateSpeed)
            {
                orient = aimOrient;
                presentRotateSpeed = 0;
            }
            else
            {
                bool direction = ((orient.y * aimOrient.x - orient.x * aimOrient.y) < 0);
                presentRotateSpeed = Mathf.Min(maxRotateSpeed, presentRotateSpeed + Time.deltaTime * rotateAccelerate);
                orient = orient.RotateVertical(Time.deltaTime * presentRotateSpeed* (direction ? 1 : -1)).normalized;
            }
        }
        protected void Stop()
        {
            isCompleted = true;
            StartCoroutine("Stopping");
        }
        IEnumerator Stopping()
        {
            while (presentSpeed > 0)
            {
                yield return null;
                presentSpeed = Mathf.Max(0, presentSpeed - Time.deltaTime * decelerateWhenStop);
            }
        }
        #endregion
        [Header("BeHit")]
        #region BeHit
        [SerializeField] float twinkleDuration;
        [SerializeField] Color twinkleColor;
        [SerializeField] int twinkleTimes;
        [SerializeField] bool isTwinkling;
        SpriteRenderer render;

        public void BeHit(ulong id)
        {
            onBeHit.Value++;

            /*
            if (!isTwinkling)
            {
                StartCoroutine("Twinkling");
                ClientRpcParams p = new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new List<ulong> { id }
                    }
                };
                BeHitClientRpc(p);
            }*/

        }
        public void Twinkle(int a,int b)
        {
            if (!isTwinkling)
            {
                StartCoroutine("Twinkling");
            }
        }
        IEnumerator Twinkling()
        {
            isTwinkling = true;
            Color c = render.color;
            for (int i = 0; i < twinkleTimes; i++)
            {
                render.color = (i % 2 == 0 ? twinkleColor : c);

                yield return new WaitForSeconds(twinkleDuration);
            }
            isTwinkling = false;
        }
        #endregion
    }
}