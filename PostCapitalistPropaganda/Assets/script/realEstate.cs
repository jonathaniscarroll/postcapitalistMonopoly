using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class realEstate : MonoBehaviour {

	public class PropertyTile {
		public string name;
		public int price;
		public int rent;
		public Color category;
		public int houses;
		public int hotels;
		public GameObject owner;
		public int housePrice;

		public PropertyTile(string thisName, int thisPrice, int thisRent, Color thisColor,int thisHousePrice){
			name = thisName;
			price = thisPrice;
			rent = thisRent;
			category = thisColor;
			housePrice = thisHousePrice;
		}
	}

	public string tileName;
	public int tilePrice;
	public int tileRent;
	public Color tileColor;
	public int tileHousePrice;

	public PropertyTile tile;

	// Use this for initialization
	void Awake () {
		tile = new PropertyTile (tileName, tilePrice, tileRent, tileColor,tileHousePrice);
		GetComponent<MeshRenderer> ().material.color = tileColor;
		updateText ();
	}

	public void updateText(){
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
	public void setOwner(movePlayer buyer){
		GameObject ownerText = new GameObject ("Owner");
		ownerText.transform.parent = transform;
		TextMesh textM = ownerText.AddComponent<TextMesh> ();
		textM.text = buyer.gameObject.name;
		textM.characterSize = 0.02f;
		textM.fontSize = 65;
		textM.color = new Color (0,0,0);
		ownerText.transform.localPosition = new Vector3 (-0.452f,0.528f,-0.2f);
		ownerText.transform.rotation = Quaternion.Euler(90,0,0);
	}

	public void updateRent(determineRent calc){
		tile.rent = calc.rentCalc (this);
		updateText ();
	}
}
