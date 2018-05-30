using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Ability{

	private int angleOffset = 10;
	

    // Use this for initialization
    public override void Start () {
        
    }

    // Update is called once per frame
    public override void Update () {
		
	}

    public override bool doAction(PlayerMovement player) {
		// reset cooldown
		player.nextFire = Time.time + player.fireRate;

		// create vectors to raycast in a cone area
		Vector2 center = new Vector2(player.hAxis, player.vAxis).normalized;
		Vector2 dir = Quaternion.AngleAxis(- player.meleeAngle / 2, Vector3.forward) * center;
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);

		RaycastHit2D right = Physics2D.Raycast(pos + dir, dir, player.meleeDistance);
		if (right.collider != null && right.transform.tag == "Player") {
			right.collider.gameObject.GetComponent<PlayerMovement>().Hit();
			right.collider.gameObject.GetComponent<PlayerMovement>().MeleeDash(player.hAxis, player.vAxis);
		}
		else if (right.collider != null && right.transform.tag == "Chest" && player.openChest1Time == 0)
		{
            if (player.spawner.chestHit(player.meleeDamage))	player.openChest1Time = 1;
		}

		for (int current = 0; current < player.meleeAngle; current += angleOffset) {
			dir = Quaternion.AngleAxis(angleOffset, Vector3.forward) * dir;

			RaycastHit2D hit = Physics2D.Raycast(pos + dir, dir, player.meleeDistance);

			if (hit.collider != null && hit.transform.tag == "Player")
			{
				hit.collider.gameObject.GetComponent<PlayerMovement>().Hit();
				hit.collider.gameObject.GetComponent<PlayerMovement>().MeleeDash(player.hAxis, player.vAxis);
			}
			else if (hit.collider != null && hit.transform.tag == "Chest" && player.openChest1Time == 0)
			{
                if (player.spawner.chestHit(player.meleeDamage)) player.openChest1Time = 1;
            }
		}

        return false;
    }

    public override bool checksCondition(PlayerMovement player) {
		if (Time.time > player.nextFire)
		{
			return true;
		}
		else return false;
    }

}
