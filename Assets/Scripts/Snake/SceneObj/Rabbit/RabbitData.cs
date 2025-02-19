using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snake.Character
{
 	public class RabbitData : AnimalDataCP
	{
        public enum RabbitState {Escape,Idle,Patrol,Rotate  };

        public RabbitState state;

        public UnityAction<AnimalControl> onFindEnemy = delegate { };
        public UnityAction onLoseEnemy = delegate { };

        public List<RabbitBuff> buffList;
        public DataWithVariableFloat visionInGrass_Var;
        public DataWithVariableFloat visionInNormal_Var;
        public DataWithVariableFloat maxMoveSpeed_Var;
        public DataWithVariableFloat accelerate_Var;
        public DataWithVariableFloat rotateSpeed_Var;
        public DataWithVariableFloat moveSpeedWhenRotate_Var;
        public DataWithVariableFloat decelerateWhenRotate_Var;
        public int trackLevel;
        public override float normalVisionLength => visionInNormal_Var.FullValue;
        public override float grassVisionLength => visionInGrass_Var.FullValue;


        public override void Open()
        {
            base.Open();
            state = RabbitState.Idle;
        }
        public override void Close()
        {
            base.Close();
            GetComponent<BaseControl>().Close();
        }
    }
}
