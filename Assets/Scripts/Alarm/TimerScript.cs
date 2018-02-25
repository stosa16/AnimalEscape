using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public int seconds;
    private Text timerText;
    private bool alarmActive;
    public Image alarmImage;
    public GameObject dogPlayer;

    // Use this for initialization
    void Start () {
        timerText = gameObject.GetComponent<Text>();
        timerText.text = seconds.ToString();
        InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
        InvokeRepeating("UpdateAlarmImage", 1.5f, 1.5f);
        alarmActive = true;
	}

    void UpdateTimer()
    {
        if (alarmActive == false)
        {
            CancelInvoke("UpdateTimer");
            return;
        }

        int sec = Int32.Parse(timerText.text);
        if(sec <= 0)
        {
            Debug.Log("Time ran out.");
            CancelInvoke("UpdateTimer");
            CancelInvoke("UpdateAlarmImage");
            timerText.text = "0";
            dogPlayer.SendMessage("SetGameOver");
            return;
        }

        timerText.text = (sec - 1).ToString();

    }

    void UpdateAlarmImage()
    {
        if(alarmActive == false)
        {
            CancelInvoke("UpdateAlarmImage");
        }

        if(alarmImage.IsActive())
        {
            alarmImage.enabled = false;
        }
        else
        {
            alarmImage.enabled = true;
        }
    }

    public void AlarmDeactivated()
    {
        CancelInvoke("UpdateAlarmImage");
        CancelInvoke("UpdateTimer");
        alarmImage.enabled = false;
    }

}


