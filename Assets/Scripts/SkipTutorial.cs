using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipTutorial : MonoBehaviour {

    public string buttonA1;
    public string buttonA2;
    public string buttonA3;
    public string buttonA4;

    private int ready1 = 0;
    private int ready2 = 0;
    private int ready3 = 0;
    private int ready4 = 0;

    private int nPlayers;

    Manager m;

    // Use this for initialization
    void Start () {
        nPlayers = Input.GetJoystickNames().Length;

        m = GameObject.Find("Manager").GetComponent<Manager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton(buttonA1))
        {
            ready1 = 1;
        }

        if (Input.GetButton(buttonA2))
        {
            ready2 = 1;
        }

        if (Input.GetButton(buttonA3))
        {
            ready3 = 1;
        }

        if (Input.GetButton(buttonA4))
        {
            ready4 = 1;
        }

        switch (nPlayers)
        {
            case 4:
                if (ready4 == 1)
                {
                    goto case 3;
                }
                break;
            case 3:
                if (ready3 == 1)
                {
                    goto case 2;
                }
                break;
            case 2:
                if (ready2 == 1)
                {
                    goto case 1;
                }
                break;
            case 1:
                if (ready1 == 1)
                {
                    m.flagTutorial();
                    SceneManager.LoadScene("AbilitySelection");
                }
                break;
            default:
                break;
        }
    }
}
