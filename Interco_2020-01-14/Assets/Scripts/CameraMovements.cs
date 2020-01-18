using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{

        public GameObject target;
        public float smoothSpeed = 10f;
        public Vector3 offset;




        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void LateUpdate()
        {
                transform.position = target.transform.position + offset;
        }
}
