using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
        public GameObject caveLight;
        public GameObject door;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                Destroy(door);
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("worldLight"))
                {
                        go.SetActive(false);
                }
                GameObject g = GameObject.FindGameObjectWithTag("caveLight");
                if (g != null)
                {
                        g.SetActive(true);
                }
                else
                {
                        caveLight.SetActive(true);
                }
                GameObject.FindGameObjectWithTag("boss").GetComponent<SpawnBoss>().enabled = true;
        }
}
