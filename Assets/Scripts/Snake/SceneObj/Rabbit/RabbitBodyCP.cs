using BF;
using Snake.Other;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Character
{
    public class RabbitBodyCP : BodyCP
    {
        [SerializeField] GameObject trackTipPrefab;
        [SerializeField] float start;

        [SerializeField] float trackCreateBreak;

        RabbitData rabbitData;

        protected override void Awake()
        {
            base.Awake();
            rabbitData = (RabbitData)data;
        }
        public override void Open()
        {
            base.Open();
        }

        public override void Close()
        {
            base.Close();
        }
        protected override void Update()
        {
            base.Update();
            if (!data.isAlive.Value)
            {
                return;
            }
            start += Time.deltaTime;
            if (start > trackCreateBreak)
            {
                start -= trackCreateBreak;
                var track = PoolManager.Instance().Release(trackTipPrefab.name);
                track.GetComponent<TrackTip>().Open(rabbitData.model.position, rabbitData.rotateModel.up, (1.2f + rabbitData.trackLevel * 1.8f));
            }
        }

    }
}
