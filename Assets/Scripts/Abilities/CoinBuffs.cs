using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBuffs : Ability
{
    public float percentage;

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
        player.multiplier = percentage / 100;
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        return true;
    }

}

public class CoinBuffs1 : CoinBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Multiplication Miracle";
        percentage = 25;
        description = "You get more " + percentage + "% of the money you picked up in a round";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("CoinBuffs1");
        active = false;
    }
}

public class CoinBuffs2 : CoinBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Multiplication Miracle";
        percentage = 50;
        description = "You get more " + percentage + "% of the money you picked up in a round";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("CoinBuffs2");
        active = false;
    }
}

public class CoinBuffs3 : CoinBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Multiplication Miracle";
        percentage = 75;
        description = "You get more " + percentage + "% of the money you picked up in a round";
        price = 15;
        level = 3;
        image = Resources.Load<Sprite>("CoinBuffs3");
        active = false;
    }
}

