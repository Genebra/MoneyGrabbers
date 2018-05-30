using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float myCoolTimer = 15;
    public int flagchest = 0;

    private Text timerText;

    public bool start = false;


    // Use this for initialization
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    public float getCoolTimer()
    {
        return myCoolTimer;
    }

    public int getFlagChest()
    {
        return flagchest;
    }

    public bool getFlagStart()
    {
        return start;
    }

    public void changeFlagChest(int flag)
    {
        flagchest = flag;
    }

    public void startCount()
    {
        start = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (start)
        {
            if (myCoolTimer > 0)
            {
                timerText.text = myCoolTimer.ToString("f0");
                myCoolTimer -= Time.deltaTime;
            }
            if (myCoolTimer < 0)
            {
                //timerText.text = "Time's up";
                myCoolTimer = 0;
                flagchest = 1;
            }
        }
    }
}
