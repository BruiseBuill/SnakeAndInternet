using BF;
using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Snake.Other
{
 	public class CameraControlGR : MonoBehaviour
	{
        [SerializeField] float visionInNormalMultiple;
        [SerializeField] float visionInGrassMultiple;

        //new Camera camera;

        public Volume volume;
        public Vignette vignette;

        public float vigInNormal;
        public float vigInGrass;

        public float transitTime;

        [SerializeField] CinemachineVirtualCamera virtualCamera;

        [SerializeField] GenericEventChannel<float> onPlayerEnterGrass;
        [SerializeField] GenericEventChannel<float> onPlayerExitGrass;
        [SerializeField] GenericEventChannel<object> onPlayerCreate;

        protected void Awake()
        {
            onPlayerEnterGrass.AddListener(OnPlayerEnterGrass);
            onPlayerExitGrass.AddListener(OnPlayerExitGrass);
            onPlayerCreate.AddListener(OnPlayerCreate);

            volume.profile.TryGet<Vignette>(out vignette);
            
        }
        private void OnDestroy()
        {
            onPlayerEnterGrass.RemoveListener(OnPlayerEnterGrass);
            onPlayerExitGrass.RemoveListener(OnPlayerExitGrass);
            onPlayerCreate.RemoveListener(OnPlayerCreate);
        }
        void OnPlayerCreate(object ob)
        {
            virtualCamera.Follow = (ob as GameObject).transform;
            virtualCamera.LookAt = (ob as GameObject).transform;
        }
        void OnPlayerEnterGrass(float length)
        {
            DOTween.To(() => vignette.intensity.value, (x) => vignette.intensity.value = x, vigInGrass, transitTime);
            DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, (x) => virtualCamera.m_Lens.OrthographicSize = x, length * visionInGrassMultiple, transitTime);
            //DOTween.To(() => virtualCamera.m_Lens.FieldOfView, (x) => virtualCamera.m_Lens.FieldOfView =  x, length * visionInGrassMultiple, transitTime);
        }
        void OnPlayerExitGrass(float length)
        {
            DOTween.To(() => vignette.intensity.value, (x) => vignette.intensity.value = x, vigInNormal, transitTime);
            DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, (x) => virtualCamera.m_Lens.OrthographicSize = x, length * visionInNormalMultiple, transitTime);
            //DOTween.To(() => virtualCamera.m_Lens.FieldOfView, (x) => virtualCamera.m_Lens.FieldOfView = x, length * visionInNormalMultiple, transitTime);
        }
	}
}
