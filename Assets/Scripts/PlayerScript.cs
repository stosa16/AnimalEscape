using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : CharacterScript {

    public bool _gameIsOver;
    public GameObject gameOver;
    public GameObject levelSuccess;
    private Animator _animator;
    private int _oldAnimatorDirection;

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
        var animatorDirection = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            animatorDirection = Constants.DirectionUp;
            direction += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animatorDirection = Constants.DirectionDown;
            direction += Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            animatorDirection = Constants.DirectionLeft;
            direction += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animatorDirection = Constants.DirectionRight;
            direction += Vector2.right;
        } else
        {
            animatorDirection = _oldAnimatorDirection * 10;
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
            Debug.Log("It is a catching obstacele");
            levelSuccess.SetActive(true);
            //_gameIsOver = true;
            SceneManager.LoadScene("LevelWithCages");
        }

        if (collision.gameObject.tag.Equals("AlarmSystem"))
        {
            Debug.Log("Dog is collliding with Alarm system");
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
