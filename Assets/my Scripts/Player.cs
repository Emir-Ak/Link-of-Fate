using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {
	[SerializeField]
	private Stat health;
	            
/*	private void Save(){
		SaveLoadManager.SavePlayer (this);
	}

	private void Load(){
		int[] loadedStats = SaveLoadManager.Loadplayer ();

		health = loadedStats [0];

	}*/

	private void Awake()
	{
		health.Initialize ();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.W)) {
			health.CurrentVal += 10;
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			health.CurrentVal -= 10;
		}
	
	}
}
