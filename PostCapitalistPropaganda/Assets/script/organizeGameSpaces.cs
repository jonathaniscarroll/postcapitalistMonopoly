using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organizeGameSpaces : MonoBehaviour {

	public List<GameObject> spaces;
	public Dictionary<Color,int> colorList;

	// Use this for initialization
	void Start () {
//		spaces = new List<GameObject> ();
		colorList = new Dictionary<Color,int>();
		groupColors (spaces);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void groupColors(List<GameObject> tileSet){


		foreach (GameObject tile in tileSet) {
			if (tile.GetComponent<realEstate> () != null) {
				realEstate prop = tile.GetComponent<realEstate> ();
//				Debug.Log (prop.tile.category);
				//add colors to a list of how many properties for each color category
				if (colorList.ContainsKey(prop.tile.category)) {
					colorList [prop.tile.category] = colorList [prop.tile.category] + 1;
				} else {
					colorList.Add (prop.tile.category, 1);
				}
//				Debug.Log (prop.tile.category + ": " + colorList [prop.tile.category]);
			}

		}
	}
}
