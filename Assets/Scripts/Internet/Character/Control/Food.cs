using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
	public class Food : MonoBehaviour,ICanBeHit
	{
		public void Initialize(Vector3 pos)
		{
			transform.position = pos;
			gameObject.SetActive(true);
		}
		public void OnBeHit()
		{
			PoolManager.Instance().Recycle(gameObject);
		}
	}
}