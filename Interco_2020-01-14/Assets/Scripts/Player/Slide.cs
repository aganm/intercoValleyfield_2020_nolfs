using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KonamiCode))]
public class Slide : MonoBehaviour
{
        public float slideSpeed = 0;

        private KonamiCode code;
        private Animator animator;
        private bool cheat = false;
        private bool ctrlUp = true;
        private int dashingCoolDown;

        private void Awake()
        {
                code = GetComponent<KonamiCode>();
                animator = GetComponent<Animator>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        void FixedUpdate()
        {
                if (code.success)
                {

                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && ctrlUp)
                        {
                                animator.SetBool("isDashing", true);
                        }
                        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && ctrlUp)
                        {
                                animator.SetBool("isDashing", true);
                        }
                        else
                        {
                                animator.SetBool("isDashing", false);
                        }

                }


        }

        // Update is called once per frame
        void Update()
        {
                if (code.success)
                {
                        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && ctrlUp)
                        {
                                ctrlUp = false;
                                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(slideSpeed, 0);
                        }
                        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && ctrlUp)
                        {
                                ctrlUp = false;
                                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(slideSpeed * -1, 0);
                        }

                        if (Input.GetKeyUp(KeyCode.LeftShift))
                        {
                                ctrlUp = true;
                        }
                }
        }
}
