using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    private string lastDirection = "right";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            lastDirection = "left";
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            lastDirection = "right";

        if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (lastDirection)
            {
                case "left":
                    gameObject.GetComponent<Collider2D>().transform.position = new Vector2(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y);
                    break;
                case "right":
                    gameObject.GetComponent<Collider2D>().transform.position = new Vector2(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y);
                    break;
            }
        }
    }
}
