using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
        public float YOffset = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
                transform.position = new Vector3(transform.position.x, GameObject.FindGameObjectWithTag("MainCamera").transform.position.y + YOffset);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.gameObject.tag == "Player")
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
}
