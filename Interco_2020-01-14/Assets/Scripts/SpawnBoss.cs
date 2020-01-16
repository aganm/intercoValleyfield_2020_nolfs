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
        boss.transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        boss.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }
}