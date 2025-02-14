using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BF.Tool
{
    public class TweenAnimator : MonoBehaviour 
    {
        [LabelText("UIStateList")]
        [SerializeField] List<TweenState> animationList = new List<TweenState>();
        [Space(2)]
        [LabelText("UIStateColorList")]
        [SerializeField] List<UIColorState> colorState = new List<UIColorState>();

        public void ChangeState(int index)
        {
            var aniIndex = index <= animationList.Count - 1 ? index : animationList.Count - 1;
            var colorIndex= index <= colorState.Count - 1 ? index : colorState.Count - 1;
            animationList[aniIndex]?.Play();
            if (colorState.Count > 0)
                colorState[colorIndex]?.Play();
        }

        public void GenerateButtons()
        {
            // 动态生成按钮，根据链表中的元素数量生成对应数量的按钮
            for (int i = 0; i < animationList.Count; i++)
            {
                string element = animationList[i].name;
                if (GUILayout.Button($"ChangeState {i + 1}: {element}"))
                {
                    ChangeState(i);
                }
            }
        }

        [Serializable]
        public class TweenState
        {
            [Header("$name")]
            [GUIColor(1,1,0)]
            public string name;

            public List<TweenAnimation> animationList;

            [LabelText("SetToShowGameobject")]
            public List<GameObject> setActiveGOList;

            public void Play() 
            {
                foreach (var ani in animationList)
                {
                    ani.PlayForward();
                }
                foreach (var go in setActiveGOList)
                {
                    go.SetActive(true);
                }
            }
        }

        [Serializable]
        public class UIColorState
        {
            [Serializable]
            public class OneUIState
            {
                public Color color;
                public GameObject go;
            }

            public List<OneUIState> list = new List<OneUIState>();
            public void Play()
            {
                foreach (var ui in list)
                {
                    ui.go.GetComponent<Graphic>().color = ui.color;
                }
            }

        }
    }
}
