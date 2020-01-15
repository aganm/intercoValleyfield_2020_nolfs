using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image ImgHealthBar;
    public Text TxtHealth;

    public int Min;
    public int Max;

    public int mCurrentValue;

    public float mCurrentPercent;

    public LifeComponent Life;



    // Start is called before the first frame update
    void Start()
    {
        Life = GameObject.FindGameObjectWithTag("Player").GetComponent<LifeComponent>();
        Min = 0;
        Max = (int)Life.MaxHealth;
        SetHealth((int)Life.CurrentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Life != null) SetHealth((int)Life.CurrentHealth);
    }

    public void SetHealth(int health)
    {
        if (health != mCurrentValue)
        {

            mCurrentValue = health;
            mCurrentPercent = (float)mCurrentValue / (float)(Max - Min);
            TxtHealth.text = Convert.ToString(health);

            ImgHealthBar.fillAmount = mCurrentPercent;
        }

    }
}
