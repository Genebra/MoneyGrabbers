using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocker : Ability
{
    private int angleOffset = 10;

    public bool started = false;
    public float time;
    public float timer = 0;
    public float cooldown;
    public float cooldownTimer = 0;
    public bool available = true;

    RaycastHit2D hit;

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
            Vector2 center = new Vector2(player.hAxis, player.vAxis).normalized;
            Vector2 dir = Quaternion.AngleAxis(-player.shockAngle / 2, Vector3.forward) * center;
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);

            RaycastHit2D right = Physics2D.Raycast(pos + dir, dir, player.shockDistance);
            if (right.collider != null && right.transform.tag == "Player")
            {
                Debug.Log("Aqui1");
                right.collider.gameObject.GetComponent<PlayerMovement>().speed = 0;
                started = true;
                timer = time;
                available = true;
                return true;
            }

            for (int current = 0; current < player.shockAngle; current += angleOffset)
            {
                dir = Quaternion.AngleAxis(angleOffset, Vector3.forward) * dir;

                hit = Physics2D.Raycast(pos + dir, dir, player.shockDistance);

                if (hit.collider != null && hit.transform.tag == "Player")
                {
                    Debug.Log("Aqui2");
                    hit.collider.gameObject.GetComponent<PlayerMovement>().speed = 0;
                    started = true;
                    timer = time;
                    available = true;
                    return true;
                }
            }
            return false;
        }
        else
        {
            if (timer > 0)
            {
                hit.collider.gameObject.GetComponent<PlayerMovement>().speed = 0;
                timer -= Time.deltaTime;
                return true;
            }
            else
            {

                hit.collider.gameObject.GetComponent<PlayerMovement>().speed = 0;
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

public class Shocker1 : Shocker
{
    // Use this for initialization
    public override void Start()
    {
        title = "Paralyze";
        time = 2;
        cooldown = 2;
        description = "Do you want to be the on who can move? This is for you. 2s Cooldown";
        price = 5;
        level = 1;
        //image = Resources.Load<Sprite>("SpeedBoost1");
        active = true;
    }
}

public class Shocker2 : Shocker
{
    // Use this for initialization
    public override void Start()
    {
        title = "Paralyze";
        time = 4;
        cooldown = 3;
        description = "Do you want to be the on who can move? This is for you. 3s Cooldown";
        price = 10;
        level = 2;
        //image = Resources.Load<Sprite>("SpeedBoost2");
        active = true;
    }
}

public class Shocker3 : Shocker
{
    // Use this for initialization
    public override void Start()
    {
        title = "Paralyze";
        time = 5;
        cooldown = 4;
        description = "Do you want to be the on who can move? This is for you. 3s Cooldown";
        price = 25;
        level = 3;
        //image = Resources.Load<Sprite>("SpeedBoost3");
        active = true;
    }
}