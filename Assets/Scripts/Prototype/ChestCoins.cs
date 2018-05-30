using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCoins : MonoBehaviour {

    public GameObject coin;

    public int coinNumber = 10;
    public float deltaX = 1.5f;
    public float deltaY = 1.0f;

    Timer timer;

    float timesup;

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
            SpawnCoins();
        }
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            Vector2 position = new Vector2(Random.Range(-deltaX, deltaX), Random.Range(-deltaY, deltaY));
            Instantiate(coin, position, Quaternion.identity);
        }
    }
}
