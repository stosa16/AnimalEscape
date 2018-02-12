using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : CharacterScript {

    public bool _gameIsOver;
    public GameObject gameOver;
    public GameObject levelSuccess;

    // Use this for initialization
    void Start()
    {
        _gameIsOver = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (_gameIsOver)
            return;
        GetInput();
        Move();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction += Vector2.right;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player Script - Dog is CollisionDetector");

        if (collision.gameObject.tag.Equals("StopObstacle"))
        {
            Debug.Log("It is a stopping Obstacle");
        }
        if (collision.gameObject.tag.Equals("CatchObstacle"))
        {
            Debug.Log("It is a catching obstacele");
            gameOver.SetActive(true);
            _gameIsOver = true;
        }
        if (collision.gameObject.tag.Equals("Door"))
        {
            Debug.Log("It is a catching obstacele");
            levelSuccess.SetActive(true);
            //_gameIsOver = true;
            SceneManager.LoadScene("LevelCages");
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
