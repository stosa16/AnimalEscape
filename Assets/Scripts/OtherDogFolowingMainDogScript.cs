using System;
using UnityEngine;


namespace Assets.Scripts {
    public class OtherDogFolowingMainDogScript : MonoBehaviour {
        public bool IsFree;
        private GameObject mainDog;
        [SerializeField]
        protected float speed;
        
        public int DogPosition;

        private CharacterScript character;

        void Start()
        {
            IsFree = false;
            mainDog = GameObject.FindGameObjectWithTag("Spieler");
            speed = 1.5f;
            character = mainDog.GetComponent<CharacterScript>();
        }

        public void MakeItSimulated()
        {
            //has to be false while dog is in the cage, otherwise it collides with cage itself.
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }

        internal void StartFollowingMainDog()
        {
            IsFree = true;
            DogPosition = DogPositionBuilder.GetNext();
            MakeItSimulated();

            character.limitOfPreviousDirections += 30;
        }

        void Update()
        {
            if (IsFree)
            {
                var mainDogPosition = mainDog.transform.position;
                //transform.position = Vector2.MoveTowards(transform.position, mainDogPosition, speed * Time.deltaTime);
                transform.position = character.previousDirections[character.limitOfPreviousDirections - DogPosition];
                //transform.Translate(character.previousDirections[0] * speed * Time.deltaTime);
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
