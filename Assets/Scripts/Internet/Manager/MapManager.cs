using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Internet
{
	public class MapManager : Single<MapManager>
	{
		public Vector3 mapSize;

		public bool IsInMap()
		{
			return false;	
		}
		public Vector3 GetRandomPos()
		{
			return new Vector3(Random.Range(0f, mapSize.x), Random.Range(0f, mapSize.y), 0);
		}
	}
}