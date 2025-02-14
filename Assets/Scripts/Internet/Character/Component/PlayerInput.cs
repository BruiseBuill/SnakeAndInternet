using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Internet
{
 	public class PlayerInput : MonoBehaviour
	{
        FlockControl flock;
        CinemachineVirtualCamera cam;

        [Header("Vision")]
        [SerializeField] float visionRadius;
        [SerializeField] float visionRadiusGrowth;
        float transitionTime = 0.2f;

        Image maskImage;


        private void Awake()
        {
            flock = GetComponent<FlockControl>();
        }

        private void Start()
        {
            if (!flock.isPlayer)
            {
                this.enabled = false;
                return;
            }

            cam = FindObjectOfType<CinemachineVirtualCamera>();
            cam.Follow = flock.transform;

            maskImage = GameObject.FindGameObjectWithTag("Mask").GetComponent<Image>();

            flock.onUnitCountChange += OnUnitCountChange;
            CursorManager.Instance().onMove += (pos) =>
            {

                flock.Move((pos - flock.massCenter).normalized);
            };
            CursorManager.Instance().onShoot += RefreshAttackMask;
            CursorManager.Instance().onShoot += flock.Shoot;

            OnUnitCountChange();
        }
        void RefreshAttackMask()
        {
            if (flock.canShoot)
                StartCoroutine("RefreshingAttackMask");
        }
        IEnumerator RefreshingAttackMask()
        {
            float start = Time.time;
            while (Time.time - start < flock.shootBreak) 
            {
                yield return null;
                maskImage.fillAmount = 1 - (Time.time - start) / flock.shootBreak;
            }
            maskImage.fillAmount = 0;
        }
        void OnUnitCountChange()
        {
            DOTween.To(() => cam.m_Lens.OrthographicSize, (a) => cam.m_Lens.OrthographicSize = a, visionRadius + flock.flockList.Count * visionRadiusGrowth, transitionTime);
        }
        private void OnDestroy()
        {
            flock.onUnitCountChange -= OnUnitCountChange;
            CursorManager.Instance().onMove -= flock.Move;
            CursorManager.Instance().onShoot -= flock.Shoot;
            CursorManager.Instance().onShoot -= RefreshAttackMask;
        }
    }
}
