using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding");
        if(other.gameObject.tag == "enemy")
        {
            LifeComponent life = GetComponentInParent<LifeComponent>(); //Get Life component of player
            life.CurrentHealth -= 1; //Take off 1 health point
            Destroy(other.gameObject);
        }
    }

}
