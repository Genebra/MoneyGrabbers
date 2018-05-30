using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public List<Ability> abilityPool;
    public List<Ability[]> playerList;
    public int[] coinList;
    public int nPlayers;
    public int nRounds = 2;
    public int countRounds = 1;
    public int tutorial = 0;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void Start () {

        nPlayers = Input.GetJoystickNames().Length;

        abilityPool = new List<Ability>();

        addAbility(gameObject.AddComponent<LawyeredUp1>());
        addAbility(gameObject.AddComponent<LawyeredUp2>());
        addAbility(gameObject.AddComponent<LawyeredUp3>());

        addAbility(gameObject.AddComponent<MeleeBuff1>());
        addAbility(gameObject.AddComponent<MeleeBuff2>());
        addAbility(gameObject.AddComponent<MeleeBuff3>());

        addAbility(gameObject.AddComponent<CoinBuffs1>());
        addAbility(gameObject.AddComponent<CoinBuffs2>());
        addAbility(gameObject.AddComponent<CoinBuffs3>());

        addAbility(gameObject.AddComponent<ShootBuff1>());
        addAbility(gameObject.AddComponent<ShootBuff2>());
        addAbility(gameObject.AddComponent<ShootBuff3>());

        addAbility(gameObject.AddComponent<Shielded1>());
        addAbility(gameObject.AddComponent<Shielded2>());
        addAbility(gameObject.AddComponent<Shielded3>());

        addAbility(gameObject.AddComponent<SpeedBoost1>());
        addAbility(gameObject.AddComponent<SpeedBoost2>());
        addAbility(gameObject.AddComponent<SpeedBoost3>());

        addAbility(gameObject.AddComponent<Undercover1>());
        addAbility(gameObject.AddComponent<Undercover2>());
        addAbility(gameObject.AddComponent<Undercover3>());

        addAbility(gameObject.AddComponent<SlowDown1>());
        addAbility(gameObject.AddComponent<SlowDown2>());
        addAbility(gameObject.AddComponent<SlowDown3>());

        addAbility(gameObject.AddComponent<Magnet1>());
        addAbility(gameObject.AddComponent<Magnet2>());
        addAbility(gameObject.AddComponent<Magnet3>());

        //addAbility(gameObject.AddComponent<Shocker1>());
        //addAbility(gameObject.AddComponent<Shocker2>());
        //addAbility(gameObject.AddComponent<Shocker3>());



        playerList = new List<Ability[]>();
        coinList = new int[4];

        for (int i = 0; i < 4; i++)
        {
            Ability[] list = new Ability[4];
            list[0] = gameObject.AddComponent<EmptyPassive>();
            list[1] = gameObject.AddComponent<EmptyPassive>();
            list[2] = gameObject.AddComponent<EmptyActive>();
            list[3] = gameObject.AddComponent<EmptyActive>();
            playerList.Add(list);
            coinList[i] = 0;
        }
       
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SaveCoins(int coins1, int coins2, int coins3, int coins4)
    {
        coinList[0] = coins1;
        coinList[1] = coins2;
        coinList[2] = coins3;
        coinList[3] = coins4;
    }

    public int getCoinsPlayer1()
    {
        return coinList[0];
    }

    public int getCoinsPlayer2()
    {
        return coinList[1];
    }

    public int getCoinsPlayer3()
    {
        return coinList[2];
    }

    public int getCoinsPlayer4()
    {
        return coinList[3];
    }

    public void flagTutorial()
    {
        tutorial = 1; 
    }

    public int getTutorial()
    {
        return tutorial;
    }

    public void addAbility(Ability a)
    {
        a.Start();
        abilityPool.Add(a);
    }

}