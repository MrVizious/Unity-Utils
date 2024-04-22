using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UlitityScripts
{

    public enum Space
    {
        Local,
        World
    }
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
                transform.Rotate(rotationAxis, speed * Time.deltaTime);
            }
            else if (space == Space.World)
            {
                transform.Rotate(transform.TransformDirection(rotationAxis), speed * Time.deltaTime);
            }
        }
    }

}