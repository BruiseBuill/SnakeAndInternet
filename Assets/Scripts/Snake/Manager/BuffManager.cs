using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Snake.ScriptableObj;
using System.Linq;
using BF.Utility;

namespace Snake
{
    public enum SnakeBuff { VisionInGrass, Mobility, MoveSpeed, Length, Track, All };
    public enum RabbitBuff { VisionInGrass, VisionInNormal, FastRotate, MoveSpeed, Mobility, All };
    public class BuffManager : Single<BuffManager>
	{
        public List<SnakeBuff> snakeBuffList;
        public List<RabbitBuff> rabbitBuffList;

        public bool isPresentSnake => GameControl.Instance().isPresentSnake;
        public List<Buff> buffList;
        public Dictionary<string, Buff> buffDic = new Dictionary<string, Buff>();

        public float sameBuffPossbility;

        public int TrackLevel
        {
            get
            {
                int count = 0;
                for(int i = 0; i < snakeBuffList.Count; i++)
                {
                    if (snakeBuffList[i] == SnakeBuff.Track)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        private void Awake()
        {
            int i = 0;
            for (; i < (int)SnakeBuff.All; i++)
            {
                buffDic.Add("Snake" + ((SnakeBuff)(i)).ToString(), buffList[i]);
            }
            for (i = 0; i < (int)RabbitBuff.All; i++) 
            {
                buffDic.Add("Rabbit" + ((RabbitBuff)(i)).ToString(), buffList[(int)SnakeBuff.All + i]);
            }
        }

        public Vector2Int GetTwoBuffs()
        {
            Vector2Int result = Vector2Int.zero;
            if (isPresentSnake)
            {
                if (snakeBuffList.Count > 0 && Random.Range(0, 1f) < sameBuffPossbility)
                {
                    result.x = (int)snakeBuffList[snakeBuffList.Count - 1];
                    for(int i = 0; ; i++)
                    {
                        result.y = Random.Range(0, (int)SnakeBuff.All);
                        if (result.y != result.x)
                            break;
                    }
                }
                else
                {
                    var r = AsyncRandom.RangeMultiple(0, (int)SnakeBuff.All, 2);
                    result.x = r[0];
                    result.y = r[1];
                }
            }
            else
            {
                if (rabbitBuffList.Count > 0 && Random.Range(0, 1f) < sameBuffPossbility)
                {
                    result.x = (int)rabbitBuffList[rabbitBuffList.Count - 1];
                    for (int i = 0; ; i++)
                    {
                        result.y = Random.Range(0, (int)RabbitBuff.All);
                        if (result.y != result.x)
                            break;
                    }
                }
                else
                {
                    var r = AsyncRandom.RangeMultiple(0, (int)RabbitBuff.All, 2);
                    result.x = r[0];
                    result.y = r[1];
                }
            }
            return result;
        }
        public void AddNewBuff(int index)
        {
            if (GameControl.Instance().isPresentSnake)
            {
                snakeBuffList.Add((SnakeBuff)index);
            }
            else
            {
                rabbitBuffList.Add((RabbitBuff)index);
            }
        }
        public void RemoveNewBuff()
        {
            if (GameControl.Instance().isPresentSnake)
            {
                snakeBuffList.RemoveAt(snakeBuffList.Count - 1);
            }
            else
            {
                rabbitBuffList.RemoveAt(rabbitBuffList.Count - 1);
            }
        }
        public void ClearAllBuff()
        {
            snakeBuffList.Clear();
            rabbitBuffList.Clear();
        }
	}
}
