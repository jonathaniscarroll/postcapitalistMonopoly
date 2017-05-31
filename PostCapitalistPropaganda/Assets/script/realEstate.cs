using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class realEstate : MonoBehaviour {

	public class PropertyTile {
		public string name;
		public int price;
		public int rent;
		public int houses;
		public int hotels;
		public GameObject owner;

		public PropertyTile(string thisName, int thisPrice, int thisRent){
			name = thisName;
			price = thisPrice;
			rent = thisRent;
		}
	}

	public string tileName;
	public int tilePrice;
	public int tileRent;

	public PropertyTile tile;

	// Use this for initialization
	void Awake () {
		tile = new PropertyTile (tileName, tilePrice, tileRent);

		Component[] tileText = GetComponentsInChildren (typeof(TextMesh));
		foreach(TextMesh txt in tileText){
			if(txt.gameObject.name == "Name"){
				txt.text = tile.name;
				gameObject.name = tile.name;
			}
			if(txt.gameObject.name == "Price"){
				txt.text = tile.price.ToString();
			}
			if(txt.gameObject.name == "Rent"){
				txt.text = tile.rent.ToString();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
