using System;
using UnityEngine;


namespace Assets.Scripts {
    public class OtherDogFolowingMainDogScript : MonoBehaviour {
        public bool IsFree;
        private GameObject mainDog;
        [SerializeField]
        protected float speed;


        void Start()
        {
            IsFree = false;
            mainDog = GameObject.FindGameObjectWithTag("Spieler");
            speed = 0.65f;
        }

        public void MakeItSimulated()
        {
            //has to be false while dog is in the cage, otherwise it collides with cage itself.
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }

        void Update()
        {
            if (IsFree)
            {
                var mainDogPosition = mainDog.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, mainDogPosition, speed * Time.deltaTime);
            }
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("CatchObstacle") || collision.gameObject.tag.Equals("Camera"))
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                Debug.Log("It is a catching obstacele");
                var playerScript = mainDog.GetComponent<PlayerScript>();
                playerScript.gameOver.SetActive(true);
                playerScript._gameIsOver = true;
            }
        }

    }
}
