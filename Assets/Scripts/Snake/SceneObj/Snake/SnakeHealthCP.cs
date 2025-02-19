using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class SnakeHealthCP : BaseComponent
    {
        AnimalDataCP animalData;

        protected override void Awake()
        {
            base.Awake();
            animalData = (AnimalDataCP)data;
        }

        public override void Close()
        {
            animalData.onBeKill = delegate { };
        }
        public override void Open()
        {
            animalData.onBeKill += animalData.Close;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (1 << collision.gameObject.layer == animalData.rabbitLayer) 
            {
                var control = collision.gameObject.GetComponent<RabbitControl>();
                control.onBeKill.Invoke();
            }
        }
    }
}
