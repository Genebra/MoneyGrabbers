using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour {

	public int numCoins;
	public GameObject Coins;
	public int owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DropCoins() {
		Vector2 myPos = gameObject.GetComponent<Transform>().position;

		for (int i = 0; i < numCoins; i++) {
			Vector2 position = new Vector2(Random.Range(myPos.x - 0.25f, myPos.x + 0.25f), Random.Range(myPos.y - 0.25f, myPos.y + 0.25f));
			Instantiate(Coins, position, Quaternion.identity);
		}

		Destroy(gameObject);
	}
}
