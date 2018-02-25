using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour {

    [SerializeField]
    protected float speed;

    protected Vector2 direction;

    [SerializeField]
    public List<Vector2> previousDirections;

    public int limitOfPreviousDirections;


    // Use this for initialization
    void Start()
    {
        previousDirections = new List<Vector2>();
        limitOfPreviousDirections = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector2 mainDogCurrentPosition = GameObject.FindGameObjectWithTag("Spieler").transform.position;

        if (previousDirections.Count < limitOfPreviousDirections)
        {
            previousDirections.Add(mainDogCurrentPosition);
            return;
        }

        if (mainDogCurrentPosition != previousDirections[limitOfPreviousDirections - 2])
        {
            previousDirections.Add(mainDogCurrentPosition);
            if (previousDirections.Count > limitOfPreviousDirections)
                previousDirections.RemoveAt(0);
        }
    }
}
