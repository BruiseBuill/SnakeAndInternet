using BF;
using Snake.Other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
 	public class MapManager : Single<MapManager>
	{
        public Vector2Int mapSize;
        public Vector2Int grassSizeRange;
        public Vector2Int grassCountRange;
        public Vector2 gridSize;
        public Vector3 startPos;
        public GameObject grassPrefab;

        public bool[,] grassMap; 

        public void CreateMap()
        {
            int grassCount = Random.Range(grassCountRange.x, grassCountRange.y);
            grassMap = new bool[mapSize.x, mapSize.y];
            for(int i = 0; i < grassCount; i++)
            {
                CreateRandomGrass();
            }
        }
    	public bool IsInMap(Vector3 pos)
        {
            return pos.y > startPos.y && pos.x > startPos.x && pos.y < (startPos.y + mapSize.y * gridSize.y) && pos.x < (startPos.x + mapSize.x * gridSize.x);
        }
        public Vector3 Grid2WorldPos(Vector2Int gridPos)
        {
            return new Vector3(gridPos.x * gridSize.x, gridPos.y * gridSize.y, 0) + (Vector3)(0.5f * gridSize);
        }
        public Vector2Int World2GridPos(Vector3 pos)
        {
            return new Vector2Int((int)((pos.x - startPos.x) / gridSize.x), (int)((pos.y - startPos.y) / gridSize.y));
        }
        void CreateRandomGrass()
        {
            var grassSize = new Vector2Int(Random.Range(grassSizeRange.x, grassSizeRange.y), Random.Range(grassSizeRange.x, grassSizeRange.y));
            var grassPos = new Vector2Int(Random.Range(0, mapSize.x - grassSize.x), Random.Range(0, mapSize.y - grassSize.y));

            for (int i = grassPos.x; i < grassPos.x+grassSize.x; i++)
            {
                for (int j = grassPos.y; j < grassPos.y + grassSize.y; j++) 
                {
                    if(!grassMap[i, j])
                    {
                        var grass = PoolManager.Instance().Release(grassPrefab.name);
                        grass.GetComponent<SimpleObject>().Open(Grid2WorldPos(new Vector2Int(i, j)));
                    }
                    grassMap[i, j] = true;
                }
            }
        }

        public bool IsInGrass(Vector3 pos)
        {
            if (!IsInMap(pos))
            {
                return false;
            }

            return grassMap[World2GridPos(pos).x, World2GridPos(pos).y];
        }
	}
}
