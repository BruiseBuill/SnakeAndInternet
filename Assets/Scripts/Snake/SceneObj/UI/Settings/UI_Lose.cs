using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
    public class UI_Lose : UIPanelObject
    {
        [SerializeField] FullButton restartBtn;
        [SerializeField] FullButton clearAllBuffBtn;

        public override void Open()
        {
            base.Open();
            PoolManager.Instance().RecycleAll();

            restartBtn.onClick.AddListener(GameControl.Instance().GameRestart);
            
            clearAllBuffBtn.onClick.AddListener(GameControl.Instance().ClearAllBuff);
        }
        public override void Close()
        {
            base.Close();

            restartBtn.onClick.RemoveListener(GameControl.Instance().GameRestart);
            
            clearAllBuffBtn.onClick.RemoveListener(GameControl.Instance().ClearAllBuff);
        }
    }
}