using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawyeredUp : Ability
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
        player.arrestMultiplier = percentage / 100;
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        return true;
    }

}

public class LawyeredUp1 : LawyeredUp
{
    // Use this for initialization
    public override void Start()
    {
        title = "Lawyered Up";
        percentage = 25;
        description = "You get to keep "+ percentage + "% of the money when you get arrested";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("LawyeredUp1");
        active = false;
    }
}

public class LawyeredUp2 : LawyeredUp
{
    // Use this for initialization
    public override void Start()
    {
        title = "Lawyered Up";
        percentage = 50;
        description = "You get to keep " + percentage + "% of the money when you get arrested";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("LawyeredUp2");
        active = false;
    }
}

public class LawyeredUp3 : LawyeredUp
{
    // Use this for initialization
    public override void Start()
    {
        title = "Lawyered Up";
        percentage = 75;
        description = "You get to keep " + percentage + "% of the money when you get arrested";
        price = 15;
        level = 3;
        image = Resources.Load<Sprite>("LawyeredUp3");
        active = false;
    }
}

