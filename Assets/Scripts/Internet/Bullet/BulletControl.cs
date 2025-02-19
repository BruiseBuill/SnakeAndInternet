using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Internet
{
	public class BulletControl : NetworkBehaviour
	{
        public float moveSpeed;
        public Vector3 orient;
        public float lifeTime;
        public Transform parent;

        [SerializeField] NetworkVariable<Vector3> netPos = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        [SerializeField] NetworkVariable<Quaternion> netRot = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        WaitForSeconds wait_Die;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (this.IsServer)
            {
                wait_Die = new WaitForSeconds(lifeTime);
                StartCoroutine("WaitDie");
            }
        }
        public void Initialize(Transform parent)
        {
            this.parent = parent;
        }
        private void Start()
        {
            orient = transform.up;
        }
        private void Update()
        {
            if (this.IsServer)
            {
                transform.position += orient * Time.deltaTime * moveSpeed;
            }
        }
        IEnumerator WaitDie()
        {
            yield return wait_Die;
            CloseServerRpc();
        }
        [ServerRpc]
        public void CloseServerRpc()
        {
            GetComponent<NetworkObject>().Despawn();
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (parent && collision.transform != parent)
            {
                CloseServerRpc();
                if (collision.GetComponent<PlayerControl>())
                {
                    collision.GetComponent<PlayerControl>().BeHit(collision.GetComponent<NetworkObject>().OwnerClientId);
                }
            }
            
        }
    }
}