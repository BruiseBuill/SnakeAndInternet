using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class SnakeBuffCP : BaseComponent
    {
        SnakeData snakeData;

        protected override void Awake()
        {
            base.Awake();
            snakeData = (SnakeData)data;
        }
        public override void Open()
        {
            for(int i = 0; i < snakeData.buffList.Count; i++)
            {
                switch (snakeData.buffList[i])
                {
                    case SnakeBuff.Mobility:
                        Buff_Mobility();
                        break;
                    case SnakeBuff.MoveSpeed:
                        Buff_MoveSpeed();
                        break;
                    case SnakeBuff.VisionInGrass:
                        Buff_VisionInGrass();
                        break;
                    case SnakeBuff.Track:
                        break;
                    case SnakeBuff.Length:
                        Buff_Length();
                        break;
                }
            }
        }
        public override void Close()
        {
            
        }
        void Buff_MoveSpeed()
        {
            snakeData.maxMoveSpeed_Var.additive += 0.2f * snakeData.maxMoveSpeed_Var.Value;
            snakeData.accelerate_Var.additive += 0.15f * snakeData.accelerate_Var.Value;
        }
        void Buff_Mobility()
        {
            snakeData.accelerate_Var.additive += 0.3f * snakeData.accelerate_Var.Value;
            snakeData.rotateSpeed_Var.additive += 0.45f * snakeData.rotateSpeed_Var.Value;
            //snakeData.rotateSpeed_Var
        }
        void Buff_VisionInGrass()
        {
            snakeData.visionInGrass_Var.additive += 0.2f * snakeData.visionInGrass_Var.Value;
        }
        void Buff_Length()
        {
            snakeData.bodyCount_Var.additive += 5;
        }


        
    }
}
