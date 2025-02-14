using BF;
using BF.Utility;
using Snake.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Other
{
 	public class GrassTransparentGR : MonoBehaviour
	{
        [SerializeField] GenericEventChannel<float> onPlayerEnterGrass;
        [SerializeField] GenericEventChannel<float> onPlayerExitGrass;
        [SerializeField] GenericEventChannel<object> onPlayerCreate;

        Transform playerTrans;
        LayerMask grassLayer;

        [SerializeField] List<Grass> grassList = new List<Grass>();

        [SerializeField] bool isInGrass;
        float length;

        private void Start()
        {
            onPlayerEnterGrass.AddListener(EnterGrass);
            onPlayerExitGrass.AddListener(ExitGrass);
            onPlayerCreate.AddListener(SetPlayer);
            grassLayer = LayerMask.GetMask("Grass");
        }
        private void OnDestroy()
        {
            onPlayerEnterGrass.RemoveListener(EnterGrass);
            onPlayerExitGrass.RemoveListener(ExitGrass);
            onPlayerCreate.RemoveListener(SetPlayer);
        }
        void SetPlayer(object player)
        {
            playerTrans = (player as GameObject).GetComponent<AnimalControl>().Model;
        }
        void EnterGrass(float length)
        {
            isInGrass = true;
            this.length = length;
        }
        private void Update()
        {
            if (!isInGrass)
            {
                return;
            }
            var res = Physics2D.OverlapCircleAll(playerTrans.position, length, grassLayer);
            if (res != null)
            {
                for (int i = 0; i < grassList.Count; i++)
                {
                    grassList[i].IncreaseAlpha();
                }
                grassList.Clear();
                for (int i = 0; i < res.Length; i++)
                {
                    grassList.Add(res[i].GetComponent<Grass>()) ;
                    grassList[i].DecreaseAlpha();
                }
            }
        }
        void ExitGrass(float length)
        {
            isInGrass = false;
            for (int i = 0; i < grassList.Count; i++)
            {
                grassList[i].IncreaseAlpha();
            }
            grassList.Clear();
        }
    }
}
