using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
 	public class FighterBullet : MonoBehaviour
	{
        public Vector3 orient;
        public FlockControl flock;

        [Header("UnityParameter")]
        public int maxLifeCount;
        public int presentLifeCounts;
        public float createOffsetDistance;
        public float lifeTime;
        public float moveSpeed;
        WaitForSeconds wait_Die;

        private void Awake()
        {
            wait_Die = new WaitForSeconds(lifeTime);
        }
        public void Initialize(Vector3 pos, Vector3 orient, string tag, FlockControl flock)
        {
            transform.position = pos + createOffsetDistance * orient;
            this.orient = orient;
            transform.up = orient;
            this.tag= tag;
            this.flock = flock;
            presentLifeCounts = maxLifeCount;

            gameObject.SetActive(true);
            StartCoroutine("Moving");
            StartCoroutine("WaitDying");
        }
        IEnumerator Moving()
        {
            while (true)
            {
                transform.position += Time.deltaTime * moveSpeed * orient;
                yield return null;
            }
        }
        IEnumerator WaitDying()
        {
            yield return wait_Die;
            if (gameObject.activeSelf)
                PoolManager.Instance().Recycle(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(tag) || !collision.gameObject.activeSelf) 
            {
                return;
            }

            if (presentLifeCounts > 0)
            {
                collision.GetComponent<ICanBeHit>().OnBeHit();
                flock.GenerateFighter(flock.massCenter);

                presentLifeCounts--;
            }
                
            if (presentLifeCounts == 0 && gameObject.activeSelf) 
            {
                PoolManager.Instance().Recycle(gameObject);
            }
        }

    }
}
