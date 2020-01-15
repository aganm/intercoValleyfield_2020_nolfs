using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip Track1;
    public AudioClip Track2;

    private bool round2IsStarted;


    // Start is called before the first frame update
    void Start()
    {
        round2IsStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkCounter() && !round2IsStarted)
        {
            StartSecondRound();
        }
    }

    bool checkCounter()
    {
        if (GetComponent<ClockCountDown>().ElapsedTime >
            GetComponent<ClockCountDown>().levelTime / 2) return true; //Should return true if more than half of the time is passed
        else return false;
    }

    void StartSecondRound()
    {
        Debug.Log("Second Round strated");
        round2IsStarted = true;
        AudioSource.Stop();
        AudioSource.PlayOneShot(Track2);
        //Change backgroun or colour (dark ambiance)
    }
}
