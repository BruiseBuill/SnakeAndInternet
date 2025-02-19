using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
 	public class SnakeMoveCP : MoveCP
	{
        SnakeData snakeData;

        protected override void Awake()
        {
            base.Awake();
            snakeData = (SnakeData)data;
        }
        public override void Open()
        {
            base.Open();
            accelerate = snakeData.accelerate_Var.FullValue;
            maxMoveSpeed = snakeData.maxMoveSpeed_Var.FullValue;
            rotateSpeed = snakeData.rotateSpeed_Var.FullValue;
            minMoveSpeedWhenRotate = snakeData.moveSpeedWhenRotate_Var.FullValue;

            orient = snakeData.rotateModel.up;
            presentSpeed = 0;
        }
        public override void Close()
        {
            base.Close();
        }
    }
}
