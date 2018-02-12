using Assets.Scripts;
using UnityEngine;

public class GuardScript : MonoBehaviour {

    public float xMovement;
    public float yMovement;
    public float distance;
    private Vector2 originPosition;

    // Use this for initialization
    void Start()
    {
        originPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position = new Vector2(gameObject.transform.position.x + (xMovement * Time.deltaTime), gameObject.transform.position.y + (yMovement * Time.deltaTime));
        if (gameObject.transform.position.y <= originPosition.y - distance)
        {
            yMovement *= -1;
        }
        if (gameObject.transform.position.x <= originPosition.x - distance)
        {
            xMovement *= -1;
        }
        if (gameObject.transform.position.y >= originPosition.y)
        {
            yMovement *= -1;
        }
        if (gameObject.transform.position.x >= originPosition.x)
        {
            xMovement *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("OtherDog"))
        {
            collision.gameObject.GetComponent<OtherDogScript>().ChangeDirection();
        }
    }

}
