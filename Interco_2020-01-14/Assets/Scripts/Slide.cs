using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KonamiCode))]
public class Slide : MonoBehaviour
{
    public float slideSpeed = 0;

    private KonamiCode code;
    private bool cheat = false;
    private bool ctrlUp = true;

    private void Awake()
    {
        code = GetComponent<KonamiCode>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (code.success)
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftControl) && ctrlUp)
            {
                ctrlUp = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(slideSpeed, 0);
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl) && ctrlUp)
            {
                ctrlUp = false;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(slideSpeed * -1, 0);
            }
            if (Input.GetKeyUp(KeyCode.LeftControl)) ctrlUp = true;
        }
    }
}
