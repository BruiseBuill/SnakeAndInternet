using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class SnakeBodyCP : BodyCP
    {
        public List<Transform> bodyList;
        public float nodeInterval;
        [SerializeField] GameObject bodyPrefab;
        SnakeData snakeData;

        protected override void Awake()
        {
            base.Awake();
            snakeData = (SnakeData)data;
        }
        private void LateUpdate()
        {
            if (!data.isAlive.Value)
            {
                return;
            }

            for (int i = 1; i < bodyList.Count; i++)
            {
                if ((bodyList[i].position - bodyList[i - 1].position).sqrMagnitude > nodeInterval * nodeInterval)
                {
                    bodyList[i].position = bodyList[i - 1].position + (bodyList[i].position - bodyList[i - 1].position).normalized * nodeInterval;
                }
                //bodyList[i].up = (bodyList[i].position - bodyList[i - 1].position).normalized;
            }
        }
        public override void Close()
        {
            base.Close();
        }

        public override void Open()
        {
            base.Open();
            bodyList.Add(snakeData.model);
            for (int i = 0; i < snakeData.bodyCount_Var.FullValue - 1; i++) 
            {
                var body = PoolManager.Instance().Release(bodyPrefab.name);
                body.transform.position = snakeData.model.position;
                bodyList.Add(body.transform);
                body.SetActive(true);
            }
        }
    }
}
