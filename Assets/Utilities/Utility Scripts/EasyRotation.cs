using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UlitityScripts
{

    public class EasyRotation : MonoBehaviour
    {
        public Space space = Space.World;
        public Vector3 rotationAxis = Vector3.up;
        public float speed = 1f;

        private void Update()
        {
            Rotate();
        }
        private void Rotate()
        {
            if (rotationAxis == Vector3.zero || speed == 0f) return;
            if (space == Space.World)
            {
                transform.Rotate(rotationAxis, speed * Time.deltaTime, UnityEngine.Space.World);
            }
            else if (space == Space.Self)
            {
                transform.Rotate(rotationAxis, speed * Time.deltaTime, UnityEngine.Space.Self);
            }
        }
    }

}