using UnityEngine;

public abstract class CharacterScript : MonoBehaviour {

    [SerializeField]
    protected float speed;

    protected Vector2 direction;

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
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
