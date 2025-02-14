using BF;
using BF.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
 	public class UI_Pause : UIPanelObject
	{
        [SerializeField] FullButton backBtn;

        [SerializeField] UIPanelObject setting;

        protected override void Awake()
        {
            base.Awake();
            backBtn.onClick.AddListener(Close);
        }
        public override void Open()
        {
            base.Open();
            backBtn.onClick.AddListener(setting.Close);
            
            Time.timeScale = 0.05f;
        }
        public override void Close()
        {
            base.Close();
            backBtn.onClick.RemoveListener(setting.Close);
            
            Time.timeScale = 1f;

        }

    }
}
