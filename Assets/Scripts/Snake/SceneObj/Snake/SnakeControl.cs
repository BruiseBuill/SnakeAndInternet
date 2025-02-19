using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class SnakeInit:ControlInitialization
    {
        public Vector3 pos;
        public Vector3 orient;
        public CharacterType type;
        public List<SnakeBuff> buffList;
        public Vector3 rabbitPos;
    }
    public class SnakeControl : AnimalControl
    {
        SnakeData snakeData;

        protected override void Awake()
        {
            base.Awake();
            snakeData = (SnakeData)data;
        }
        public override void Initialize<T>(T para)
        {
            var init = para as SnakeInit;
            snakeData.model.position = init.pos;
            snakeData.rotateModel.up = init.orient;
            snakeData.characterType = init.type;
            snakeData.buffList = init.buffList;
            snakeData.lastAimPos = init.rabbitPos;
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
