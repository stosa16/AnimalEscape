using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutroScript : MonoBehaviour {

    public Button retry;
    public Button endgame;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("saved_dogs"))
            gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt("saved_dogs").ToString();
        else
            gameObject.GetComponent<Text>().text = "0";

        InvokeRepeating("ShowButtons", 1.0f, 10.0f);


    }
	
	public void EndGameClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowButtons()
    {
        retry.gameObject.SetActive(true);
        endgame.gameObject.SetActive(true);

    }
}
