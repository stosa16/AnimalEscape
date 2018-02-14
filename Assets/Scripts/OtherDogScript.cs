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

        public void ChangeDirectionFromObstacle()
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

        public void ChangeDirectionFromGuard()
        {
            var dir = currentDirection;
            if (dir == Vector2.up)
                dir = Vector2.down;
            else if (dir == Vector2.left)
                dir = Vector2.right;
            else if (dir == Vector2.down)
                dir = Vector2.up;
            else
                dir = Vector2.left;

            currentDirection = dir;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ChangeDirectionFromObstacle();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}
