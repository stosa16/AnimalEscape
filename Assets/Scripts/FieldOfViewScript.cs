using Assets.Scripts;
using UnityEngine;

public class FieldOfViewScript : MonoBehaviour {

    public GameObject GameOver;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Gesichtet");
        var tag = collision.gameObject.tag;
        if (gameObject.tag.Equals("DialogObstacle"))
        {
            return;
        }
        if (tag.Equals("Spieler") || tag.Equals("OtherDog"))
        {
           // Debug.Log("Spieler detected");
            GameObject player = GameObject.FindGameObjectWithTag("Spieler");
            PlayerScript ps = player.GetComponent<PlayerScript>();
            //ps._gameIsOver = true;
            //GameOver.SetActive(true);
            ps.SetGameOver();
        }
    }
}
