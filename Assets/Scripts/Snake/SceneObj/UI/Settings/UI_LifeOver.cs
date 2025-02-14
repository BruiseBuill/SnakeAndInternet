using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
 	public class UI_LifeOver : UIPanelObject
	{
        [SerializeField] FullButton clearAllBuffBtn;

        public override void Open()
        {
            base.Open();
            PoolManager.Instance().RecycleAll();
            clearAllBuffBtn.onClick.AddListener(GameControl.Instance().ClearAllBuff);
        }
        public override void Close()
        {
            base.Close();
            clearAllBuffBtn.onClick.RemoveListener(GameControl.Instance().ClearAllBuff);
        }
    }
}
