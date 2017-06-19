using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simulateMonopoly : MonoBehaviour {

	public GameObject playerPanel;
	public GameObject simulationChoicePanel;

	private Lexic.NameGenerator namegen;


	// Use this for initialization
	void Start () {
		namegen = GetComponent<Lexic.NameGenerator> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void activateSimulation(bool activate){
		playerPanel.SetActive (false);
		Debug.Log (activate);
		if (activate) {
			addPlayers addplayers = GetComponent<addPlayers> ();
			for (int i = 0; i < Random.Range (2, 10); i++) {
				string name = namegen.GetNextRandomName ();
				addplayers.populateList (name);
				Debug.Log("adding player " + name);
			}
			addplayers.createPlayers ();

		} else {
			simulationChoicePanel.SetActive (false);
		}
	}

	void aiTurn(){

	}
}
