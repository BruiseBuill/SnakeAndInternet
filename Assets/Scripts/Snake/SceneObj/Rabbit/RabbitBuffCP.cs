using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class RabbitBuffCP : BaseComponent
    {
        RabbitData rabbitData;

        protected override void Awake()
        {
            base.Awake();
            rabbitData = (RabbitData)data;
        }
        public override void Open()
        {
            for(int i = 0; i < rabbitData.buffList.Count; i++)
            {
                switch (rabbitData.buffList[i])
                {
                    case RabbitBuff.Mobility:
                        Buff_Mobility();
                        break;
                    case RabbitBuff.VisionInGrass:
                        Buff_VisionInGrass();
                        break;
                    case RabbitBuff.VisionInNormal:
                        Buff_VisionInNormal();
                        break;
                    case RabbitBuff.FastRotate:
                        Buff_FastRotate();
                        break;
                    case RabbitBuff.MoveSpeed:
                        Buff_MoveSpeed();
                        break;
                }
            }
        }
        public override void Close()
        {
            
        }
        void Buff_Mobility()
        {
            rabbitData.accelerate_Var.additive += 0.3f * rabbitData.accelerate_Var.Value;
            rabbitData.decelerateWhenRotate_Var.additive += 0.2f * rabbitData.decelerateWhenRotate_Var.Value;
        }
        void Buff_VisionInGrass()
        {
            rabbitData.visionInGrass_Var.additive += rabbitData.visionInGrass_Var.Value * 0.25f;
        }
        void Buff_VisionInNormal()
        {
            rabbitData.visionInNormal_Var.additive += rabbitData.visionInNormal_Var.Value * 0.25f;
        }
        void Buff_MoveSpeed()
        {
            rabbitData.maxMoveSpeed_Var.additive += 0.2f * rabbitData.maxMoveSpeed_Var.Value;
            rabbitData.accelerate_Var.additive += 0.3f * rabbitData.accelerate_Var.Value;
        }
        void Buff_FastRotate()
        {
            rabbitData.rotateSpeed_Var.additive += 0.2f * rabbitData.rotateSpeed_Var.Value;
            rabbitData.moveSpeedWhenRotate_Var.additive += 0.2f * rabbitData.moveSpeedWhenRotate_Var.Value;
        }
    }
}
