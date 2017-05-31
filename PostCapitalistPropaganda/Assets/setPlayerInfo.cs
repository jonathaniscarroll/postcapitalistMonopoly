using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setPlayerInfo : MonoBehaviour {

	public GameObject playerInfoPanel;

	private Text info;

	// Use this for initialization
	void Start () {
		info = playerInfoPanel.GetComponent<Text> ();
	}
	
	public void setInfo(movePlayer currentPlayer){
		info.text = "Player: " + currentPlayer.gameObject.name + "\nMoney: " + currentPlayer.player.money;
		Debug.Log ("hey");
	}
}
