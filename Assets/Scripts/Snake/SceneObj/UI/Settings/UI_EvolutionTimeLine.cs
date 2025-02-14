using BF;
using BF.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Snake.UI
{
 	public class UI_EvolutionTimeLine : UIPanelObject
	{
        [SerializeField] GameObject tipUpPrefab;
        [SerializeField] GameObject tipDownPrefab;
        [SerializeField] RectTransform content;
        [SerializeField] float tipLength;

        [SerializeField] float textOneCharacterTime = 0.07f;

        public override void Open()
        {
            base.Open();
            RefreshTimeLine();
        }
        public override void Close()
        {
            base.Close();
        }
        void RefreshTimeLine()
        {
            for(int i = content.childCount; i > 0; i--)
            {
                Destroy(content.GetChild(i - 1).gameObject);
            }

            for (int i = 0; i < GameControl.Instance().turnCount + 1; i++) 
            {
                if (i % 2 == 0)
                {
                    var tip = PoolManager.Instance().Release(tipUpPrefab.name);
                    string con = "蛇进化出了" + BuffManager.Instance().buffDic["Snake" + BuffManager.Instance().snakeBuffList[(i / 2)].ToString()].name;
                    if (i == GameControl.Instance().turnCount)
                    {
                        tip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DoText(con, textOneCharacterTime * con.Length).SetUpdate(true);
                    }
                    else
                    {
                        tip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = con;
                    }
                    tip.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "XX" + i.ToString() + "年";
                    tip.transform.SetParent(content);
                    tip.transform.localScale = Vector3.one;
                    tip.SetActive(true);
                }
                else
                {
                    var tip = PoolManager.Instance().Release(tipDownPrefab.name);
                    string con = "兔子进化出了" + BuffManager.Instance().buffDic["Rabbit" + BuffManager.Instance().rabbitBuffList[(i / 2)].ToString()].name;
                    if (i == GameControl.Instance().turnCount)
                    {
                        tip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().DoText(con, textOneCharacterTime * con.Length).SetUpdate(true);
                    }
                    else
                    {
                        tip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = con;
                    }
                    
                    tip.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "XX" + i.ToString() + "年";
                    tip.transform.SetParent(content);
                    tip.transform.localScale = Vector3.one;
                    tip.SetActive(true);
                }
            }

            content.sizeDelta = new Vector2(tipLength * content.childCount, content.sizeDelta.y);
        }
	}
}
