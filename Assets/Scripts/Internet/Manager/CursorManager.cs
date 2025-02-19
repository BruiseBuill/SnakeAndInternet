using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Internet
{
	public class CursorManager : Single<CursorManager>
	{
        public UnityAction<Vector3> onMove = delegate { };
        public UnityAction onShoot = delegate { };

        public bool canInput;

        new Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }
        private void Start()
        {
            EnableInput();
        }
        public void DisableInput()
        {
            canInput = false;

            InputManager.onClick = delegate { };
            InputManager.onClickRight = delegate { };
        }
        public void EnableInput()
        {
            canInput = true;

            InputManager.onClick += (screenPos) =>
            {
                var pos = camera.ScreenToWorldPoint(screenPos);
                pos.z = 0;
                onMove.Invoke(pos);
            };
            InputManager.onClickRight += () =>
            {
                onShoot.Invoke();
            };
        }
        private void OnDisable()
        {
            DisableInput();
        }
    }
}