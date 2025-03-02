using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BF.Tool
{
    public class UITweenCanvasGroup : TweenAnimation
    {
        public float from;
        public float to;
        [SerializeField] CanvasGroup group;

        public override void PlayForward()
        {
            Play(from, to);
        }
        public override void PlayerReverse()
        {
            Play(to, from);
        }
        void Play(float from, float to)
        {
            if (tween != null && tween.active)
                tween.Kill();

            group.alpha = from;
            tween = group.DOFade(to, duration)
                                   .SetDelay(startDelay)
                                   .SetUpdate(ignoreTimeScale)
                                   .SetEase(animationCurve)
                                   .SetAutoKill(false);

            switch (playStyle)
            {
                case PlayStyle.Once:
                    break;
                case PlayStyle.Repeat:
                    tween.SetLoops(-1, LoopType.Restart);
                    break;
                case PlayStyle.PingPong:
                    tween.SetLoops(-1, LoopType.Yoyo);
                    break;
            }
            tween.onComplete += () => finishEvent.Invoke();
        }
        public override void PlayEnd()
        {
            tween.Kill();
        }

    }
}