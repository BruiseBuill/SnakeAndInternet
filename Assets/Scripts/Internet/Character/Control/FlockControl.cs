using BF;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
    public class FlockControl : MonoBehaviour
	{
        [SerializeField] EventChannel onAnyUnitChange;
        [SerializeField] GenericEventChannel<object> onFlockGenerate;
        public Action onUnitCountChange = delegate { };

        public GameObject fighterPrefab;
        public bool isAlive;
        [Header("League")]
        public Sprite skin;
        public bool isPlayer;

        [Header("Move")]
        public Vector3 orient;

        [Header("Shoot")]
        public bool canShoot = true;
        public float shootBreak;

        [Header("UniverseParameter")]
        public List<FighterControl> flockList; // 鸟群的成员
        public int maxUnitCount;
        public Vector3 massCenter;
        public float detectRadius = 5.0f; // 邻居检测半径
        public float maxSpeed = 5.0f; // 最大速度
        public float maxForce = 4f; // 最大转向力

        public float separationWeight = 1.5f;
        public float alignmentWeight = 1.0f;
        public float cohesionWeight = 1.0f;
        public float leadingWeight = 1f;

        public int initialUnitCount;


        public void Initialize(Vector3 pos,LayerMask layer, string tag, Sprite skin,bool isPlayer)
        {
            gameObject.layer = layer;
            this.tag = tag;
            this.skin = skin;
            this.isPlayer= isPlayer;
            isAlive = true;
            massCenter = pos;

            for (int i = 0; i < initialUnitCount; i++) 
            {
                GenerateFighter(pos);
            }
            
            gameObject.SetActive(true);
            onFlockGenerate.Invoke(this);
        }
        public void Move(Vector3 orient)
        {
            if (!isAlive)
                return;

            this.orient = orient;

            for(int i = 0; i < flockList.Count; i++)
            {
                flockList[i].Move(orient);
            }
        }
        public void Shoot()
        {
            if (!isAlive)
                return;

            if (canShoot)
            {
                StartCoroutine("Shooting");
            }
        }
        IEnumerator Shooting()
        {
            canShoot = false;
            for (int i = 0; i < flockList.Count; i++)
            {
                flockList[i].Shoot();
            }
            yield return new WaitForSeconds(shootBreak);
            canShoot = true;
        }
        public void GenerateFighter(Vector3 pos)
        {
            if (!isAlive || flockList.Count == maxUnitCount) 
            {
                return;
            }

            var fighter = PoolManager.Instance().Release(fighterPrefab.name).GetComponent<FighterControl>();
            fighter.Initialize(pos, orient, this, tag, gameObject.layer, skin); 
            flockList.Add(fighter);

            onUnitCountChange.Invoke();
            onAnyUnitChange.Invoke();
        }
        public void RecycleFighter(FighterControl fighter)
        {
            flockList.Remove(fighter);
            PoolManager.Instance().Recycle(fighter.gameObject);

            if (flockList.Count == 0)
            {
                isAlive = false;
            }

            onUnitCountChange.Invoke();
            onAnyUnitChange.Invoke();
        }
        private void Update()
        {
            if (flockList.Count == 0)
                return;

            massCenter = Vector3.zero;
            foreach (var bird in flockList)
            {
                massCenter += bird.transform.position;  
            }
            massCenter /= flockList.Count;
            transform.position = massCenter;
            
            for (int i = 0; i < flockList.Count; i++)
            {
                Vector3 cohesion = Vector3.zero;
                Vector3 separate = Vector3.zero;
                Vector3 align = Vector3.zero;
                Vector3 lead = leadingWeight * orient;
                cohesion += cohesionWeight * (massCenter - flockList[i].transform.position);
                if (cohesion.sqrMagnitude > maxForce * maxForce)
                    cohesion = cohesion.normalized * maxForce;

                var res = Physics2D.OverlapCircleAll(flockList[i].transform.position, detectRadius, 1 << gameObject.layer);
                for (int j = 0; j < res.Length; j++) 
                {
                    if (res[j].gameObject != flockList[i].gameObject && res[j].CompareTag(flockList[i].tag))
                    {
                        separate += separationWeight * (flockList[i].transform.position - res[j].transform.position) / (flockList[i].transform.position - res[j].transform.position).sqrMagnitude;
                        align += alignmentWeight * res[j].GetComponent<FighterControl>().moveOrient;
                    }
                }
                if (separate.sqrMagnitude > maxForce * maxForce)
                    separate = separate.normalized * maxForce;
                if (align.sqrMagnitude > maxForce * maxForce)
                    align = align.normalized * maxForce;
                Vector3 total = lead + separate + cohesion + align;
                flockList[i].moveOrient = total.normalized;
                flockList[i].transform.position += flockList[i].moveOrient * Mathf.Min(total.magnitude, maxSpeed) * Time.deltaTime;
                flockList[i].Test(separate, cohesion, align, lead, total);
            }
        }
    }
}