using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class PlayerInputCP : BaseComponent
    {
        AnimalDataCP animalData;
        

        protected override void Awake()
        {
            base.Awake();
            animalData = (AnimalDataCP)data;
        }
        public override void Open()
        {
            if(animalData.characterType!= CharacterType.Player)
            {
                this.enabled = false;
                return;
            }
            CursorManager.Instance().onMove += Move;
        }
        public override void Close()
        {
            CursorManager.Instance().onMove -= Move;
        }
        void Move(Vector3 pos)
        {
            animalData.onMove.Invoke(pos);
        }
    }
}
