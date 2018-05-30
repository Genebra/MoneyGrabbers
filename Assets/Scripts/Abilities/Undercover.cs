using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undercover : Ability
{
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
            foreach (var police in GameObject.FindGameObjectsWithTag("Police"))
            {
                police.gameObject.GetComponent<PoliceMan>().whoBribed[player.nPlayer - 1] = true;
            }
            timer = time;
            started = true;
            available = true;
            return true;
        }
        else
        {
            if (timer > 0)
            {
                foreach (var police in GameObject.FindGameObjectsWithTag("Police"))
                {
                    police.gameObject.GetComponent<PoliceMan>().whoBribed[player.nPlayer - 1] = true;
                }
                timer -= Time.deltaTime;
                return true;
            }
            else {
                foreach (var police in GameObject.FindGameObjectsWithTag("Police"))
                {
                    police.gameObject.GetComponent<PoliceMan>().whoBribed[player.nPlayer - 1] = false;
                }
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

public class Undercover1 : Undercover
{
    // Use this for initialization
    public override void Start()
    {
        title = "Dirty Cops";
        time = 1;
        cooldown = 2;
        description = "Here's a little compensation for donating to the law enforcement: Cops ignore you for 1 second. \n 2s Cooldown";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("Undercover1");
        active = true;
    }
}

public class Undercover2 : Undercover
{
    // Use this for initialization
    public override void Start()
    {
        title = "Dirty Cops";
        time = 2;
        cooldown = 2;
        description = "What whould law enforcement do without you? Cops ignore you for 2 seconds. \n 2s Cooldown";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("Undercover2");
        active = true;
    }
}

public class Undercover3 : Undercover
{
    // Use this for initialization
    public override void Start()
    {
        title = "Dirty Cops";
        time = 3;
        cooldown = 3;
        description = "You basically privatized the law enforcement. Cops ignore you for 4 seconds. \n 2s Cooldown";
        price = 25;
        level = 3;
        image = Resources.Load<Sprite>("Undercover3");
        active = true;
    }
}