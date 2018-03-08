using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScript : CharacterScript {

    public bool inputEnabled = true;
    public bool _gameIsOver;
    public GameObject gameOver;
    public GameObject levelSuccess;
    private Animator _animator;
    private int _oldAnimatorDirection;
    public GameObject PressEnterContainer;
    private bool _goToNextLvlPossible;
    public Image goToNextLvlImg;

    // Use this for initialization
    void Start()
    {
        _gameIsOver = false;
        _animator = GetComponent<Animator>();

        PreviousPositions = new List<DogState>();
        _goToNextLvlPossible = false;
        StartCoroutine(FadeImage(true));
        MaxNumberOfStoredPositions = 0;
        gameObject.GetComponent<AudioManager>().Play("GeneralGameSound");

    }

    // Update is called once per frame
    protected override void Update()
    {


        if (_goToNextLvlPossible)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Go to next level after hit enter.");
                StartCoroutine(FadeImage(false));
            }
        }

        if (_gameIsOver)
            return;
        GetInput();
        Move();
    }

    private void GetInput()
    {
        direction = Vector2.zero;
        var animatorDirection = 0;
        if (inputEnabled == false)
        {
            animatorDirection = _oldAnimatorDirection * 10;
            _oldAnimatorDirection = animatorDirection;
            _animator.SetInteger("Direction", animatorDirection);
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            animatorDirection = Constants.DirectionUp;
            direction += Vector2.up;
            _isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animatorDirection = Constants.DirectionDown;
            direction += Vector2.down;
            _isMoving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animatorDirection = Constants.DirectionLeft;
            direction += Vector2.left;
            _isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animatorDirection = Constants.DirectionRight;
            direction += Vector2.right;
            _isMoving = true;
        }
        else
        {
            animatorDirection = _oldAnimatorDirection * 10;
            _isMoving = false;
        }

        _oldAnimatorDirection = animatorDirection;
        _animator.SetInteger("Direction", animatorDirection);
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
            Debug.Log("Dog is colliding with Door.");
            //levelSuccess.SetActive(true);
            //_gameIsOver = true;
            //SceneManager.LoadScene("LevelWithCages");
            _goToNextLvlPossible = true;
            PressEnterContainer.SetActive(true);
        }

        if (collision.gameObject.tag.Equals("AlarmSystem"))
        {
            Debug.Log("Dog is collliding with Alarm system");
            _goToNextLvlPossible = false;
        }

        _isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Player Script - Collision EXIT");
        _isColliding = false;
        PressEnterContainer.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("OtherDog"))
        {
            collision.gameObject.GetComponent<OtherDogScript>().ChangeDirectionFromObstacle();
        }
        if (collision.gameObject.tag.Equals("DialogObstacle"))
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    public void SetGameOver()
    {
        gameOver.SetActive(true);
        _gameIsOver = true;

        var otherDogs = GameObject.FindGameObjectsWithTag("OtherDog");

        foreach (var dog in otherDogs)
        {
            var script = dog.GetComponent<OtherDogFolowingMainDogScript>();
            if (script != null)
                script.IsFree = false;
        }
    }

    public void DisableInput()
    {
        inputEnabled = false;
    }

    public void EnableInput()
    {
        inputEnabled = true;
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                goToNextLvlImg.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                goToNextLvlImg.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }

}
