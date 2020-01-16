using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointScript : MonoBehaviour
{

    public GameObject VictoryMenu;
    public AudioSource AudioSource;
    public bool isFinished = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Victory");
        if(other.tag == "Player")
        {
            isFinished = true;
            VictoryMenu.SetActive(true);
            Time.timeScale = 0f;
            AudioSource.Play();
        }
    }

}
