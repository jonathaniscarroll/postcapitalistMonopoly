using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setPlayerInfo : MonoBehaviour {

	public GameObject playerInfoPanel;
	public GameObject rentInfoText;

	private propertyUpgrade propUp;

	private Text info;

	private Dictionary<Color,int> GameTileColors;

	// Use this for initialization
	void Start () {
		info = playerInfoPanel.GetComponent<Text> ();
		GameTileColors = new Dictionary<Color,int> ();

		propUp = GetComponent<propertyUpgrade> ();
	}
	
	public void setInfo(movePlayer currentPlayer){
		GameTileColors = GetComponent<organizeGameSpaces>().colorList;
		string ownings = "";
		//create a dictionary of the categories of properties the player owns
		Dictionary<Color,int> playerCategories = new Dictionary<Color,int>();

		foreach (GameObject prop in currentPlayer.player.owned) {
//			ownings += prop.name + "\n";
			realEstate real = prop.GetComponent<realEstate> ();
			int posY = 0;
			//this is for determining whether user can buy hotels
			if (playerCategories.ContainsKey (real.tile.category)) {
				playerCategories [real.tile.category]++;
//				Debug.Log (currentPlayer);
				if(playerCategories [real.tile.category] == GameTileColors[real.tile.category]){
//					Debug.Log ("buy a hotel");
					propUp.upgrade (real.tile.category);
				}
			} else {
				playerCategories.Add (real.tile.category,1);
			}

			ownings += prop.name + "\n";
		}

		info.text = "Player: " + currentPlayer.gameObject.name + "\nMoney: " + currentPlayer.player.money + "\n" + ownings;
	}

	public void rentInfo(GameObject tile,movePlayer player){
//		Debug.Log ("RENTING");
		realEstate prop = tile.GetComponent<realEstate> ();
		Text txt = rentInfoText.GetComponent<Text> ();
		txt.text = "Would you like to pay rent to " + prop.tile.owner + "\nAt: " + prop.tile.name + "\nFor: " + prop.tile.rent;
	}
}
