using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
 	public class RabbitVisionCP : BaseComponent
    {
        RabbitData rabbitData;

        protected override void Awake()
        {
            base.Awake();
            rabbitData = (RabbitData)data;
        }
        public override void Open()
        {
            //rabbitData.grassVisionLength = rabbitData.visionInGrass_Var.FullValue;
            //rabbitData.normalVisionLength = rabbitData.visionInNormal_Var.FullValue;

        }
        public override void Close()
        {

        }
        private void Update()
        {
            if (!data.isAlive.Value)
            {
                return;
            }

            var length = rabbitData.isInGrass ? rabbitData.grassVisionLength : rabbitData.normalVisionLength;
            //Debug.Log(length);
            var res = Physics2D.OverlapCircleAll(rabbitData.model.position, length, rabbitData.snakeLayer);
            if (res != null) 
            {
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i] && res[i].TryGetComponent<AnimalControl>(out AnimalControl control) && (!MapManager.Instance().IsInGrass(control.Model.position) || rabbitData.isInGrass))  
                    {
                        rabbitData.onFindEnemy(control);
                    }
                }
            }
            else if (rabbitData.state == RabbitData.RabbitState.Escape|| rabbitData.state == RabbitData.RabbitState.Rotate)
            {
                rabbitData.onLoseEnemy.Invoke();
            }
        }
    }
}
