using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organizeGameSpaces : MonoBehaviour {

	public List<GameObject> spaces;

	// Use this for initialization
	void Start () {
//		spaces = new List<GameObject> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void placement(List<GameObject> tileSet){
		Vector3 pos;
		int i = 0;
		int side = 0;

		foreach (GameObject tile in tileSet) {
			Debug.Log ("bloo");
			if (side == 0 || side == 2) {
				
				tile.transform.position = new Vector3 (i,0,0);
			}
			i++;
		}
	}
}
