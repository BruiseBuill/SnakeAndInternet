using BF;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Snake
{
 	public class CursorManager : Single<CursorManager>
	{
        public UnityAction<Vector3> onMove = delegate { };

        [ReadOnly]
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
            canInput= false;

            InputManager.onClick = delegate { };
            InputManager.onLongPress = delegate { };
        }
        public void EnableInput()
        {
            canInput= true;

            InputManager.onClick += (screenPos) => 
            {
                var pos = camera.ScreenToWorldPoint(screenPos);
                var ray = camera.ScreenPointToRay(screenPos);

                var worldPos = new Vector3(-pos.z / ray.direction.z * ray.direction.x + pos.x, -pos.z / ray.direction.z * ray.direction.y + pos.y, 0);
                onMove.Invoke(worldPos);
            };
            InputManager.onLongPress += (screenPos,time) =>
            {
                var pos = camera.ScreenToWorldPoint(screenPos);
                var ray = camera.ScreenPointToRay(screenPos);

                var worldPos = new Vector3(-pos.z / ray.direction.z * ray.direction.x + pos.x, -pos.z / ray.direction.z * ray.direction.y + pos.y, 0);
                onMove.Invoke(worldPos);
            };
        }
        private void OnDisable()
        {
            DisableInput();
        }
    }
}
