using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SablierScript : MonoBehaviour
{

        public AudioSource audioSource;
        private bool isColected = false;
        List<GameObject> Children = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
                foreach (Transform child in transform)
                {
                        Children.Add(child.gameObject);
                }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D Col)
        {
                if (Col.gameObject.tag == "Player" && isColected == false)
                {
                        isColected = true;
                        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().SablierCollected += 1;
                        //Play audio sound
                        //disable sablier
                        audioSource.Play();
                        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        foreach (GameObject child in Children) child.SetActive(false);
                }

        }
}
