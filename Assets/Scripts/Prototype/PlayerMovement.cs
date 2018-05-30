using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public GameObject coin;
	public GameObject bag;
	Rigidbody2D rb;

    public int coins = 0;
    
    public float speed = 1.0f;
    public float speedDash = 0.5f;
	public float projectileSpeed = 15.0f;
    public int coinsSpent = 2;
    public float fireRate = 2.0f;
	public int dropNumCoins = 6;
    public float arrestMultiplier = 0.0f;
    public float multiplier = 1.0f;
    public float meleeAngle = 30;
    public float meleeDistance = 0.25f;
    public float meleeDamage = 10;
    public bool timeBasedY;
    public bool timeBasedB;

    public float shockAngle = 30;
    public float shockDistance = 1.35f;
    public bool slowedDown = false;

    public int nPlayer;

    public int openChest1Time = 0;

    public float hAxisDash;
    public float vAxisDash;

    // strings for the joystick buttons and axis
    public string horizontal;
	public string vertical;
	public string A_button;
	public string X_button;
    public string B_button;
    public string Y_button;

    public Ability abilityX;
    public Ability abilityY;
    public Ability abilityA;
    public Ability abilityB;

    public float nextFire;
	public bool meleeHit;
    public bool arrested = false;

	public float hAxis;
	public float vAxis;

    Timer timer;

	public Transform indicator;

    public Spawner spawner;

    bool timeStart;

	private Animator animator;
	private string currentAnimation;

	private Vector3 A = new Vector3(1.0f, 1.0f);
	private Vector3 B = new Vector3(1.0f, -1.0f);
	private Vector3 C = new Vector3(-1.0f, -1.0f);
	private Vector3 D = new Vector3(-1.0f, 1.0f);

    public bool escape = false;

    public bool shock = false;

	// Use this for initialization
	void Start()
	{
		rb = gameObject.GetComponent("Rigidbody2D") as Rigidbody2D;
        timer = GameObject.Find("CountDown").GetComponent<Timer>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();


        Manager m = GameObject.Find("Manager").GetComponent<Manager>();

        m.playerList[nPlayer - 1][0].doAction(this);
        m.playerList[nPlayer - 1][1].doAction(this);
        abilityY = m.playerList[nPlayer - 1][2];
        abilityB = m.playerList[nPlayer - 1][3];

        abilityX = gameObject.AddComponent<ShootCoins>();
        abilityA = gameObject.AddComponent<Melee>();

        animator = GetComponent<Animator>();
		currentAnimation = "Idle";

		A = new Vector3(1.0f, 1.0f);
		B = new Vector3(1.0f, -1.0f);
		C = new Vector3(-1.0f, -1.0f);
		D = new Vector3(-1.0f, 1.0f);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        timer = GameObject.Find("CountDown").GetComponent<Timer>();

        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();

        timeStart = timer.getFlagStart();

        if (timeStart)
        {
            if (!shock)
            {

                if (!arrested)
                {

                    if (meleeHit)
                    {
                        DropCoins();
                        rb.AddForce(new Vector2(hAxisDash * speedDash, vAxisDash * speedDash), ForceMode2D.Impulse);
                    }

                    // Get direction vectors
                    hAxis = Input.GetAxis(horizontal);
                    vAxis = Input.GetAxis(vertical);

                    // if there is movement to apply 
                    // then we have to check in what direction
                    if (hAxis != 0.0f || vAxis != 0.0f)
                    {

                        Vector3 dir = new Vector3(hAxis, vAxis);
                        indicator.rotation = Quaternion.LookRotation(Vector3.forward, -dir);

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


                    // Check if player wants to shoot, if it has enough coins and fire rate
                    if (Input.GetButtonDown(X_button))
                    {
                        if (abilityX.checksCondition(this))
                        {
							SIS.Instance.CoinTossed(nPlayer);
                            abilityX.doAction(this);
                            animator.SetTrigger("Throw");
                        }
                    }

                        // Check if player wants to melee
                        if (Input.GetButton(A_button))
                        {
                            if (abilityA.checksCondition(this))
                            {
								SIS.Instance.MeleeThrown(nPlayer);
                                abilityA.doAction(this);
                                rb.AddForce(new Vector2(hAxis * speedDash, vAxis * speedDash), ForceMode2D.Impulse);
                                animator.SetTrigger("Melee");
                            }
                        }

                        if (Input.GetButton(Y_button) || timeBasedY)
                        {
                            if (abilityY.checksCondition(this))
                            {
                                timeBasedY = abilityY.doAction(this);
                            }
                        }

                        if (Input.GetButton(B_button) || timeBasedB)
                        {
                            if (abilityB.checksCondition(this))
                            {
                                timeBasedB = abilityB.doAction(this);
                            }
                        }

                        rb.AddForce(new Vector2(hAxis * speed, vAxis * speed), ForceMode2D.Impulse);
                    }
                    else
                    {
                        animator.ResetTrigger(currentAnimation);
                        currentAnimation = "Idle";
                        animator.SetTrigger(currentAnimation);
                        GetComponent<SpriteRenderer>().flipX = false;
                        indicator.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                    }
                }
            }
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Catch coin
		if (collision.gameObject.CompareTag("Coin"))
		{
			Destroy(collision.gameObject);
			coins++;
			SIS.Instance.GrabbedCoin(nPlayer);
            //Spawner.scoreChange = true;
		}
		// Get hit by coin
		if (collision.gameObject.CompareTag("Bag"))
		{
			SIS.Instance.Hit(collision.gameObject.GetComponent<Bag>().owner);
			Destroy(collision.gameObject);
			DropCoins();
		}

        if (collision.gameObject.CompareTag("Barrier") )
        {
            gameObject.SetActive(false);
            GameObject.Find("Spawner").GetComponent<Spawner>().DecreaseActivePlayers();
            escape = true;
			SIS.Instance.RanAway(nPlayer);
        }
	}

	private void DropCoins() {

		for (int i = 0; i < dropNumCoins ; i++) {
			
			if (coins > 0) {
				Vector2 direction = Random.insideUnitCircle.normalized;
				Instantiate(coin, transform.position + (Vector3)direction, Quaternion.identity);
				coins--;
			} else {
				break;
			}
		}

		for (int i = 0; i < 2; i++) {
			Vector2 direction = Random.insideUnitCircle.normalized;
			Instantiate(coin, transform.position + (Vector3)direction, Quaternion.identity);
		}

		meleeHit = false;
	}

	public void Hit() {
		meleeHit = true;
	}

    public void Shock()
    {
        shock = true;
    }

    public void NotShock()
    {
        shock = false;
    }

    public bool IsArrested()
    {
        return arrested;
    }

    public bool isHit()
    {
        return meleeHit;
    }

    public bool IsEscaped()
    {
        return escape;
    }

    public void Arrest() {
        arrested = true;
		animator.ResetTrigger(currentAnimation);
		currentAnimation = "Idle";
		animator.SetTrigger(currentAnimation);
		GetComponent<SpriteRenderer>().flipX = false;
        GameObject.Find("Spawner").GetComponent<Spawner>().DecreaseActivePlayers();
    }

    public void MeleeDash(float h, float x)
    {
        hAxisDash = h;
        vAxisDash = x;
    }

    public int getCoins()
    {
        return coins;
    }
}
