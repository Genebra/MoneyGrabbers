using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

    public string description;
    public string title;
    public int price;
    public Sprite image;
    public bool active;
    public int level;

    // Use this for initialization
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract bool doAction(PlayerMovement player);

    public abstract bool checksCondition(PlayerMovement player);
}

public class EmptyActive : Ability
{ 

    // Use this for initialization
    public override void Start()
    {
        description = "";
        title = "Empty";
        price = 0;
        level = 0;
        active = true;
        image = Resources.Load<Sprite>("EmptyActive");
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override bool doAction(PlayerMovement player)
    {
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        return true;
    }
}

public class EmptyPassive : Ability
{
    // Use this for initialization
    public override void Start()
    {
        description = "";
        title = "Empty";
        price = 0;
        level = 0;
        active = false;
        image = Resources.Load<Sprite>("EmptyPassive");
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override bool doAction(PlayerMovement player)
    {
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        return true;
    }
}