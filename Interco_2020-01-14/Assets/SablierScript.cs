using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SablierScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D Col)
    {
        if(Col.gameObject.tag == "Player")
        {
            Debug.Log("Sablier picked up");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().SablierCollected += 1;
            //Play audio sound
            //disable sablier
            this.gameObject.SetActive(false);
        }

    }
}
