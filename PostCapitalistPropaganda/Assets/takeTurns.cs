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

	public void Turn(){

		setPlayerInfo info = GetComponent<setPlayerInfo> ();

		if (playerNumber == 0) {
			playerNumber = playerList.Count;

		}
//		Debug.Log (playerNumber);
		//determine the current player 
		GameObject currentPlayer = playerList[currentTurn];
		movePlayer moveP = currentPlayer.GetComponent<movePlayer> ();
		int diceRoll = Random.Range (1, 12);
		int currentTile;
		GameObject.Find ("dice text").GetComponent<Text>().text = "Dice Roll: " + diceRoll;
		//determine if the player is passing Go/making a round of the board, & move player
		if (moveP.player.position + diceRoll > tileSet.Count) {
			currentTile = moveP.player.position + diceRoll - tileSet.Count;
			moveP.move (currentTile);
			moveP.player.position = currentTile;
		} else {
			currentTile = moveP.player.position + diceRoll;
			moveP.move (currentTile);
			moveP.player.position = currentTile;
		}
		camera.GetComponent<moveCamera> ().move (currentPlayer);

		if (currentTurn == playerNumber - 1) {
			currentTurn = 0;
		} else currentTurn++;

		turnButton.SetActive (false);
		info.setInfo(moveP);
		turnAction (tileSet[currentTile], moveP);
	}

	private void turnAction(GameObject thisTile,movePlayer thisPlayer){

		setPlayerInfo info = GetComponent<setPlayerInfo> ();

		//is it a property tile or a game mechanic tile?
		if(thisTile.GetComponent<realEstate>() != null){
			realEstate property = thisTile.GetComponent<realEstate> ();
			Debug.Log ("real estate tile");

			//is it owned by a player?
			if(property.tile.owner != null){
				
				//is it owned by the player?
				if (thisPlayer.gameObject == property.tile.owner) {
					//the player chooses to buy or no

				}

				else {
					//the player chooses to pay rent or no

				}
				info.setInfo(thisPlayer);

				turnButton.SetActive (true);

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
			turnButton.SetActive (true);
			info.setInfo(thisPlayer);
		}
//		turnButton.SetActive (true);
	}

	public void buyTile(bool buy){
		setPlayerInfo info = GetComponent<setPlayerInfo> ();
		if (buy == true) {
			//can player afford it
			if (_playerBuys.player.money >= _propBuys.GetComponent<realEstate> ().tile.price) {
				
				_propBuys.GetComponent<realEstate> ().tile.owner = _playerBuys.gameObject;
				_playerBuys.player.money -= _propBuys.GetComponent<realEstate> ().tile.price;
				buyButton.SetActive (false);
			} else {
				buyButton.SetActive (false);
			}
		} else {
			buyButton.SetActive (false);
		}

		turnButton.SetActive (true);
		info.setInfo(_playerBuys);
	}

	public void endTurn(){

	}

}
