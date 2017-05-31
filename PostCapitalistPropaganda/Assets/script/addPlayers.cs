using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPlayers : MonoBehaviour {
	
	public Dictionary<string,GameObject> players;

	private Dictionary<string,GameObject> playerDic;

	public GameObject user;

	private takeTurns turns;

	// Use this for initialization
	void Start () {
		players = new Dictionary<string,GameObject> ();
		turns = GetComponent<takeTurns>();
	}
	
	public void populateList(string input){
		GameObject player;
		player = user;
		players.Add (input,player);
//		Debug.Log (input + ": " + players[input]);
	}

	public void createPlayers(){
		int pos = 0;
		foreach (KeyValuePair<string,GameObject> dude in players) {
//			Debug.Log (dude.Key + ": " + dude.Value);
			GameObject go = Instantiate (dude.Value, new Vector3 (pos, 2, 0), Quaternion.identity) as GameObject;
			go.name = dude.Key;
			Color newcol = new Color (((Random.Range (0, 4) * 25) / 100f), ((Random.Range (0, 4) * 25) / 100f), ((Random.Range (0, 4) * 25) / 100f), 1);
			go.GetComponent<MeshRenderer>().material.color = newcol;
			//move player to starting position
			go.GetComponent<movePlayer>().move(0);
			pos++;
			turns.playerList.Add (go);
		}
//		turns.populatePlayerDictionary (players);
		GameObject.Find ("PlayerPanel").SetActive (false);
		GameObject.Find ("TurnButton").SetActive (true);
	}
}
