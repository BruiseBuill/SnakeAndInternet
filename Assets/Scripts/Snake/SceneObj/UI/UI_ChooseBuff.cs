using BF;
using BF.UI;
using Snake.ScriptableObj;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI
{
 	public class UI_ChooseBuff : UIPanelObject
	{
        public GameObject[] choicesGo;

        public TextMeshProUGUI[] nameTexts;
        public TextMeshProUGUI[] descriptionTexts;
        public FullButton[] FullButtons; 

        public Vector2Int choices;


        protected override void Start()
        {
            base.Start();
            GameControl.Instance().onEnterGame += Open;
        }
        public override void Open()
        {
            base.Open();
            EnterGame();
        }
        public override void Close()
        {
            base.Close();
            GameControl.Instance().onEnterGame -= Open;
        }
        void EnterGame()
        {
            SelectBuff();
            FullButtons[0].onClick.AddListener(() => Choosed(choices.x));
            FullButtons[1].onClick.AddListener(() => Choosed(choices.y));
        }
        void SelectBuff()
        {
            var choices = BuffManager.Instance().GetTwoBuffs();
            this.choices = choices;

            if (GameControl.Instance().isPresentSnake)
            {
                string a = "Snake" + (SnakeBuff)(choices[0]);
                string b = "Snake" + (SnakeBuff)(choices[1]);
                Buff buffa = BuffManager.Instance().buffDic[a];
                Buff buffb = BuffManager.Instance().buffDic[b];
                ShowBuff(buffa, buffb);
            }
            else
            {
                string a = "Rabbit" + (RabbitBuff)(choices[0]);
                string b = "Rabbit" + (RabbitBuff)(choices[1]);
                Buff buffa = BuffManager.Instance().buffDic[a];
                Buff buffb = BuffManager.Instance().buffDic[b];
                ShowBuff(buffa, buffb);
            }
        }
        void ShowBuff(Buff a, Buff b)
        {
            nameTexts[0].text = a.name;
            nameTexts[1].text = b.name;
            descriptionTexts[0].text = a.description;
            descriptionTexts[1].text = b.description;
            choicesGo[0].SetActive(true);
            choicesGo[1].SetActive(true);
        }
        void Choosed(int index)
        {
            BuffManager.Instance().AddNewBuff(index);
            GameControl.Instance().onGameStart.Invoke();

            Close();
        }
    }
}
