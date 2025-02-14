using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class BodyCP : BaseComponent
    {
        [SerializeField] GenericEventChannel<float> onPlayerEnterGrass;
        [SerializeField] GenericEventChannel<float> onPlayerExitGrass;
        AnimalDataCP animalData;

        protected override void Awake()
        {
            base.Awake();
            animalData = (AnimalDataCP)data;
        }
        public override void Open()
        {
            var isInGrass = MapManager.Instance().IsInGrass(animalData.model.position);
            animalData.isInGrass = isInGrass;
            if(animalData.characterType == CharacterType.Player)
            {
                if (isInGrass)
                {
                    onPlayerEnterGrass.Invoke(animalData.grassVisionLength);
                }
                else if (!isInGrass)
                {
                    onPlayerExitGrass.Invoke(animalData.normalVisionLength);
                }
            }
        }
        public override void Close()
        {
            
        }
        protected virtual void Update()
        {
            if (!data.isAlive.Value)
            {
                return;
            }

            var isInGrass = MapManager.Instance().IsInGrass(animalData.model.position);
            if (isInGrass && !animalData.isInGrass)
            {
                animalData.isInGrass = true;
                if (animalData.characterType == CharacterType.Player)
                    onPlayerEnterGrass.Invoke(animalData.grassVisionLength);
            }
            else if(!isInGrass && animalData.isInGrass)
            {
                animalData.isInGrass = false;

                if (animalData.characterType == CharacterType.Player)
                    onPlayerExitGrass.Invoke(animalData.normalVisionLength);
            }
        }
    }
}
