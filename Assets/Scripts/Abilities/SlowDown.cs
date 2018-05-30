using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : Ability
{
    public bool started = false;
    public float dilationFactor;
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
                police.gameObject.GetComponent<PoliceMan>().speed *= dilationFactor;
                police.gameObject.GetComponent<PoliceMan>().slowedDown = true;
            }
            foreach (var players in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(players.gameObject.GetComponent<PlayerMovement>().nPlayer != player.nPlayer && !players.gameObject.GetComponent<PlayerMovement>().slowedDown)
                {
                    players.gameObject.GetComponent<PlayerMovement>().speed *= dilationFactor;
                    players.gameObject.GetComponent<PlayerMovement>().slowedDown = true;
                }
                    
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
                    if (!police.gameObject.GetComponent<PoliceMan>().slowedDown)
                    {
                        police.gameObject.GetComponent<PoliceMan>().speed *= dilationFactor;
                        police.gameObject.GetComponent<PoliceMan>().slowedDown = true;
                    }
                }
                foreach (var players in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (players.gameObject.GetComponent<PlayerMovement>().nPlayer != player.nPlayer && !players.gameObject.GetComponent<PlayerMovement>().slowedDown)
                    {
                        players.gameObject.GetComponent<PlayerMovement>().speed *= dilationFactor;
                        players.gameObject.GetComponent<PlayerMovement>().slowedDown = true;
                    }
                        
                }
                timer -= Time.deltaTime;
                return true;
            }
            else
            {
                foreach (var police in GameObject.FindGameObjectsWithTag("Police"))
                {
                    police.gameObject.GetComponent<PoliceMan>().speed /= dilationFactor;
                    police.gameObject.GetComponent<PoliceMan>().slowedDown = false;
                }
                foreach (var players in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (players.gameObject.GetComponent<PlayerMovement>().nPlayer != player.nPlayer && players.gameObject.GetComponent<PlayerMovement>().slowedDown)
                    {
                        players.gameObject.GetComponent<PlayerMovement>().speed /= dilationFactor;
                        players.gameObject.GetComponent<PlayerMovement>().slowedDown = false;
                    }
                        
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

public class SlowDown1 : SlowDown
{
    // Use this for initialization
    public override void Start()
    {
        title = "Chronophobia";
        time = 1.5f;
        dilationFactor = 0.5f;
        cooldown = 2;
        description = "Do you know when everybody else seems to be in slow motion for 1.5 seconds? You do now. \n 2s Cooldown";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("SlowDown1");
        active = true;
    }
}

public class SlowDown2 : SlowDown
{
    // Use this for initialization
    public override void Start()
    {
        title = "Chronophobia";
        time = 3;
        dilationFactor = 0.3f;
        cooldown = 2.5f;
        description = "Your fast metabolism makes everybody else seem to be slowed down by 70% for 3s. \n 2.5s Cooldown";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("SlowDown2");
        active = true;
    }
}

public class SlowDown3 : SlowDown
{
    // Use this for initialization
    public override void Start()
    {
        title = "Chronophobia";
        time = 4;
        dilationFactor = 0.2f;
        cooldown = 3;
        description = "You turned everybody into turtles. 4s where everybody else slows down by 80%. \n 3s Cooldown";
        price = 25;
        level = 3;
        image = Resources.Load<Sprite>("SlowDown3");
        active = true;
    }
}