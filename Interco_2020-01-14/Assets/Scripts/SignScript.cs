using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{
        public string TextTag;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnTriggerEnter2D(Collider2D col)
        {
                if (col.gameObject.tag.Equals("Player"))
                {
                GetComponentInChildren<MeshRenderer>().enabled = true;
                }
        }


        public void OnTriggerExit2D(Collider2D col)
        {
                if (col.gameObject.tag.Equals("Player"))
                {
                GetComponentInChildren<MeshRenderer>().enabled = false;
                }
        }

}
