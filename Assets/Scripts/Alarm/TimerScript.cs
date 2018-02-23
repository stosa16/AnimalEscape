using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public int seconds;
    private Text timerText;

	// Use this for initialization
	void Start () {
        timerText = gameObject.GetComponent<Text>();
        timerText.text = seconds.ToString();
        InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
	}

    void UpdateTimer()
    {
        int sec = Int32.Parse(timerText.text);
        if(sec <= 0)
        {
            Debug.Log("Time ran out.");
            CancelInvoke("UpdateTimer");
            timerText.text = "0";
            return;
        }

        timerText.text = (sec - 1).ToString();

    }


	
	

}
