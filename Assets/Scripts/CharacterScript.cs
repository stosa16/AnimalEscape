using Assets.Scripts;
using UnityEngine;
using System.Collections.Generic;

public abstract class CharacterScript : MonoBehaviour {

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected bool _isColliding;


    protected Vector2 direction;

    [SerializeField]
    public List<DogState> PreviousPositions;


    public int MaxNumberOfStoredPositions;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    public void Move()
    {
        GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x + direction.x * speed * Time.deltaTime, transform.position.y + direction.y * speed * Time.deltaTime));

        Vector2 mainDogCurrentPosition = GameObject.FindGameObjectWithTag("Spieler").transform.position;
        int mainDogCurrentDirection = GameObject.FindGameObjectWithTag("Spieler").GetComponent<Animator>().GetInteger("Direction");

        if (PreviousPositions.Count < MaxNumberOfStoredPositions)
        {
            PreviousPositions.Add(new DogState
            {
                Position = mainDogCurrentPosition,
                Direction = mainDogCurrentDirection
            });

            return;
        }

        if (MaxNumberOfStoredPositions > 3 && mainDogCurrentPosition != PreviousPositions[MaxNumberOfStoredPositions - 1].Position)
        {
            PreviousPositions.Add(new DogState
            {
                Position = mainDogCurrentPosition,
                Direction = mainDogCurrentDirection
            });
            if (PreviousPositions.Count > MaxNumberOfStoredPositions)
                PreviousPositions.RemoveAt(0);
        }
    }
}
