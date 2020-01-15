using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockCountDown : MonoBehaviour
{
    public float startTime = 0f; //Hour of the day
    public float levelTime = 0f; //In seconds ??
    private float timeMultiplier = 0f;
    private float lastUpdateTime = 0f;
    private int multiplier = 100;
    private float timer = 0;
    private int H, M;
    public int ElapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Clock").GetComponent<Text>().color = Color.white;
        timeMultiplier = ((19f - startTime) * 60) / levelTime;
        H = (int)startTime;
        M = (int)((startTime - (int)startTime) * multiplier);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (H >= 19)
        {
            GameObject.FindGameObjectWithTag("Clock").GetComponent<Text>().text = "19 H 00";
            if (timer > lastUpdateTime + 1)
            {
                if (GameObject.FindGameObjectWithTag("Clock").GetComponent<Text>().color == Color.white)
                    GameObject.FindGameObjectWithTag("Clock").GetComponent<Text>().color = Color.red;
                else
                    GameObject.FindGameObjectWithTag("Clock").GetComponent<Text>().color = Color.white;
            }
        }
        if (timer > lastUpdateTime + 1)
        {
            M += (int)timeMultiplier;
            ElapsedTime++;
            if ((M % 60) == 0 || M >= 60)
            {
                H++;
                M = 0;
            }
            if(M <10) GameObject.FindGameObjectWithTag("Clock").GetComponent<Text>().text = H + " H 0" + M;
            else GameObject.FindGameObjectWithTag("Clock").GetComponent<Text>().text = H + " H " + M;
            lastUpdateTime = timer;
        }
    }
}
