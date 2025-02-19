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

        string snakeContent = "������ˣ���һֻ̰���ߡ�\n�Ե��������ӻ��ʤ��";
        string rabbitContent = "������ˣ���һֻ���ӡ�\n����߲�ʳһ��ʱ����ʤ��";

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
