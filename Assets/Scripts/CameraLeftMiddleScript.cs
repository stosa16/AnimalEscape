using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLeftMiddleScript : MonoBehaviour {

    private GameObject cam_right;
    private GameObject last_cam;
    private GameObject current_cam;
    public GameObject cam_half_right;
    public GameObject cam_back_half_right;
    public GameObject cam_front;
    public GameObject cam_back;
    public float rotation_speed;

    // Use this for initialization
    void Start()
    {
        cam_right = gameObject;
        last_cam = gameObject;
        current_cam = gameObject;
        InvokeRepeating("RotateCamera", rotation_speed * Time.deltaTime, rotation_speed * Time.deltaTime);
    }

    void RotateCamera()
    {
        Debug.Log(rotation_speed + " Sekunden sind vergangen.");

        if (current_cam == cam_right && last_cam == cam_right)
        {
            last_cam = cam_right;
            current_cam = cam_half_right;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_half_right && last_cam == cam_right)
        {
            last_cam = cam_half_right;
            current_cam = cam_front;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_front)
        {
            last_cam = cam_front;
            current_cam = cam_half_right;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_half_right && last_cam == cam_front)
        {
            last_cam = cam_half_right;
            current_cam = cam_right;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_right && last_cam == cam_half_right)
        {
            last_cam = cam_right;
            current_cam = cam_back_half_right;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_back_half_right && last_cam == cam_right)
        {
            last_cam = cam_back_half_right;
            current_cam = cam_back;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_back)
        {
            last_cam = cam_back;
            current_cam = cam_back_half_right;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_back_half_right && last_cam == cam_back)
        {
            last_cam = cam_back_half_right;
            current_cam = cam_right;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }

        if (current_cam == cam_right && last_cam == cam_back_half_right)
        {
            last_cam = cam_right;
            current_cam = cam_half_right;
            last_cam.SetActive(false);
            current_cam.SetActive(true);
            return;
        }
    }
}
