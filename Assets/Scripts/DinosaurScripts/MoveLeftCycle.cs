using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCycle : MonoBehaviour {

	[SerializeField]
	protected float moveSpeed = 3f;
	[SerializeField]
	float leftWayPointX = -20f, rightWayPointX = 30f;

	// Update is called once per frame
	virtual protected void Update () {
		transform.position = new Vector2 (transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

        if (transform.position.x < leftWayPointX)   
            transform.position = new Vector2 (rightWayPointX, transform.position.y);
	}
}
