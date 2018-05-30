using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;

public class SIS : MonoBehaviour {

	private string currentScene;

	private Manager manager;

    public bool notWritten = true;

    private float endOfRound;
    private int[] IDs;

	private int numPlayers;
	private int currentRound;
	private int numRounds;

    private int[] hits;
    private float[] accuracy;
    private int[] coinsTossed;
	private int[] meleesThrown;
	private float[][] endTimes;
	private float[] endTimesAverages;

	private int[] purchases;
	private int[] upgrades;
	private int[] swaps;
	private int[] actives;
	private int[] passives;
	private int[] coinsSpent;
	private int[] totalCoins;

	private int[] finalCoins;



	public static SIS Instance { get; private set; }

	private void Awake()
	{
		DontDestroyOnLoad(this);

		if (Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			Instance = this;
		}

	}

	// Use this for initialization
	void Start() {
		currentScene = "Start";

		manager = GameObject.Find("Manager").GetComponent<Manager>();
		numPlayers = Input.GetJoystickNames().Length;
		currentRound = manager.countRounds;
		numRounds = manager.nRounds;

		IDs = new int[numPlayers];

		coinsTossed = new int[numPlayers];
		meleesThrown = new int[numPlayers];
		endTimes = new float[numPlayers][];
		for(int i = 0; i < numPlayers; i++) {
			endTimes[i] = new float[numRounds];
		}

		purchases = new int[numPlayers];
		upgrades = new int[numPlayers];
		swaps = new int[numPlayers];
		actives = new int[numPlayers];
		passives = new int[numPlayers];
		coinsSpent = new int[numPlayers];
		totalCoins = new int[numPlayers];

		finalCoins = new int[numPlayers];

		hits = new int[numPlayers];
		accuracy = new float[numPlayers];

		GenerateIDs();
	}
	
	// Update is called once per frame
	void Update () {

		if (SceneManager.GetActiveScene().name != currentScene)
		{
			currentScene = SceneManager.GetActiveScene().name;
			
			if (SceneManager.GetActiveScene().name == "AbilitySelection") {
				currentRound = manager.countRounds;
			}
		}
	}

	public void GenerateIDs() {
		string path = "Assets/Resources/lastID.txt";
		StreamReader reader = new StreamReader(path);

		string ID = reader.ReadToEnd();

		reader.Close();

		int id;

		if (ID == "") {
			id = -1;	
		}
		else {
			id = Int32.Parse(ID);
			FileStream fcreate = File.Open("Assets/Resources/lastID.txt", FileMode.Create);
			fcreate.Close();
		}

		for (int i = 0; i < numPlayers; i++)
		{
			id++;
			IDs[i] = id;
			Debug.Log(id);
		}

		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine(id);
		writer.Close();
	}

	public void GameOver()
	{
		EndGameCalculations();
        writeToFile();
	}

    public void writeToFile()
    {
        string path = "Assets/Resources/data.txt";
        Debug.Log("RIP");
        SIS.Instance.notWritten = false;

        if (File.Exists(path))
        {
            Debug.Log("It exists");
            StreamWriter tw = new StreamWriter(path, true);
            writeData(tw);
            tw.Close();

        } else {
            File.Create(path);
            Debug.Log(File.Exists(path));
            using (var tw = new StreamWriter(path, true))
            {
                Debug.Log(tw != null);
                tw.WriteLine(" ID | hits | accuracy | coinsTossed | meleesThrown | endTimesAverages | purchases | upgrades | swaps | actives | passives | coinsSpent | totalCoins | finalCoins");
                writeData(tw);
                tw.Close();
            }
        }
        
    }

    public void writeData(TextWriter tw)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            string str = IDs[i].ToString().PadRight(4) + " " +
                hits[i].ToString().PadRight(6) + " " +
                accuracy[i].ToString().PadRight(10) + " " +
                coinsTossed[i].ToString().PadRight(13) + " " +
                meleesThrown[i].ToString().PadRight(14) + " " +
                endTimesAverages[i].ToString().PadRight(18) + " " +
                purchases[i].ToString().PadRight(11) + " " +
                upgrades[i].ToString().PadRight(10) + " " +
                swaps[i].ToString().PadRight(7) + " " +
                actives[i].ToString().PadRight(9) + " " +
                passives[i].ToString().PadRight(10) + " " +
                coinsSpent[i].ToString().PadRight(12) + " " +
                totalCoins[i].ToString().PadRight(12) + " " +
                finalCoins[i].ToString().PadRight(12);
            Debug.Log(str);
            tw.WriteLine(str);
        }
    }

	public void EndGameCalculations() {

		//calculate averages
		endTimesAverages = new float[numPlayers];
		int index = 0;

		foreach (var player in endTimes) {
			int invalid = 0;
			float sum = 0.0f;

			foreach (var round in player) {
				if (round == -1) {
					invalid++;
				}
				else {
					sum += round;
				}
			}

            if (numRounds - invalid == 0) endTimesAverages[index] = 0;
			else endTimesAverages[index] = sum / (numRounds - invalid);
			index++;
		}

		// calculate accuracy
		for (int i = 0; i < numPlayers; i++) {

            if (coinsTossed[i] == 0) accuracy[i] = 0;
            else accuracy[i] = hits[i] / coinsTossed[i];
		}

		// get final coin values
		for (int i = 0; i < numPlayers; i++) {
			finalCoins[i] = manager.coinList[i];
		}
	}

	public void CoinTossed(int playerID) {
		coinsTossed[playerID - 1]++;
	}
	
	public void MeleeThrown(int playerID) {
		meleesThrown[playerID - 1]++;
	}

	public void RoundEnd() {
		endOfRound = Time.time;
	}

	public void RanAway(int PlayerID) {
		float ranAway = Time.time;
		endTimes[PlayerID - 1][currentRound - 1] = ranAway - endOfRound;
	}

	public void WasArrested(int PlayerID) {
		endTimes[PlayerID - 1][currentRound - 1] = -1;
	}

	public void Upgraded(int PlayerID, int price, bool type) {
		purchases[PlayerID - 1]++;
		upgrades[PlayerID - 1]++;
		if (type) {
			actives[PlayerID - 1]++;
		}
		else {
			passives[PlayerID - 1]++;
		}
		coinsSpent[PlayerID - 1] += price;
	}

	public void Swapped(int PlayerID, int price, bool type) {
		purchases[PlayerID - 1]++;
		swaps[PlayerID - 1]++;
		if (type)
		{
			actives[PlayerID - 1]++;
		}
		else
		{
			passives[PlayerID - 1]++;
		}
		coinsSpent[PlayerID - 1] += price;
	}

	public void GrabbedCoin(int PlayerID) {
		totalCoins[numPlayers - 1]++;
	}

	public void Hit(int PlayerID) {
		hits[numPlayers - 1]++;
	}

    public int GetID(int i)
    {
        return IDs[i];
    }

}
