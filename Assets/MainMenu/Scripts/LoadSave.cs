using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadSave : MonoBehaviour {

	public GameConfig gameConfig;																// ERGH NOT EVERYTHING IS REALLY AS IT SHOULD BE HERE

	// Use this for initialization
	void Start () {
		loadConfig ();
		Debug.Log (gameConfig.resulationId);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Saves settings to JSON file
	public void saveConfig(){
		string jsonData = JsonUtility.ToJson(gameConfig, true);									// Converts the class to the json file
		File.WriteAllText (Application.persistentDataPath + "/gamesettings.json", jsonData);	// Save the jsonData (class _gameConfig) in persistenceData
	}// END

	// Load settings JSON file
	public void loadConfig(){
		// Load the json file in the path where it is located and store it in the variable _gameConfig
		gameConfig = JsonUtility.FromJson<GameConfig> (File.ReadAllText (Application.persistentDataPath + "/gamesettings.json"));

	}// END


}
