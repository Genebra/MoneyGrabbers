using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBuffs : Ability
{
    public float distance;
    public float angle;
    public float damage;

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
        player.meleeAngle *= angle;
        player.meleeDistance *= distance;
        player.meleeDamage *= damage;
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        return true;
    }

}

public class MeleeBuff1 : MeleeBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Brawler";
        angle = 2.0f;
        distance = 1.35f;
        damage = 1.25f;
        description = "Increases the angle by " + (int)(angle * 100) + "%, the distance by " + (int)(distance * 100) + " and the damage by " + (int)(damage * 100) + "%";
        price = 5;
        level = 1;
        image = Resources.Load<Sprite>("Melee1");
        active = false;
    }
}

public class MeleeBuff2 : MeleeBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Brawler";
        angle = 4.0f;
        distance = 1.5f;
        damage = 1.75f;
        description = "Increases the angle by " + (int)(angle * 100) + "%, the distance by " + (int)(distance * 100) + " and the damage by " + (int)(damage * 100) + "%";
        price = 10;
        level = 2;
        image = Resources.Load<Sprite>("Melee2");
        active = false;
    }
}

public class MeleeBuff3 : MeleeBuffs
{
    // Use this for initialization
    public override void Start()
    {
        title = "Brawler";
        angle = 10.0f;
        distance = 2.0f;
        damage = 2.0f;
        description = "Increases the angle by " + (int)(angle * 100) + "%, the distance by " + (int)(distance * 100) + " and the damage by " + (int)(damage * 100) + "%";
        price = 15;
        level = 3;
        image = Resources.Load<Sprite>("Melee3");
        active = false;
    }
}

