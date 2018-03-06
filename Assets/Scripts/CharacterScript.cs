using System;
using Boo.Lang;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour
{

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected bool _isColliding;


    protected Vector2 direction;

    [SerializeField]
    public System.Collections.Generic.List<Vector2> PreviousPositions;


    public int MaxNumberOfStoredPositions;


    // Use this for initialization
    void Start()
    {
        PreviousPositions = new System.Collections.Generic.List<Vector2>();
        MaxNumberOfStoredPositions = 0;
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

        if (PreviousPositions.Count < MaxNumberOfStoredPositions)
        {
            PreviousPositions.Add(mainDogCurrentPosition);
            return;
        }

        if (!_isColliding && MaxNumberOfStoredPositions > 3 && mainDogCurrentPosition != PreviousPositions[MaxNumberOfStoredPositions-1])
        {
            PreviousPositions.Add(mainDogCurrentPosition);
            if (PreviousPositions.Count > MaxNumberOfStoredPositions)
                PreviousPositions.RemoveAt(0);
        }
    }
}
