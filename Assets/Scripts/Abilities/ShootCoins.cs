using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCoins : Ability
{

	private Vector3 A = new Vector3(1.0f, 1.0f);
	private Vector3 B = new Vector3(1.0f, -1.0f);
	private Vector3 C = new Vector3(-1.0f, -1.0f);
	private Vector3 D = new Vector3(-1.0f, 1.0f);

	// Use this for initialization
	public override void Start()
    {
        description = "throw coins";
        price = 0;
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override bool doAction(PlayerMovement player)
    {
        float H = player.hAxis;
        float V = player.vAxis;
        player.coins -= player.coinsSpent;
        player.nextFire = Time.time + player.fireRate;
        GameObject obj = (GameObject)Instantiate(player.bag, transform.position + new Vector3(H, V, 0.0f).normalized, transform.rotation);



		Vector3 dir = new Vector3(H, V);

		// check if direction is between A and B (right)
		if (Vector3.Dot(Vector3.Cross(A, dir), Vector3.Cross(A, B)) >= 0 &&
			Vector3.Dot(Vector3.Cross(B, dir), Vector3.Cross(B, A)) >= 0)
		{
			obj.transform.Rotate(Vector3.forward, 90);
		}
		// check if direction is between C and D (left)
		else if (Vector3.Dot(Vector3.Cross(C, dir), Vector3.Cross(C, D)) >= 0 &&
				Vector3.Dot(Vector3.Cross(D, dir), Vector3.Cross(D, C)) >= 0)
		{
			obj.transform.Rotate(Vector3.forward, -90);
		}
		// check if direction is between D and A (up)
		else if (Vector3.Dot(Vector3.Cross(D, dir), Vector3.Cross(D, A)) >= 0 &&
				Vector3.Dot(Vector3.Cross(A, dir), Vector3.Cross(A, D)) >= 0)
		{
			obj.transform.Rotate(Vector3.forward, 180);
		}


		obj.gameObject.GetComponent<Transform>().localScale += new Vector3(0.25f, 0.25f, 0.0f);
        obj.gameObject.GetComponent<Bag>().numCoins = player.coinsSpent;
		obj.gameObject.GetComponent<Bag>().owner = player.nPlayer;
		obj.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(H, V).normalized * player.projectileSpeed, ForceMode2D.Impulse);
        return false;
    }

    public override bool checksCondition(PlayerMovement player)
    {
        if (player.coins >= 2 && Time.time > player.nextFire) return true;
        else return false;
    }
    
}
