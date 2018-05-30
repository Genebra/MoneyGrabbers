using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceMan : MonoBehaviour {

    public float speed;
    public bool[] whoBribed; 

    private Rigidbody2D rb;

	private Animator animator;
	private string currentAnimation;

	private Vector3 A;
	private Vector3 B;
	private Vector3 C;
	private Vector3 D;

    public bool slowedDown = false;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent("Rigidbody2D") as Rigidbody2D;
		animator = gameObject.GetComponent<Animator>();

        whoBribed = new bool[4];
    }
	
	// Update is called once per frame
	void Update () {
        ChooseTarget();
		currentAnimation = "Idle";

		A = new Vector3(1.0f, 1.0f);
		B = new Vector3(1.0f, -1.0f);
		C = new Vector3(-1.0f, -1.0f);
		D = new Vector3(-1.0f, 1.0f);
	}

    void ChooseTarget() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float distance = Mathf.Infinity;
        float newDistance;
        GameObject closestPlayer = null;

        foreach (var player in players)
        {
            if (!whoBribed[player.gameObject.GetComponent<PlayerMovement>().nPlayer - 1]) {
                newDistance = Vector2.Distance(transform.position, player.transform.position);
                bool arrested = player.gameObject.GetComponent<PlayerMovement>().IsArrested();

                if (newDistance < distance && !arrested)
                {
                    closestPlayer = player;
                    distance = newDistance;
                }
            }

        }

        if (closestPlayer == null) {
			SetAnimation(Vector3.zero);
			return;
		}
		

        Vector2 direction = closestPlayer.transform.position - transform.position;
        direction.Normalize();

		SetAnimation(direction);

        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

	private void SetAnimation(Vector3 dir) {
		
		if (dir == Vector3.zero) {
			animator.ResetTrigger(currentAnimation);
			currentAnimation = "Idle";
			animator.SetTrigger(currentAnimation);
			GetComponent<SpriteRenderer>().flipX = false;
		}
		
		else {
			// check if direction is between A and B (right)
			if (Vector3.Dot(Vector3.Cross(A, dir), Vector3.Cross(A, B)) >= 0 &&
				Vector3.Dot(Vector3.Cross(B, dir), Vector3.Cross(B, A)) >= 0)
			{
				animator.ResetTrigger(currentAnimation);
				currentAnimation = "MoveLeft";
				animator.SetTrigger(currentAnimation);
				GetComponent<SpriteRenderer>().flipX = true;
			}
			// check if direction is between C and D (left)
			else if (Vector3.Dot(Vector3.Cross(C, dir), Vector3.Cross(C, D)) >= 0 &&
					Vector3.Dot(Vector3.Cross(D, dir), Vector3.Cross(D, C)) >= 0)
			{
				animator.ResetTrigger(currentAnimation);
				currentAnimation = "MoveLeft";
				animator.SetTrigger(currentAnimation);
				GetComponent<SpriteRenderer>().flipX = false;
			}
			// check if direction is between D and A (up)
			else if (Vector3.Dot(Vector3.Cross(D, dir), Vector3.Cross(D, A)) >= 0 &&
					Vector3.Dot(Vector3.Cross(A, dir), Vector3.Cross(A, D)) >= 0)
			{
				animator.ResetTrigger(currentAnimation);
				currentAnimation = "MoveUp";
				animator.SetTrigger(currentAnimation);
				GetComponent<SpriteRenderer>().flipX = false;
			}
			// check if direction is between B and C (down)
			else if (Vector3.Dot(Vector3.Cross(B, dir), Vector3.Cross(B, C)) >= 0 &&
					Vector3.Dot(Vector3.Cross(C, dir), Vector3.Cross(C, B)) >= 0)
			{
				animator.ResetTrigger(currentAnimation);
				currentAnimation = "MoveDown";
				animator.SetTrigger(currentAnimation);
				GetComponent<SpriteRenderer>().flipX = false;
			}
		}

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Grab player
        if (collision.gameObject.CompareTag("Player"))
        {
            bool arrested = collision.gameObject.GetComponent<PlayerMovement>().IsArrested();
            if (!arrested)
            {
                collision.gameObject.GetComponent<PlayerMovement>().Arrest();
            }
        }
    }
}
