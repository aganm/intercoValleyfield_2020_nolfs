﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
        public float loseHeight = -5f;

        void Update()
        {
                if (transform.position.y <= loseHeight)
                {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
        }
}
