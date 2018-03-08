using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperFunctions {

	public static void StopAllGuards()
    {
        GameObject guardsContainer = GameObject.Find("CatchObstacles");
        if (guardsContainer == null)
            return;
        int children = guardsContainer.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            Debug.Log("For loop: " + guardsContainer.transform.GetChild(i));
            guardsContainer.transform.GetChild(i).GetComponent<GuardScript>().SetGuardMovement(false);
        }
    }

    public static void MoveAllGuards()
    {
        GameObject guardsContainer = GameObject.Find("CatchObstacles");
        if (guardsContainer == null)
            return;
        int children = guardsContainer.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            Debug.Log("For loop: " + guardsContainer.transform.GetChild(i));
            guardsContainer.transform.GetChild(i).GetComponent<GuardScript>().SetGuardMovement(true);
        }
    }

    public static void StopAlarmSystem()
    {
        Text[] allObjects = UnityEngine.Object.FindObjectsOfType<Text>();
        if (allObjects.Length == 0)
            return;
        foreach (Text go in allObjects)
        {
            TimerScript ts = go.GetComponent<TimerScript>();
            if(ts != null)
            {
                ts.AlarmDeactivated();
            }
        }

        
    }

    public static void StartAlarmSystem()
    {
        Text[] allObjects = UnityEngine.Object.FindObjectsOfType<Text>();
        if (allObjects.Length == 0)
            return;
        foreach (Text go in allObjects)
        {
            TimerScript ts = go.GetComponent<TimerScript>();
            if (ts != null)
            {
                ts.AlarmActivated();
            }
        }


    }

    public static void StopAllCameras()
    {
        GameObject camerasContainer = GameObject.Find("Cameras");
        if (camerasContainer == null)
            return;
        int children = camerasContainer.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            int childrenchild = camerasContainer.transform.GetChild(i).transform.childCount;

            for(int c = 0; c < childrenchild; c++)
            {
                var clm = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraLeftMiddleScript>();
                var cltc = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraLeftTopCornerScript>();
                var crm = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraRightMiddleScript>();
                var crtc = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraRightTopCornerScript>();
                var ctm = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraTopMiddleScript>();

                if (clm != null)
                {
                    clm.StopCameraRotation();
                    return;
                }
                if (cltc != null)
                {
                    clm.StopCameraRotation();
                    return;
                }
                if (crm != null)
                {
                    clm.StopCameraRotation();
                    return;
                }
                if (crtc != null)
                {
                    clm.StopCameraRotation();
                    return;
                }
                if (ctm != null)
                {
                    clm.StopCameraRotation();
                    return;
                }
            }           

        }
    }

    public static void MoveAllCameras()
    {
        GameObject camerasContainer = GameObject.Find("Cameras");
        if (camerasContainer == null)
            return;
        int children = camerasContainer.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            int childrenchild = camerasContainer.transform.GetChild(i).transform.childCount;

            for (int c = 0; c < childrenchild; c++)
            {
                var clm = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraLeftMiddleScript>();
                var cltc = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraLeftTopCornerScript>();
                var crm = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraRightMiddleScript>();
                var crtc = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraRightTopCornerScript>();
                var ctm = camerasContainer.transform.GetChild(i).transform.GetChild(c).GetComponent<CameraTopMiddleScript>();

                if (clm != null)
                {
                    clm.StartCameraRotation();
                    return;
                }
                if (cltc != null)
                {
                    clm.StartCameraRotation();
                    return;
                }
                if (crm != null)
                {
                    clm.StartCameraRotation();
                    return;
                }
                if (crtc != null)
                {
                    clm.StartCameraRotation();
                    return;
                }
                if (ctm != null)
                {
                    clm.StartCameraRotation();
                    return;
                }
            }

        }
    }
}
