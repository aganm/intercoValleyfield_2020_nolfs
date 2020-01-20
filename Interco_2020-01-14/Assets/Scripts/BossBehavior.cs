using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
        private GameObject camera = null;
        private BoxCollider2D collider = null;

        // Start is called before the first frame update
        void Start()
        {
                camera = GameObject.FindGameObjectWithTag("MainCamera");
                collider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
                transform.position = new Vector3(transform.position.x, camera.transform.position.y - collider.bounds.extents.y / 2f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.gameObject.tag == "Player")
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
}
