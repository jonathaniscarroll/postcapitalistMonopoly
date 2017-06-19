using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class propertyUpgrade : MonoBehaviour {

	public GameObject upgradeButton;
	public Canvas canvas;
	public GameObject buyUpgradePanel;
	public Dropdown field;
	public GameObject house;
	public GameObject hotel;

	private int pos;
	public static List<Color> ownedColors;

	void Start(){
//		buyUpgradePanel.SetActive (false);
		ownedColors = new List<Color> ();
	}

	public void upgrade(Color category){
		
		if (upgradeButton.activeInHierarchy == false) {
			upgradeButton.SetActive (true);
		}
		ownedColors.Add (category);
		Debug.Log (category + " " + ownedColors.Count);
	}

	public void upgradePanel(){
		Debug.Log (ownedColors.Count);
		organizeGameSpaces tiles = GetComponent<organizeGameSpaces> ();
//		Dictionary<Color,int> colorDict = tiles.colorList;
		List<GameObject> tileList = tiles.spaces;
		buyUpgradePanel.SetActive (true);
//		Dropdown field = buyUpgradePanel.transform.FindChild("selectProperty").GetComponent<Dropdown>();
		List<string> options = new List<string>();
		List<GameObject> propertySpaces = new List<GameObject> ();
		int i = 0;
		field.ClearOptions ();
		foreach (GameObject property in tileList) {
			realEstate real = property.GetComponent<realEstate> ();
			if (real != null) {
				if (ownedColors.Contains(real.tile.category)) {
					//now we need to fill a list with the properties available to buy houses for
					if(real.tile.houses > i){
						propertySpaces.Clear ();
						propertySpaces.Add (property);
					} else if(real.tile.houses == i){
						propertySpaces.Add (property);
					}
//					options.Add(property.name);
//					propertySpaces.Add (property);
					Debug.Log ("name: " + property.name);
					//TO DO: figure out the effin drop down add values??? Maybe switch it for series of button, gawd

				}
			}

		}
		field.AddOptions (options);
		buyHouses(propertySpaces[Random.Range(0,propertySpaces.Count)]);
//		field.AddOptions (new List<Dropdown.OptionData>());
//		field.RefreshShownValue ();
		destroyButtons ();
	}

	public void buyHouses(GameObject property){
		realEstate realestate = property.GetComponent<realEstate> ();
		movePlayer player = realestate.tile.owner.GetComponent<movePlayer> ();
		//find out if there's any houses on the property
		if (realestate.tile.houses == 4) {
			Debug.Log ("hotel");
			//delete all children houses of that tile
			foreach (Transform child in property.transform) {
				Debug.Log (child.gameObject.name);
				if (child.gameObject.tag == "house") {
					Destroy (child.gameObject);
				}
			}
			GameObject thisHotel = (GameObject)Instantiate (hotel, property.transform.position + new Vector3 ((Random.Range(0.1f,3)/10f)-0.3f, 1, (Random.Range(0,10)/10f)), Quaternion.identity, property.transform);
			realestate.tile.hotels++;
			realestate.tile.houses = 0;
			player.player.money -= realestate.tile.housePrice;
			GetComponent<setPlayerInfo> ().setInfo (player);
		} else if(realestate.tile.hotels == 1){
			//the property is full of houses, etc
			Debug.Log ("no more hotels, houses");
		}
		else {
			GameObject thisHouse = (GameObject)Instantiate (house, property.transform.position + new Vector3 ((Random.Range(0.1f,3)/10f)-0.3f, 1, (Random.Range(0,10)/10f)), Quaternion.identity, property.transform);
			realestate.tile.houses++;
			player.player.money -= realestate.tile.housePrice;
			Debug.Log ("house");
		}

		realestate.updateRent (GetComponent<determineRent>());
	}

	public void destroyButtons(){
//		Debug.Log ("END TURN");
		ownedColors.Clear ();
		upgradeButton.SetActive (false);
		buyUpgradePanel.SetActive (false);
	}

	public void buyUpgrade(){

	}
}
