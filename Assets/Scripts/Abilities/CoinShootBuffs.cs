using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinShootBuffs : Ability
{
    public float fireRate;
    public float projectileSpeed;
    public int coinsSpent;

    // Use this for initialization
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override bool doAction(PlayerMovement player)
    {
        player.fireRate *= fireRate;
        player.projectileSpeed *= projectileSpeed;
        player.coinsSpent += coinsSpent;
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        return true;
    }

}

public class ShootBuff1 : CoinShootBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Pitcher";
        coinsSpent = -1;
        fireRate = 0.5f;
        projectileSpeed = 1.5f;
        description = "You can't afford to throw 2 coins slow, so you throw 1 coin faster";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("CoinShoot1");
        active = false;
    }
}

public class ShootBuff2 : CoinShootBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Pitcher";
        coinsSpent = -1;
        fireRate = 0.33f;
        projectileSpeed = 2.0f;
        description = "You're like one of those machines that throw tennis balls, only with a coin";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("CoinShoot2");
        active = false;
    }
}

public class ShootBuff3 : CoinShootBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Pitcher";
        coinsSpent = -3;
        fireRate = 0.25f;
        projectileSpeed = 3.0f;
        description = "You're a god, throwing stones like lightning. \n That's right, NO MORE COINS!!!";
        price = 15;
        level = 3;
        image = Resources.Load<Sprite>("CoinShoot3");
        active = false;
    }
}

