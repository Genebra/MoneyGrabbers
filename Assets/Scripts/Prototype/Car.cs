using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	public GameObject coinPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)	{
		GameObject obj = collision.gameObject;

		if (obj.CompareTag("Bag")) {
			//obj.GetComponent<Bag>().DropCoins();
            Destroy(collision.gameObject);
        }
		//else if (obj.CompareTag("Coin")) {
		//	Vector2 oldPos = obj.GetComponent<Transform>().position;
		//	BoxCollider2D spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BoxCollider2D>();
		//	Vector2 newPos;

		//	while (true) {
		//		newPos = new Vector2(Random.Range(oldPos.x - 1, oldPos.x + 1), Random.Range(oldPos.y - 1, oldPos.y + 1));
		//		if(spawn.bounds.Contains(newPos)) {
		//			obj.transform.position = newPos;
		//			return;
		//		}	
		//	}
		//}

		//// Get hit by coin
		//if (obj.CompareTag("Bag")) {
		//	Vector2 pos = obj.GetComponent<Transform>().position;
		//	Destroy(obj);
		//	BoxCollider2D spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<BoxCollider2D>();

		//	for (int i = 0; i < 2; i++) {
		//		Vector2 position = new Vector2(Random.Range(pos.x - 1, pos.x + 1), Random.Range(pos.y - 1, pos.y + 1));
		//		if (spawn.bounds.Contains(position)) {
		//			Instantiate(coinPrefab, position, Quaternion.identity);
		//		}
		//		else {
		//			i--;
		//		}
		//	}

		//	//obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		//}

	}

}
