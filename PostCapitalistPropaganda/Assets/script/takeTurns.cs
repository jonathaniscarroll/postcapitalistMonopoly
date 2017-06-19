using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class takeTurns : MonoBehaviour {

	public static int currentTurn;
	public static int currentRound;
	private int playerNumber;

	//get the dictionary of players
	private addPlayers addplayers;
	private Dictionary<string,GameObject> playerDict;

	//creating a list from that dictionary
	public List<GameObject> playerList;

	//get the list of game tiles
	private organizeGameSpaces tiles;
	private List<GameObject> tileSet;

	public GameObject camera;

	public GameObject turnButton;
	public GameObject buyButton;
	public GameObject buyButtonInfo;
	public GameObject rentButton;
	public GameObject endTurnButton;

	private movePlayer _playerBuys;
	private GameObject _propBuys;

	// Use this for initialization
	void Start () {

		playerList = new List<GameObject> ();

		//create instance of the player dictionary, from other scripts
		addplayers = GetComponent<addPlayers>();
		playerDict = new Dictionary<string,GameObject> ();

		//grab players from dicitonary and put name strings into list

		tiles = GetComponent<organizeGameSpaces>();
		tileSet = new List<GameObject> ();
		tileSet = tiles.spaces;

		currentTurn = 0;
		currentRound = 0;
	}

	//The Turn function is first triggered after the players are create in addPlayers.cs, and subsequently is 
	//activated by the endTurn function.
	public void Turn(){
		setPlayerInfo info = GetComponent<setPlayerInfo> ();
		if (playerNumber == 0) {
			playerNumber = playerList.Count;
		}
		//determine the current player 
		GameObject currentPlayer = playerList[currentTurn];
		camera.GetComponent<moveCamera> ().move (currentPlayer);
		_playerBuys = currentPlayer.GetComponent<movePlayer> ();
		if (currentTurn == playerNumber - 1) {
			currentTurn = 0;
		} else currentTurn++;
		turnButton.SetActive (true);
		info.setInfo(_playerBuys);

	}

	//rollDice is triggered by the Roll Dice button, referred to in this script as turnButton.
	//turnButton is made active after the turn is initiated in the Turn() function
	public void rollDice(){
		int diceRoll = Random.Range (1, 12);
		int currentTile;
		if (_playerBuys.player.position + diceRoll > tileSet.Count) {
			currentTile = _playerBuys.player.position + diceRoll - tileSet.Count;
			_playerBuys.move (currentTile);
			_playerBuys.player.position = currentTile;
		} else {
			currentTile = _playerBuys.player.position + diceRoll;
			_playerBuys.move (currentTile);
			_playerBuys.player.position = currentTile;
		}
		turnAction (tileSet[currentTile], _playerBuys);
		GameObject.Find ("dice text").GetComponent<Text>().text = "Dice Roll: " + diceRoll;
		turnButton.SetActive (false);
	}

	//this determines whether the player lands on a property or game mech tile. 
	//It plays after the dice is rolled, and triggers the end turn button
	private void turnAction(GameObject thisTile,movePlayer thisPlayer){
		setPlayerInfo info = GetComponent<setPlayerInfo> ();
		info.setInfo (thisPlayer);
		//is it a property tile or a game mechanic tile?
		if(thisTile.GetComponent<realEstate>() != null){
			realEstate property = thisTile.GetComponent<realEstate> ();
			Debug.Log ("real estate tile");

			//is it owned by a player?
			if(property.tile.owner != null){
				Debug.Log ("Someone Owns it");
				//is it owned by the player?
				if (thisPlayer.gameObject == property.tile.owner) {
					Debug.Log ("You Owns it");
				}
				else {
					//the player chooses to pay rent or no
					Debug.Log (thisPlayer.gameObject.name + " Owns it");
					rentButton.SetActive (true);
					_playerBuys = thisPlayer;
					_propBuys = thisTile;
					info.rentInfo(thisTile, thisPlayer);
				}
				info.setInfo(thisPlayer);
//				turnButton.SetActive (true);

			} else {
				//the player can buy the tile
				//set info for this specific tile
				buyButtonInfo.GetComponent<Text>().text = "Would you like to buy " + thisTile.name + "?\nPrice: " + property.tile.price + "\nRent: " + property.tile.rent;
				buyButton.SetActive(true);
				_playerBuys = thisPlayer;
				_propBuys = thisTile;
			}
		} 
		else if(thisTile.GetComponent<goTile>() != null){
			Debug.Log ("game mechanic tile");
			info.setInfo(thisPlayer);
		}
		endTurnButton.SetActive (true);
	}

	public void buyTile(bool buy){
		setPlayerInfo info = GetComponent<setPlayerInfo> ();
		realEstate realestate = _propBuys.GetComponent<realEstate> ();
		if (buy == true) {
			//can player afford it
			if (_playerBuys.player.money >= realestate.tile.price) {
				realestate.setOwner (_playerBuys);
				realestate.tile.owner = _playerBuys.gameObject;
				_playerBuys.player.money -= realestate.tile.price;
				_playerBuys.player.addProperty (_propBuys);
				buyButton.SetActive (false);
			} else {
				buyButton.SetActive (false);
			}
		} else {
			buyButton.SetActive (false);
		}
//		turnButton.SetActive (true);
		info.setInfo(_playerBuys);
	}

	public void payRent(bool rent){
		setPlayerInfo info = GetComponent<setPlayerInfo> ();
		realEstate realestate = _propBuys.GetComponent<realEstate> ();
		movePlayer owner = realestate.tile.owner.GetComponent<movePlayer> ();
		if (rent == true) {
			int rentNumber = GetComponent<determineRent>().rentCalc(realestate);
			_playerBuys.player.money -= realestate.tile.rent;
			owner.player.money += realestate.tile.rent;
			rentButton.SetActive (false);
			info.setInfo (_playerBuys);
		} else {
			rentButton.SetActive (false);
		}

	}

//	public void proposeTrade(){
//		Dictionary<Color,int> categoriesOwned;
//		foreach(GameObject property in _playerBuys.player.owned){
//			realEstate realestate = property.GetComponent<realEstate> ();
// 			if (categoriesOwned.ContainsKey (realestate.tile.category)) {
//				categoriesOwned [realestate.tile.category]++;
//			} else {
//				categoriesOwned.Add (realestate.tile.category, 1);
//			}
//		}
//		//TODO prioritize what property you want to trade, based on if any multiples are owned within a color category.
//		//the AI will propose a trade every turn
//		//They select the highest priority category they want to buy from, and see if any other player has property in that category
//		//iterate through list from highest priority to lowest priority, looking to see if other players own property in that category.
//		//propose a trade, with details sent to the target player's moveplayer.player class,
//		//and on Turn() function it verifies if there is any wanting trades, and processes whether it accepts the terms.
//		//can either trade property or cash
//		//maybe i should do the prioritizing in the AI brain script
//	}

	public void endTurn(){
		_playerBuys = null;
		_propBuys = null;
		endTurnButton.SetActive (false);
		turnButton.SetActive (true);
		Turn ();
	}

}
