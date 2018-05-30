using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Ability
{
    public bool started = false;
    public float range;
    public float velocity;
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
            timer = time;
            started = true;
            available = true;
            return true;
        }
        else
        {
            if (timer > 0)
            {
                foreach (var coin in GameObject.FindGameObjectsWithTag("Coin"))
                {
                    if(Vector3.Distance(player.gameObject.transform.position, coin.gameObject.transform.position) <= range)
                    {
                        makeCoinCloser(player, coin.gameObject.GetComponent<Coin>());
                    }
                }

                timer -= Time.deltaTime;
                return true;
            }
            else
            {
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

    public void makeCoinCloser(PlayerMovement player, Coin coin)
    {
        float dX = player.gameObject.transform.position.x - coin.gameObject.transform.position.x;
        float dY = player.gameObject.transform.position.y - coin.gameObject.transform.position.y;

        coin.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        coin.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dX, dY).normalized * velocity, ForceMode2D.Impulse);

    }
}

public class Magnet1 : Magnet
{
    // Use this for initialization
    public override void Start()
    {
        title = "Eletric Magnetic";
        time = 1.5f;
        velocity = 0.1f;
        range = 2;
        cooldown = 2;
        description = "Like a true X-Men all coins in a small range are atracted to you for 1.5s.\n 2s Cooldown";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("Magnet1");
        active = true;
    }
}

public class Magnet2 : Magnet
{
    // Use this for initialization
    public override void Start()
    {
        title = "Eletric Magnetic";
        time = 2.5f;
        velocity = 0.2f;
        range = 5;
        cooldown = 2;
        description = "When coins start worshipping someone, they'll worship you. 2.5s of pure coin attractiveness.\n 2s Cooldown";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("Magnet2");
        active = true;
    }
}

public class Magnet3 : Magnet
{
    // Use this for initialization
    public override void Start()
    {
        title = "Eletric Magnetic";
        time = 4;
        velocity = 0.5f;
        range = 10;
        cooldown = 3;
        description = "You are the sun to the coins' sea, their ying to your yang, for 4s they'll be all over you.\n 3s Cooldown";
        price = 25;
        level = 3;
        image = Resources.Load<Sprite>("Magnet3");
        active = true;
    }
}