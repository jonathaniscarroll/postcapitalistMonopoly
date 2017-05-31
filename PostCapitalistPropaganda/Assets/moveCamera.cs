using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {

	public int step;
	private Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			if (transform.position != target.position) {
				transform.position = Vector3.MoveTowards (transform.position, target.position, step);
			}
		}
	}

	public void move(GameObject player){
		//if this is the first time being moved, get the list of game tiles from statemachine
		target = player.transform;
	}
}
