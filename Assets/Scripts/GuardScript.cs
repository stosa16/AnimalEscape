using Assets.Scripts;
using UnityEngine;

public class GuardScript : MonoBehaviour {

    public float xMovement;
    public float yMovement;
    public float distance;
    public bool isAnimatedGuard;
    public int initialDirection;
    private Vector2 originPosition;
    private Vector2 previousPosition;
    public Animator _animator;
    private bool _guardMoving;

    // Use this for initialization
    void Start()
    {
        originPosition = gameObject.transform.position;
        _guardMoving = true;
        if (isAnimatedGuard)
        {
            _animator = this.GetComponent<Animator>();
            _animator.SetInteger("Direction", initialDirection);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_guardMoving)
            return;
        previousPosition = gameObject.transform.position;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + (xMovement * Time.deltaTime), gameObject.transform.position.y + (yMovement * Time.deltaTime));
        if (isAnimatedGuard)
        {
            AnimateGuard();
        }

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

    private void AnimateGuard()
    {
        var fieldOfView = gameObject.transform.GetChild(0);
        if (gameObject.transform.position.y < previousPosition.y)
        {
            if (_animator.GetInteger("Direction") != Constants.DirectionDown)
            {
                _animator.SetInteger("Direction", Constants.DirectionDown);                
                fieldOfView.transform.Rotate(180, 0, 0);
                fieldOfView.transform.position = new Vector3(fieldOfView.transform.position.x, gameObject.transform.position.y - (float)1, fieldOfView.transform.position.z);
            }
        }
        if (gameObject.transform.position.x < previousPosition.x)
        {
            if (_animator.GetInteger("Direction") != Constants.DirectionLeft)
            {
                _animator.SetInteger("Direction", Constants.DirectionLeft);
                fieldOfView.transform.Rotate(180, 0, 0);
                fieldOfView.transform.position = new Vector3(fieldOfView.transform.position.x - (float)1.8, fieldOfView.transform.position.y, fieldOfView.transform.position.z);
            }
        }
        if (gameObject.transform.position.y > previousPosition.y)
        {
            if (_animator.GetInteger("Direction") != Constants.DirectionUp)
            {
                _animator.SetInteger("Direction", Constants.DirectionUp);
                fieldOfView.transform.Rotate(180, 0, 0);
                fieldOfView.transform.position = new Vector3(fieldOfView.transform.position.x, gameObject.transform.position.y + (float)0.8, fieldOfView.transform.position.z);
            }
        }
        if (gameObject.transform.position.x > previousPosition.x)
        {
            if (_animator.GetInteger("Direction") != Constants.DirectionRight)
            {
                _animator.SetInteger("Direction", Constants.DirectionRight);
                fieldOfView.transform.Rotate(180, 0, 0);
                fieldOfView.transform.position = new Vector3(fieldOfView.transform.position.x + (float)1.8, fieldOfView.transform.position.y, fieldOfView.transform.position.z);
            }
        }
    }

        public void SetGuardMovement(bool shouldMove)
    {
        _guardMoving = shouldMove;
    }

}
