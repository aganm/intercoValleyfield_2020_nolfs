using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeComponent : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public AudioSource AudioSource;
    public AudioClip HurtClip;
    public AudioClip deathSound;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        AudioSource.PlayOneShot(deathSound);
        GameObject.Destroy(gameObject);
    }

    public void Heal()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        if (col.gameObject.tag.Equals("Enemy")){
            CurrentHealth -= 1;
            Destroy(col.gameObject);
            AudioSource.PlayOneShot(HurtClip,0.7f);
        }
    }

}
