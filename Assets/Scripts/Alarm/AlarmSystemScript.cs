using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmSystemScript : MonoBehaviour {

    public GameObject alarmActiveLightLeft;
    public GameObject alarmActiveLightRight;
    public GameObject alarmActiveLightMiddle;
    public GameObject alarmActiveLightBackside;
    public GameObject alarmNotActive;
    public GameObject pressEContainer;
    private bool deactivationIsPossible;

    public Text timerText;


    // Use this for initialization
    void Start () {
        deactivationIsPossible = false;
        InvokeRepeating("UpdateAlarmSystem", 1.0f, 1.0f);
	}

    private void Update()
    {
        if(deactivationIsPossible)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("User deactivates Alarm system!");
                CancelInvoke("UpdateAlarmSystem");
                alarmActiveLightLeft.SetActive(false);
                alarmActiveLightRight.SetActive(false);
                alarmActiveLightMiddle.SetActive(false);
                alarmActiveLightBackside.SetActive(false);
                alarmNotActive.SetActive(true);
                timerText.SendMessage("AlarmDeactivated");
            }
        }
    }

    void UpdateAlarmSystem()
    {
        if(alarmActiveLightLeft.activeSelf)
        {
            alarmActiveLightMiddle.SetActive(true);
            alarmActiveLightLeft.SetActive(false);
            return;
        }

        if (alarmActiveLightMiddle.activeSelf)
        {
            alarmActiveLightRight.SetActive(true);
            alarmActiveLightMiddle.SetActive(false);
            return;
        }

        if (alarmActiveLightRight.activeSelf)
        {
            alarmActiveLightBackside.SetActive(true);
            alarmActiveLightRight.SetActive(false);
            return;
        }

        if (alarmActiveLightBackside.activeSelf)
        {
            alarmActiveLightLeft.SetActive(true);
            alarmActiveLightBackside.SetActive(false);
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Alarm System Script - Alarm collides with " + collision.transform.tag);
        if(alarmNotActive.activeSelf)
        {
            return;
        }
        if(collision.transform.tag.Equals("Spieler"))
        {
            deactivationIsPossible = true;
            pressEContainer.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Alarm System Script - ALarm is collision exit");

        if (collision.transform.tag.Equals("Spieler"))
        {
            deactivationIsPossible = false;
            pressEContainer.SetActive(false);
        }
    }
}
