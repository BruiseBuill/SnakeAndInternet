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
                teachText.text = "����ʾ�������Ļ�����ƶ����ڹ涨ʱ���ڳԵ��������Ӽ�ʤ����";
            }
            else if (GameControl.Instance().turnCount == 1)
            {
                teachText.text = "����ʾ�������Ļ�����ƶ����ڹ涨ʱ���ڱ��ⱻ�߳Ե���ʤ����";
            }
        }
        
    }
}
