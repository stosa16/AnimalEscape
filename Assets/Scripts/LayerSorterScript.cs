using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorterScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Dog is Trigger");
        if(collision.tag.Equals("StopObstacle"))
        {
            Debug.Log("It is a stopping Obstacle");
        }
        if(collision.tag.Equals("CatchObstacle"))
        {
            Debug.Log("It is a catching obstacele");    
        }
    }
}
