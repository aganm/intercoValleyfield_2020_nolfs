using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    private float originalWidth = 0;
    private float originalHeight = 0;
    // Start is called before the first frame update
    void Start()
    {
        originalHeight = gameObject.transform.localScale.y;
        originalWidth = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            gameObject.GetComponent<Collider2D>().transform.localScale = new Vector3(originalWidth, originalHeight / 2, 0);
        else
            gameObject.GetComponent<Collider2D>().transform.localScale = new Vector3(originalWidth, originalHeight, 0);
    }
}