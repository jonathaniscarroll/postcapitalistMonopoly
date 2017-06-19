using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movePlayer : MonoBehaviour {

	private organizeGameSpaces gameTiles;
	private GameObject stateMachine;
	private List<GameObject> tiles;

	public class playerStats
	{
		public int money;
		public int position;
		public List<GameObject> owned;

		public playerStats(int mon,int pos){
			money = mon;
			position = pos;
		}

		public void setMoney(int cash)
		{
			money = cash;

		}
		public void setPosition(int pos)
		{
			position = pos;
		}
		public void addProperty (GameObject prop){
			owned.Add (prop);
		}
	}

	private NavMeshAgent m_Agent;
	public playerStats player;

	// Use this for initialization
	void Start () {
		player = new playerStats (15000,0);
		player.owned = new List<GameObject> ();
//		Debug.Log (player.position);
	}
	

	//move command has a stored list of the game tiles, is sent which tile the player is moving to
	public void move(int destination){
		//if this is the first time being moved, get the list of game tiles from statemachine
		if (tiles == null) {
			m_Agent = GetComponent<NavMeshAgent> ();
			stateMachine = GameObject.Find ("gameRun");
			gameTiles = stateMachine.GetComponent<organizeGameSpaces>();
			tiles = new List<GameObject> ();
//			Debug.Log ("loading tileset");
			tiles = gameTiles.spaces;
//			Debug.Log (tiles);
		}
//		Debug.Log (tiles[destination].transform.position);
		m_Agent.destination = tiles[destination].transform.position + new Vector3(0,0.5f,0);
	}
}
