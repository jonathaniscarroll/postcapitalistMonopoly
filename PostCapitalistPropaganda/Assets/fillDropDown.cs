using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fillDropDown : MonoBehaviour {

	public Dropdown dd;
	private List<string> sf;

	// Use this for initialization
	void Start () {
		sf = new List<string> (){"pee","poo"};
		dd.ClearOptions ();
		dd.AddOptions (sf);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
