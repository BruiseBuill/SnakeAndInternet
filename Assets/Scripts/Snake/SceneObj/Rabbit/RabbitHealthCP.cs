using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class RabbitHealthCP : BaseComponent
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
            if(animalData.characterType== CharacterType.Player)
            {
                GameControl.Instance().PlayerLose();
            }
            else
            {
                Generator.Instance().RabbitDie();
            }
        }
        public override void Open()
        {
            animalData.onBeKill += animalData.Close;
        }

    }
}
