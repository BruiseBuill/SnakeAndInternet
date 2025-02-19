using BF.Tool;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BF.UI
{
    [RequireComponent(typeof(TextMeshProUGUI),typeof(UITweenScale))]
	public class TextCountRoll : MonoBehaviour
    {
		public int from;
		public int to;
		[SerializeField] TextMeshProUGUI textMesh;
		[SerializeField] float speed;
        ///[SerializeField] TweenAnimation[] 

        private void Start()
        {
            from = int.Parse(textMesh.text);
            textMesh.OnPreRenderText += ChangeText;
        }
        void ChangeText(TMP_TextInfo tMP_Text)
        {
            to = int.Parse(textMesh.text);
            if (from != to)
            {

            }
        }
        //IEnumerator Changing()
        
    }
}