using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
 	public class RabbitMoveCP : MoveCP
	{
        RabbitData rabbitData;

        protected override void Awake()
        {
            base.Awake();
            rabbitData = (RabbitData)data;
        }
        public override void Open()
        {
            base.Open();
            accelerate = rabbitData.accelerate_Var.FullValue;
            maxMoveSpeed = rabbitData.maxMoveSpeed_Var.FullValue;
            rotateSpeed = rabbitData.rotateSpeed_Var.FullValue;
            minMoveSpeedWhenRotate = rabbitData.moveSpeedWhenRotate_Var.FullValue;
            decelerateWhenRotate = rabbitData.decelerateWhenRotate_Var.FullValue;

            orient = rabbitData.model.up;
            
            presentSpeed = 0;   
        }
        public override void Close()
        {
            base.Close();   
        }
    }
}
