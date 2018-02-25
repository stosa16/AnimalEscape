using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : CharacterScript {

    public bool _gameIsOver;
    public GameObject gameOver;
    public GameObject levelSuccess;
    private Animator _animator;


    // Use this for initialization
    void Start()
    {
        _gameIsOver = false;
        _animator = this.GetComponent<Animator>();
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
            _animator.SetInteger("Direction", 4);
            direction += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _animator.SetInteger("Direction", 0);
            direction += Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _animator.SetInteger("Direction", 1);
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _animator.SetInteger("Direction", 2);
            direction += Vector2.right;
        }
        //previousDirections.Add(direction);
        //if(previousDirections.Count == 51)
        //    previousDirections.RemoveAt(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player Script - Dog is CollisionDetector");

        if (collision.gameObject.tag.Equals("StopObstacle"))
        {
            Debug.Log("It is a stopping Obstacle");
        }
        if (collision.gameObject.tag.Equals("CatchObstacle") || collision.gameObject.tag.Equals("Camera"))
        {
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            Debug.Log("It is a catching obstacele");
            SetGameOver();
        }
        if (collision.gameObject.tag.Equals("Door"))
        {
            Debug.Log("It is a catching obstacele");
            levelSuccess.SetActive(true);
            //_gameIsOver = true;
            SceneManager.LoadScene("LevelWithCages");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("OtherDog"))
        {
            collision.gameObject.GetComponent<OtherDogScript>().ChangeDirectionFromObstacle();
        }
    }

    public void SetGameOver(){
        gameOver.SetActive(true);
        _gameIsOver = true;

        var otherDogs = GameObject.FindGameObjectsWithTag("OtherDog");

        foreach (var dog in otherDogs)
        {
            var script = dog.GetComponent<OtherDogFolowingMainDogScript>();
            if(script != null)
                script.IsFree = false;
        }
    }
}
