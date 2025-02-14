using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snake.Character
{
 	public class SnakeData : AnimalDataCP
	{
        public enum SnakeState { Chase, Patrol };

        public UnityAction<Transform> onFindAim = delegate { };
        public UnityAction<Vector3, Vector3> onTrack = delegate { };
        public UnityAction onLoseAim = delegate { };

        public SnakeState state;
        public Transform aim;
        public Vector3 lastAimPos;

        public List<SnakeBuff> buffList;
        public float visionInNormal;
        public DataWithVariableFloat visionInGrass_Var;
        public DataWithVariableFloat maxMoveSpeed_Var;
        public DataWithVariableFloat accelerate_Var;
        public DataWithVariableInt bodyCount_Var;
        public DataWithVariableFloat rotateSpeed_Var;
        public DataWithVariableFloat moveSpeedWhenRotate_Var;

        public override float normalVisionLength => visionInNormal;
        public override float grassVisionLength => visionInGrass_Var.FullValue;

        public override void Awake()
        {
            base.Awake();

            
        }
        public override void Open()
        {
            base.Open();
            Debug.Log(1);
            state = SnakeState.Patrol;
        }
        public override void Close()
        {
            base.Close();
            GetComponent<BaseControl>().Close();
        }
    }
}
