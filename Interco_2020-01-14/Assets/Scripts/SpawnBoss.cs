using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject boss;
    public Transform spawnPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("isSpawn");
        boss = GameObject.Instantiate(boss);
        float playerX = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position.x -30;
        boss.transform.position = new Vector2(playerX, 0);
       
        
    }

    // Update is called once per frame
    void Update()
    {
        boss.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    
}