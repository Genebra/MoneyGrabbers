using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabelBarriers : MonoBehaviour {

    Timer timer;

    private float timesup;


    // Use this for initialization
    void Start () {
        timer = GameObject.Find("CountDown").GetComponent<Timer>();
    }
	
	// Update is called once per frame
	void Update () {
        timer = GameObject.Find("CountDown").GetComponent<Timer>();

        timesup = timer.getCoolTimer();

        if (timesup == 0)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

}
