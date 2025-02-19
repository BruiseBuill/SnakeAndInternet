using BF;
using Snake.Other;
using System.Collections;
using System.Collections.Generic;
using Test;
using UnityEngine;

namespace Snake.Character
{
    public class SnakeVisionCP : BaseComponent
    {
        SnakeData snakeData;       

        protected override void Awake()
        {
            base.Awake();
            snakeData = (SnakeData)data;
        }
        public override void Open()
        {
            
        }
        public override void Close()
        {
            
        }
        private void Update()
        {
            if (!data.isAlive.Value)
            {
                return;
            }

            var length = snakeData.isInGrass ? snakeData.grassVisionLength : snakeData.normalVisionLength;
            var res = Physics2D.OverlapCircle(snakeData.model.position, length, snakeData.rabbitLayer);
            if (res && res.TryGetComponent<AnimalControl>(out AnimalControl control) && (!MapManager.Instance().IsInGrass(control.Model.position) || snakeData.isInGrass)) 
            {
                snakeData.onFindAim.Invoke(control.Model);
            }
            else if(snakeData.state == SnakeData.SnakeState.Chase)
            {
                snakeData.onLoseAim.Invoke();
            }

            
            var tips = Physics2D.OverlapCircleAll(snakeData.model.position, length, snakeData.trackTipLayer);
            if (tips != null && tips.Length > 0)  
            {
                int index = -1;
                float maxAlpha = -1;
                for (int i = 0; i < tips.Length; i++) 
                {
                    if (tips[i].GetComponent<TrackTip>().Priority > maxAlpha && (!MapManager.Instance().IsInGrass(tips[i].transform.position) || snakeData.isInGrass))
                    {
                        maxAlpha = tips[i].GetComponent<TrackTip>().Priority;
                        index = i;
                    }
                }
                if (snakeData.characterType == CharacterType.AI && index >= 0) 
                {                    
                    snakeData.onTrack.Invoke(tips[index].GetComponent<TrackTip>().Pos, tips[index].GetComponent<TrackTip>().Orient);
                }
                    
            }
        }
    }
}
