﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameScript : MonoBehaviour {

    private void OnMouseDown()
    {
        //if()
        Debug.Log("Retry");
        if (gameObject.transform.name.Equals("Retry"))
        {
            var pf = PlayerPrefs.GetInt("difficulty");
            if (PlayerPrefs.GetInt("difficulty") == 1)
                SceneManager.LoadScene("Level_1");
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(gameObject.transform.name.Equals("Endgame"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


}
