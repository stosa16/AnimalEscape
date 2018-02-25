using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectingScript : MonoBehaviour {

    public bool HasKey;

    // Use this for initialization
    void Start()
    {
        HasKey = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Cage") && HasKey)
        {
            var dogFromCage = collision.gameObject.transform.GetChild(0).gameObject;
            var theDogScript = dogFromCage.GetComponent<OtherDogFolowingMainDogScript>();
            theDogScript.StartFollowingMainDog();

        }
        else if (collision.gameObject.tag.Equals("Key"))
        {
            HasKey = true;
            gameObject.transform.Find("KeyStatus").GetComponent<SpriteRenderer>().enabled = true;
            Destroy(GameObject.FindGameObjectWithTag("Key"));
        }

    }
}
