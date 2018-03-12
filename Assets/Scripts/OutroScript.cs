using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("saved_dogs"))
            gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("saved_dogs").ToString();
        else
            gameObject.GetComponent<Text>().text = "0";


    }
	
	public void EndGameClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
