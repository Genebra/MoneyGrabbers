using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsUp : Ability
{
    public int dropNumCoins;

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
        player.dropNumCoins += dropNumCoins;
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        return true;
    }

}

public class Shielded1 : ShieldsUp
{
    // Use this for initialization
    public override void Start()
    {
        title = "Shielded";
        dropNumCoins = -2;
        description = "Thanks to glueing the coins onto your body, you now lose less 2 coins when hit";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("Shield1");
        active = false;
    }
}

public class Shielded2 : ShieldsUp
{
    // Use this for initialization
    public override void Start()
    {
        title = "Shielded";
        dropNumCoins = -4;
        description = "Thanks to covering your body in tar, you now lose less 4 coins when hit";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("Shield2");
        active = false;
    }
}

public class Shielded3 : ShieldsUp
{
    // Use this for initialization
    public override void Start()
    {
        title = "Shielded";
        dropNumCoins = -6;
        description = "Achieving the protection of a Norse God wasn't easy, but now you don't lose any coins when hit";
        price = 25;
        level = 3;
        image = Resources.Load<Sprite>("Shield3");
        active = false;
    }
}