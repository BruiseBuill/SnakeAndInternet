using BF.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Snake.UI
{
 	public class UI_Teach : UIPanelObject
	{
        public TextMeshProUGUI teachText;

        protected override void Start()
        {
            base.Start();
            GameControl.Instance().onEnterGame += Open;
            GameControl.Instance().onWin += Close;
            GameControl.Instance().onLose += Close;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            GameControl.Instance().onWin -= Close;
            GameControl.Instance().onLose -= Close;
        }
        public override void Close()
        {
            base.Close();
            GameControl.Instance().onEnterGame -= Open;
        }
        public override void Open()
        {
            base.Open();
            StartTeach();
        }
        void StartTeach()
        {
            teachText.text = "";
            if (GameControl.Instance().turnCount == 0)
            {
                teachText.text = "（提示：点击屏幕进行移动，在规定时间内吃掉所有兔子即胜利）";
            }
            else if (GameControl.Instance().turnCount == 1)
            {
                teachText.text = "（提示：点击屏幕进行移动，在规定时间内避免被蛇吃掉即胜利）";
            }
        }
        
    }
}
