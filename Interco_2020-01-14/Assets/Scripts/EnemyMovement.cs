using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
        public float walkSpeed;      // Walkspeed
        public float maxLeft = 0.0f;       // Define maxLeft
        public float maxRight = 5.0f;      // Define maxRight
        public float jumpHeight;
        float walkingDirection = 1.0f;
        Vector2 walkAmount;
        float originalX; // Original float value
        private Rigidbody2D rBody;

        private Vector3 pos1;
        private Vector3 pos2;
        public float speed = 1.0f;

        bool dirRight;


        // Start is called before the first frame update
        void Start()
        {
                pos1 = new Vector3(transform.position.x - 4, 0, 0);
                pos2 = new Vector3(transform.position.x + 4, 0, 0);

                rBody = GetComponent<Rigidbody2D>();
                this.originalX = this.transform.position.x;
                maxLeft = transform.position.x - 2.5f;
                maxRight = transform.position.x + 2.5f;
        }

        // Update is called once per frame
        void Update()
        {


                if (transform.position.x < maxLeft)
                        transform.Translate(Vector2.right * speed * Time.deltaTime);
                else
                        transform.Translate(-Vector2.right * speed * Time.deltaTime);

                if (transform.position.x >= 4.0f)
                {
                        dirRight = false;
                }

                if (transform.position.x <= -4)
                {
                        dirRight = true;
                }

                //walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
                //    if (walkingDirection > 0.0f && transform.position.x >= maxRight)
                //    {
                //        transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
                //        rBody.velocity = new Vector2(rBody.velocity.x, jumpHeight);

                //    }
                //    else if (walkingDirection < 0.0f && transform.position.x <= maxLeft)
                //    {
                //        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                //        rBody.velocity = new Vector2(rBody.velocity.x, jumpHeight);           
                //    }
                //    transform.Translate(walkAmount *-1);
        }
}
