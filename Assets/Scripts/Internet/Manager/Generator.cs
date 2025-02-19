using BF;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Internet
{
	public class Generator : Single<Generator>
	{
		[Header("Flock")]
		[SerializeField] GameObject flockPrefab;
		[SerializeField] int flockCount;
		[SerializeField] List<string> tagList;
		[SerializeField] List<FlockControl> flockList;
		[SerializeField] List<Sprite> skinList;
		[SerializeField] float sqrInitialFlockInterval;
		public List<FlockControl> FlockList => flockList;

		[Header("Food")]
		[SerializeField] GameObject foodPrefab;
		[SerializeField] int initialFoodCount;
		[SerializeField] int createIntervalMilliSecond;

		[ContextMenu("Start")]
		public void StartGame()
		{
            CreateFlock();
            CreateFood();
        }
		void CreateFlock()
		{
			var skinList = SkinManager.Instance().GetSkinList(flockCount);
			for(int i = 0; i < flockCount; i++)
			{
				var flock = PoolManager.Instance().Release(flockPrefab.name).GetComponent<FlockControl>();
				flock.Initialize(GetFlockPos(), LayerManager.Instance().GetLeagurLayer(), tagList[i], skinList[i], i == 0);
				flockList.Add(flock);
            }
		}
		Vector3 GetFlockPos()
		{
			while (true)
			{
				var pos = MapManager.Instance().GetRandomPos();
				bool found = false;
				for(int i = 0; i < flockList.Count; i++)
				{
					if ((flockList[i].massCenter - pos).sqrMagnitude < sqrInitialFlockInterval)
					{
						found = true;
						break;
					}
				}
				if (found)
					continue;
				else
					return pos;
            }
		}
		async void CreateFood()
		{
			for(int i = 0; i < initialFoodCount; i++)
			{
				var food = PoolManager.Instance().Release(foodPrefab.name).GetComponent<Food>();
				food.Initialize(MapManager.Instance().GetRandomPos());
			}

			for (int i = 0; ; i++) 
			{
                await Task.Delay(createIntervalMilliSecond);
                var food = PoolManager.Instance().Release(foodPrefab.name).GetComponent<Food>();
                food.Initialize(MapManager.Instance().GetRandomPos());
            }
		}
	}
}