using BF.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Snake.UI
{
 	public class UI_Clock : UIPanelObject
	{
        public int second;
        public float remainTime;

        public TextMeshProUGUI clockText;

        protected override void Awake()
        {
            base.Awake();
            remainTime = second;
        }
        protected override void Start()
        {
            base.Start();
            GameControl.Instance().onEnterGame += EnterGame;
        }
        public override void Close()
        {
            base.Close();
            GameControl.Instance().onEnterGame -= EnterGame;
        }
        void EnterGame()
        {
            gameObject.SetActive(true);
            clockText.text = "0:" + (Mathf.CeilToInt(remainTime)).ToString();
        }
        private void Update()
        {
            if (!GameControl.Instance().isGameStart)
            {
                return;
            }
            remainTime -= Time.deltaTime;
            clockText.text = "0:" + (Mathf.CeilToInt(remainTime)).ToString();
            if (remainTime < 0)
            {
                if (GameControl.Instance().isPresentSnake)
                {
                    GameControl.Instance().PlayerLose();
                }
                else
                {
                    GameControl.Instance().PlayerWin();
                }
            }
        }
    }
}
