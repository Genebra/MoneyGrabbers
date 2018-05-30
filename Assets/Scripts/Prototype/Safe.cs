using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject obj = collision.gameObject;
		if (obj.CompareTag("Coin"))
		{
			Vector2 oldPos = obj.GetComponent<Transform>().position;
			Vector2 newPos = new Vector2(Random.Range(oldPos.x - 1, oldPos.x + 1), Random.Range(oldPos.y - 1, oldPos.y + 1));
			obj.transform.position = newPos;
		}
	}
}
