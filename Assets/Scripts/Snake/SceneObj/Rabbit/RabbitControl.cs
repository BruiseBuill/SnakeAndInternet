using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snake.Character
{
    public class RabbitInit:ControlInitialization
    {
        public Vector3 pos;
        public Vector3 orient;
        public CharacterType type;
        public List<RabbitBuff> buffList;
        public int trackLevel;
    }

    public class RabbitControl : AnimalControl
    {
        public RabbitData rabbitData;

        protected override void Awake()
        {
            base.Awake();
            rabbitData = (RabbitData)data;
        }
        public override void Initialize<T>(T para)
        {
            var init = para as RabbitInit;

            rabbitData.model.position = init.pos;
            rabbitData.rotateModel.up = init.orient;
            rabbitData.characterType = init.type;
            rabbitData.buffList = init.buffList;
            rabbitData.trackLevel = init.trackLevel;
        }
        public override void Open()
        {
            data.Open();
            gameObject.SetActive(true);
        }
        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
