using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreCoins : MonoBehaviour {

    private Text scorePlayer1;
    private Text scorePlayer2;
    private Text scorePlayer3;
    private Text scorePlayer4;
    public Text roundCounter;

    Manager m;

    Timer timer;

    // Use this for initialization
    void Start () {
        timer = GameObject.Find("CountDown").GetComponent<Timer>();
        timer.startCount();

        m = GameObject.Find("Manager").GetComponent<Manager>();
        roundCounter.text = m.countRounds.ToString();

        scorePlayer1 = GameObject.Find("score_1").GetComponent<Text>();
        scorePlayer2 = GameObject.Find("score_2").GetComponent<Text>();
        scorePlayer3 = GameObject.Find("score_3").GetComponent<Text>();
        scorePlayer4 = GameObject.Find("score_4").GetComponent<Text>();

        scorePlayer1.text = m.getCoinsPlayer1().ToString();
        scorePlayer2.text = m.getCoinsPlayer2().ToString();
        scorePlayer3.text = m.getCoinsPlayer3().ToString();
        scorePlayer4.text = m.getCoinsPlayer4().ToString();


    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Canvas/Panel (1)/CountDown").GetComponent<Timer>().getCoolTimer() == 0 && m.nRounds > m.countRounds)
        {
            m.countRounds++;

			if (m.getTutorial() == 1)
            {
                SceneManager.LoadScene("AbilitySelection");
            }
            if (m.getTutorial() == 0)
            {
                SceneManager.LoadScene("TutorialSkills");
            }
        }
		else if (m.countRounds == m.nRounds && SIS.Instance.notWritten)
		{
			SIS.Instance.GameOver();
            switch (m.nPlayers)
            {
                case (4):
                    GameObject.Find("Canvas/Canvas_player/Player 4/Text").GetComponent<Text>().text = SIS.Instance.GetID(3).ToString();
                    goto case (3);
                case (3):
                    GameObject.Find("Canvas/Canvas_player/Player 3/Text").GetComponent<Text>().text = SIS.Instance.GetID(2).ToString();
                    goto case (2);
                case (2):
                    GameObject.Find("Canvas/Canvas_player/Player 2/Text").GetComponent<Text>().text = SIS.Instance.GetID(1).ToString();
                    goto case (1);
                case (1):
                    GameObject.Find("Canvas/Canvas_player/Player 1/Text").GetComponent<Text>().text = SIS.Instance.GetID(0).ToString();
                    break;
                default: break;
            }
		}
	}
}

