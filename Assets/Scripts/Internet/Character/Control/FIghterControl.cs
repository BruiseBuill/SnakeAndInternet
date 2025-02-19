using BF;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using static UnityEditor.PlayerSettings;

namespace Internet
{
	public class FighterControl : MonoBehaviour , ICanBeHit
    {
		// 
		//faceOrient: transform.up
		public Vector3 moveOrient;

		[SerializeField] GameObject bulletPrefab;
		public FlockControl flock;

		SpriteRenderer spriteRenderer;

        public bool isDebug;
		public Vector3 separateForce;
		public Color separateColor;
		public Vector3 alignForce;
		public Color alignForceColor;
		public Vector3 cohesionForce;
		public Color cohesionColor;
		public Vector3 leadForce;
		public Color leadColor;
		public Vector3 totalForce;
		public Color totalColor;

		float randomGeneratorOffset = 0.3f;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void Initialize(Vector3 pos,Vector3 orient,FlockControl flock,string tag,LayerMask layer,Sprite skin)
		{
			transform.position = pos + (Vector3)UnityEngine.Random.insideUnitCircle * randomGeneratorOffset;
			transform.up = orient;
			this.flock = flock;
			gameObject.tag = tag;
			gameObject.layer = layer;
            SetSkin(skin);

            gameObject.SetActive(true);
        }
        public void Move(Vector3 orient)
		{
            transform.up = orient;
		}
		public void Shoot()
		{
			var b = PoolManager.Instance().Release(bulletPrefab.name).GetComponent<FighterBullet>();
			b.Initialize(transform.position, transform.up, tag, flock);

		}
		public void OnBeHit()
		{
			flock.RecycleFighter(this);
		}
		public void SetSkin(Sprite sprite)
		{
			spriteRenderer.sprite = sprite;
		}
		public void Test(Vector3 separateForce, Vector3 cohesionForce, Vector3 alignForce, Vector3 leadForce, Vector3 totalForce)
		{
			this.separateForce = separateForce;
			this.alignForce = alignForce;
			this.cohesionForce = cohesionForce;
			this.leadForce=leadForce;
			this.totalForce = totalForce;
		}
        private void OnDrawGizmos()
        {
			if (isDebug)
			{
				Gizmos.color = separateColor;
				Gizmos.DrawLine(transform.position, transform.position + separateForce);

				Gizmos.color = alignForceColor;
                Gizmos.DrawLine(transform.position, transform.position + alignForce);

				Gizmos.color = cohesionColor;
                Gizmos.DrawLine(transform.position, transform.position + cohesionForce);

				Gizmos.color = leadColor;
                Gizmos.DrawLine(transform.position, transform.position +  leadForce);

				Gizmos.color = totalColor;
				Gizmos.DrawLine(transform.position, transform.position + totalForce);
            }
        }
    }
}