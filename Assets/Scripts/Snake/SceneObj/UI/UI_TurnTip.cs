using BF.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Snake.UI
{
 	public class UI_TurnTip : UIPanelObject
	{
        [SerializeField] TextMeshProUGUI tipText;

        string snakeContent = "你出生了，是一只贪吃蛇。\n吃掉所有兔子获得胜利";
        string rabbitContent = "你出生了，是一只兔子。\n躲避蛇捕食一段时间获得胜利";

        [SerializeField] float speedPerCharacter;
        [SerializeField] float additioalShowDuration;

        protected override void Start()
        {
            base.Start();
            GameControl.Instance().onEnterGame += Open;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            GameControl.Instance().onEnterGame -= Open;
        }
        public override void Open()
        {
            base.Open();
            Show();
        }
        void Show()
        {
            if (GameControl.Instance().isPresentSnake)
            {
                tipText.DoText(snakeContent, speedPerCharacter * snakeContent.Length);
                StartCoroutine("WaitShow", speedPerCharacter * snakeContent.Length);
            }
            else
            {
                tipText.DoText(rabbitContent, speedPerCharacter * rabbitContent.Length);
                StartCoroutine("WaitShow", speedPerCharacter * snakeContent.Length);
            }
            
        }
        IEnumerator WaitShow(float baseTime)
        {
            yield return new WaitForSeconds(baseTime + additioalShowDuration);
            Close();
        }
        
    }
}
