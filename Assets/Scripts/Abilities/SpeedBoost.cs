using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Ability
{
    public float percentage;
    public bool started = false;
    public float time;
    public float timer = 0;
    public float cooldown;
    public float cooldownTimer = 0;
    public bool available = true;

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
        if (!started)
        {
            player.speed *= percentage;
            timer = time;
            started = true;
            available = true;
            return true;
        }
        else {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                return true;
            }
            else
            {
                player.speed /= percentage;
                timer = time;
                cooldownTimer = Time.time;
                started = false;
                available = false;
                return false;
            }
        }
    }

    public override bool checksCondition(PlayerMovement player)
    {
        if (Time.time - cooldownTimer > cooldown || available) return true;
        else return false;
    }

}

public class SpeedBoost1 : SpeedBoost
{
    // Use this for initialization
    public override void Start()
    {
        title = "Shade Runner";
        time = 1;
        cooldown = 2;
        percentage = 1.5f;
        description = "A short sprint that gives you 50% aditional speed for a second. \n 2s Cooldown";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("SpeedBoost1");
        active = true;
    }
}

public class SpeedBoost2 : SpeedBoost
{
    // Use this for initialization
    public override void Start()
    {
        title = "Shade Runner";
        time = 2;
        cooldown = 2;
        percentage = 2;
        description = "The pills you took give you twice the speed for 2 seconds. \n 2s Cooldown";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("SpeedBoost2");
        active = true;
    }
}

public class SpeedBoost3 : SpeedBoost
{
    // Use this for initialization
    public override void Start()
    {
        title = "Shade Runner";
        time = 3;
        percentage = 3.0f;
        cooldown = 3;
        description = "Almost instant tele-transportation that lasts 3 seconds. \n 3s Cooldown";
        price = 25;
        level = 3;
        image = Resources.Load<Sprite>("SpeedBoost3");
        active = true;
    }
}