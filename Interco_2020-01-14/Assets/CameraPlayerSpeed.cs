using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerSpeed : MonoBehaviour
{
        private float defaultSize;

        // Start is called before the first frame update
        void Start()
        {
                defaultSize = GetComponent<Camera>().orthographicSize;
        }

        // Update is called once per frame
        void Update()
        {
                float speed = Mathf.Abs(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.magnitude);
                float newSize = defaultSize * (speed * 0.1f + 1f);
                GetComponent<Camera>().orthographicSize = newSize;
        }
}
