using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroy : MonoBehaviour {

	public Vector3 destination;
	public float speed;

	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, destination, step);

		if (transform.position == destination) {
			Destroy (this);
		}
	}
}
