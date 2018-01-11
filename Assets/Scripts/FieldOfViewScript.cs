using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewScript : MonoBehaviour {

    public GameObject GameOver;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Gesichtet");
        if(collision.gameObject.tag.Equals("Spieler"))
        {
            Debug.Log("Spieler detected");
            GameOver.SetActive(true);
            GameObject player = collision.gameObject;
            PlayerScript ps = player.GetComponent<PlayerScript>();
            ps._gameIsOver = true;
        }

    }
}
