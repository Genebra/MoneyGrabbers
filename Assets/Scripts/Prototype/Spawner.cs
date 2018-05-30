using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

	public GameObject player1Prefab;
	public GameObject player2Prefab;
	public GameObject player3Prefab;
	public GameObject player4Prefab;
    public static bool scoreChange = false;

    public GameObject chestPrefab;

    public GameObject policePrefab;

	private GameObject player1;
	private GameObject player2;
	private GameObject player3;
	private GameObject player4;

    private GameObject chest;

	public GameObject coin;

	public int coinNumber = 20;
	public float deltaX = 20.0f;
	public float deltaY = 8.0f;

	public int activePlayers;
    public float chestHealth = 100;

    public int chestOpen = 0;

    public int coinNumberchest = 10;
    public float deltaXchest = 2.5f;
    public float deltaYchest = 2.0f;

    public Transform[] policeSpawns;

    Timer timer1;
    Timer timer2;
	private float spawnRate = 3.0f;
	private float nextSpawn;

    private int flagtime = 0;

    private int flagchest;
	private bool spawnPolice;

	private Animator safeAnimator;

    // Use this for initialization
    void Start()
	{
        Manager m = GameObject.Find("Manager").GetComponent<Manager>();
        SpawnPlayer();
        SpawnCoins();
        timer1 = GameObject.Find("StartGame").GetComponent<Timer>();
        timer1.startCount();
        timer2 = GameObject.Find("CountDown").GetComponent<Timer>();
        activePlayers = m.nPlayers;
		spawnPolice = false;
	}

	private void Update()
	{
        if (flagtime == 0)
        {
            if (timer1.getCoolTimer() == 0)
            {
                GameObject.Find("Canvas/Panel (5)").SetActive(false);
                GameObject.Find("CountDown").GetComponent<Timer>().startCount();
                flagtime = 1;
            }
        }

        flagchest = timer2.getFlagChest();

        if (flagchest == 1 )
        {
            chest = (GameObject)Instantiate(chestPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
			safeAnimator = GameObject.FindGameObjectWithTag("Chest").GetComponent<Animator>();
			timer2.changeFlagChest(0);
			spawnPolice = true;
			Instantiate(policePrefab, ChoosePosition(), Quaternion.identity);
			nextSpawn = Time.time + spawnRate;
			SIS.Instance.RoundEnd();
		}

		if (spawnPolice && Time.time > nextSpawn) {
			Instantiate(policePrefab, ChoosePosition(), Quaternion.identity);
			nextSpawn = Time.time + spawnRate;
		}

        if (chestOpen == 1)
        {
            SpawnCoinsChest();
            chestOpen = 0;
        }

        if (Input.GetKeyDown("r")) {
			SceneManager.LoadScene("Prototype");
		}

        if (activePlayers == 0) RoundOver();

    }	

    void SpawnPlayer()
	{
        Vector3 c;
        switch (GameObject.Find("Manager").GetComponent<Manager>().nPlayers) 
		{
			case 4:
                c = GameObject.Find("Player Spawn Points/Player4").GetComponent<Transform>().position;
				player4 = (GameObject) Instantiate(player4Prefab, new Vector2(c.x, c.y), Quaternion.identity);
                player4.GetComponent<PlayerMovement>().nPlayer = 4;
				goto case 3;
			case 3:
                c = GameObject.Find("Player Spawn Points/Player3").GetComponent<Transform>().position;
                player3 = (GameObject) Instantiate(player3Prefab, new Vector2(c.x, c.y), Quaternion.identity);
                player3.GetComponent<PlayerMovement>().nPlayer = 3;
                goto case 2;
			case 2:
                c = GameObject.Find("Player Spawn Points/Player2").GetComponent<Transform>().position;
                player2 = (GameObject) Instantiate(player2Prefab, new Vector2(c.x, c.y), Quaternion.identity);
                player2.GetComponent<PlayerMovement>().nPlayer = 2;
                goto case 1;
			case 1:
                c = GameObject.Find("Player Spawn Points/Player1").GetComponent<Transform>().position;
                player1 = (GameObject) Instantiate(player1Prefab, new Vector2(c.x, c.y), Quaternion.identity);
                player1.GetComponent<PlayerMovement>().nPlayer = 1;
                break;
			default:
				break;
		}
	}

	void SpawnCoins()
	{
		for (int i = 0; i < coinNumber; i++)
		{
			Vector2 position = new Vector2(Random.Range(-deltaX, deltaX), Random.Range(-deltaY, deltaY));
			if (GetComponent<BoxCollider2D>().bounds.Contains(position)) {
				Instantiate(coin, position, Quaternion.identity);
			}
			else {
				i--;
			}
		}
	}

    void SpawnCoinsChest()
    {
        for (int i = 0; i < coinNumberchest; i++)
        {
            Vector2 position = new Vector2(Random.Range(-deltaXchest, deltaXchest), Random.Range(-deltaYchest, deltaYchest));
			if (!chest.GetComponent<BoxCollider2D>().bounds.Contains(position))
			{
				Instantiate(coin, position, Quaternion.identity);
			}
			else
			{
				i--;
			}
		}
	}

	public void DecreaseActivePlayers()
    {
        activePlayers--;
    }

    public void RoundOver()
    {
        Manager m = GameObject.Find("Manager").GetComponent<Manager>();

        int finalCoins1 = m.coinList[0];
        int finalCoins2 = m.coinList[1];
        int finalCoins3 = m.coinList[2];
        int finalCoins4 = m.coinList[3];

        switch (m.nPlayers)
        {
            case (4):
                finalCoins4 += getFinalCoins(player4.GetComponent<PlayerMovement>());
                goto case (3);

            case (3):
                finalCoins3 += getFinalCoins(player3.GetComponent<PlayerMovement>());
                goto case (2);

            case (2):
                finalCoins2 += getFinalCoins(player2.GetComponent<PlayerMovement>());
                goto case (1);

            case (1):
                finalCoins1 += getFinalCoins(player1.GetComponent<PlayerMovement>());
                break;

            default:
                break;
        }

        GameObject.Find("Manager").GetComponent<Manager>().SaveCoins(finalCoins1, finalCoins2, finalCoins3, finalCoins4);

        SceneManager.LoadScene("ScoreBoard");
    }

    public int getFinalCoins(PlayerMovement p)
    {
        int coins = p.getCoins();
        float multiplier;

        if (p.IsArrested())
            multiplier = p.arrestMultiplier;
        else
            multiplier = p.multiplier;

        int final = (int)(coins * multiplier);
        return final;
    }

    public bool chestHit(float damage)
    {
        chestHealth -= damage;
        if (chestHealth <= 0)
        {
			safeAnimator.SetTrigger("Open");
			changeChestOpen(1);
            return true;
        }
		else {
			safeAnimator.SetTrigger("Hit");
			return false;
		}
    }

    public void changeChestOpen(int flag)
    {
        chestOpen = flag;
    }

    public GameObject getPlayer1()
    {
        return player1;
    }

    public GameObject getPlayer2()
    {
        return player2;
    }

    public GameObject getPlayer3()
    {
        return player3;
    }

    public GameObject getPlayer4()
    {
        return player4;
    }

    public bool getScoreChange()
    {
        return scoreChange;
    }

    public int getActivePlayers()
    {
        return activePlayers;
    }

	private Vector2 ChoosePosition() {
		int index = Random.Range(0, policeSpawns.Length);
		float x = policeSpawns[index].position.x;
		float y = policeSpawns[index].position.y;

		return new Vector2(x, y);
	}
}
