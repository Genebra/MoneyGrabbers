using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text coinsPlayer1;
    public Text coinsPlayer2;
    public Text coinsPlayer3;
    public Text coinsPlayer4;
    public Text roundCounter;

	Spawner spawner;

    // Use this for initialization
    void Start () {
		spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        roundCounter.text = GameObject.Find("Manager").GetComponent<Manager>().countRounds.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		/*if (spawner.getScoreChange()) */
		ScoreUpdate();
	}

    public void ScoreUpdate()
    {
		spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
		
		int numberOfPlayers = GameObject.Find("Manager").GetComponent<Manager>().nPlayers;

		switch (numberOfPlayers)
		{
			case 4:
				coinsPlayer4.text = spawner.getPlayer4().GetComponent<PlayerMovement>().coins.ToString();
				goto case 3;
			case 3:
				coinsPlayer3.text = spawner.getPlayer3().GetComponent<PlayerMovement>().coins.ToString();
				goto case 2;
			case 2:
				coinsPlayer2.text = spawner.getPlayer2().GetComponent<PlayerMovement>().coins.ToString();
				goto case 1;
			case 1:
				coinsPlayer1.text = spawner.getPlayer1().GetComponent<PlayerMovement>().coins.ToString();
				break;
			default:
				break;
		}
	}
}
