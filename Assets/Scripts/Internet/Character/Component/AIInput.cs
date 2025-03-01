using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
 	public class AIInput : MonoBehaviour
	{
        FlockControl flock;

        [SerializeField] Vector2 aiThinkBreak;

        [Header("Vision")]
        [SerializeField] float visionRadius;
        [SerializeField] float visionRadiusGrowth;

        [Header("Attack")]
        [SerializeField] bool canAttack;
        [SerializeField] Vector2 attackBreak;

        [Header("Patrol")]
        [SerializeField] Vector3 patrolPoint;
        [SerializeField] float arriveRadius;
        [SerializeField] float arriveRadiusGrowth;

        [SerializeField] LayerMask enemylayer;

        private void Awake()
        {
            flock = GetComponent<FlockControl>();
        }
        private void OnEnable()
        {
            if (flock.isPlayer)
            {
                this.enabled = false;
                return;
            }

            patrolPoint = flock.massCenter;

            StartCoroutine("Thinking");
            enemylayer = LayerManager.Instance().GetEnemyLayer(gameObject.layer);
        }
        IEnumerator Thinking()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(aiThinkBreak.x, aiThinkBreak.y));
                Patrol();
                Attack();
                Check();
            }            
        }
        void Patrol()
        {
            float sqrR = (arriveRadius + flock.flockList.Count * arriveRadiusGrowth) * (arriveRadius + flock.flockList.Count * arriveRadiusGrowth);
            if ((flock.massCenter - patrolPoint).sqrMagnitude < sqrR) 
            {
                patrolPoint = MapManager.Instance().GetRandomPos();
            }
            else
            {
                flock.Move((patrolPoint - flock.massCenter).normalized);
            }
        }
        void Check()
        {
            var ori = flock.orient;
            var length = visionRadius + flock.flockList.Count * visionRadiusGrowth;
            var col = Physics2D.OverlapBox(flock.massCenter + ori * length * 0.5f, new Vector2(length, flock.flockList.Count * visionRadiusGrowth), Mathf.Atan(ori.y / ori.x) * Mathf.Rad2Deg, enemylayer);
            if (col)
            {
                flock.Shoot();
            }
        }
        void Attack()
        {
            if (!canAttack)
            {
                return;
            }
            var col = Physics2D.OverlapCircle(flock.massCenter, visionRadius + flock.flockList.Count * visionRadiusGrowth, enemylayer);
            if (col)
            {
                flock.Move((col.transform.position - flock.massCenter).normalized);
                canAttack = false;
                StartCoroutine("WaitingAttack");
            }
        }
        IEnumerator WaitingAttack()
        {
            yield return new WaitForSeconds(Random.Range(attackBreak.x, attackBreak.y));
            canAttack = true;
        }
    }
}
