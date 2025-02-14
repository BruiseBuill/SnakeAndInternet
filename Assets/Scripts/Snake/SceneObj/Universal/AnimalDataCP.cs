using BF;
using BF.Utility;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snake.Character
{
    public enum CharacterType { Player, AI };
    public class AnimalDataCP : BaseShareData
	{
        [ReadOnly]
        public CharacterType characterType;

        public UnityAction<Vector3> onMove = delegate { };
        public UnityAction onStopMove = delegate { };

        public UnityAction onBeKill = delegate { };

        public Transform model; 
        public Transform rotateModel;
        public Rigidbody2D rb;

        [ReadOnly]
        public bool isInGrass;

        public virtual float normalVisionLength => 0;
        public virtual float grassVisionLength => 0;

        public const float minSqrDistance = 0.01f;

        public LayerMask grassLayer;
        public LayerMask rabbitLayer;
        public LayerMask snakeLayer;
        public LayerMask trackTipLayer;
        public LayerMask wallLayer;

        public override void Awake()
        {
            base.Awake();

            grassLayer = LayerMask.GetMask("Grass");
            rabbitLayer = LayerMask.GetMask("Rabbit");
            snakeLayer = LayerMask.GetMask("Snake");
            trackTipLayer = LayerMask.GetMask("Track");
            wallLayer = LayerMask.GetMask("Wall");
        }
    }
}
