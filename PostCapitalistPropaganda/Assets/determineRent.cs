using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class determineRent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int rentCalc(realEstate property){
		int houses = property.tile.houses;
		int hotels = property.tile.hotels;
		int rent = property.tile.rent;
		int price = property.tile.price;
		if (houses == 0 && hotels == 0) {
			return rent;
		} else {
			if (hotels == 1) {
				//how much is property with hotels
				rent = ((price / 2) - 20) * 5 + 600;
				return rent;
			} else if (houses == 4) {
				//how much is property with 4 hosues
				rent = ((price / 2) - 20) * 7 + 270;
				return rent;
			} else if (houses == 3) {
				rent = ((price / 2) - 20) * 6 + 140;
				return rent;
			} else if (houses == 2) {
				rent = ((price / 2) - 20) * 3;
				return rent;
			} else if (houses == 1) {
				rent = (price / 2) - 20;
				return rent;
			} else {
				return rent;
			}
		}
	}
}
