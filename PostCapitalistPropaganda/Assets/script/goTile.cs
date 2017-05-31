using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class goTile : MonoBehaviour {

	public string tileName;

	// Use this for initialization
	void Awake () {

		Component[] tileText = GetComponentsInChildren (typeof(TextMesh));
		foreach (TextMesh txt in tileText) {
			if (txt.gameObject.name == "Name") {
				txt.text = tileName;
				gameObject.name = tileName;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
