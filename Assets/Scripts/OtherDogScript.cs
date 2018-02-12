using UnityEngine;

namespace Assets.Scripts {
    public class OtherDogScript : MonoBehaviour {
        public float speed;

        public Vector2 currentDirection;

        void Start()
        {
            speed = 0.9f;
           
        }

        private void Update()
        {

            transform.Translate(currentDirection * speed * Time.deltaTime);
        }

        public void ChangeDirection()
        {
            var dir = currentDirection;
            if (dir == Vector2.up)
                dir = Vector2.left;
            else if (dir == Vector2.left)
                dir = Vector2.down;
            else if (dir == Vector2.down)
                dir = Vector2.right;
            else
                dir = Vector2.up;

            currentDirection = dir;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ChangeDirection();
        }
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
