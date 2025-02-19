using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
 	public class UI_Win : UIPanelObject
	{
        [SerializeField] FullButton continueBtn;

        public override void Open()
        {
            base.Open();
            continueBtn.onClick.AddListener(GameControl.Instance().Continue);
            PoolManager.Instance().RecycleAll();
        }
        public override void Close()
        {
            base.Close();
            continueBtn.onClick.RemoveListener(GameControl.Instance().Continue);
        }
    }
}
