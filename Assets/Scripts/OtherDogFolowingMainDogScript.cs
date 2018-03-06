using System;
using UnityEngine;

namespace Assets.Scripts {
    public class OtherDogFolowingMainDogScript : MonoBehaviour {
        public bool IsFree;
        private GameObject _mainDog;
        [SerializeField]
        protected float Speed;
        
        public int DogPosition;

        private CharacterScript _character;

        public int FollowingDistance;

        void Start()
        {
            IsFree = false;
            _mainDog = GameObject.FindGameObjectWithTag("Spieler");
            Speed = 1.5f;
            _character = _mainDog.GetComponent<CharacterScript>();
            FollowingDistance = 30;
        }

        public void MakeItSimulated()
        {
            //has to be false while dog is in the cage, otherwise it collides with cage itself.
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }

        internal void StartFollowingMainDog()
        {
            _character.MaxNumberOfStoredPositions += FollowingDistance;

            DogPosition = DogPositionBuilder.GetNext();
            MakeItSimulated();
            IsFree = true;
        }

        void Update()
        {
            try
            {
                if (IsFree)
                {
                    var nextState = _character.PreviousPositions[_character.MaxNumberOfStoredPositions - DogPosition];
                    transform.position = new Vector2(nextState.Position.x, nextState.Position.y); 

                    // FOR SANDRA:  nextState.Direction should be the thing you can use :)
                }
            }
            catch (Exception)
            {
                //doNothing
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("CatchObstacle") || collision.gameObject.tag.Equals("Camera"))
            {
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                Debug.Log("It is a catching obstacele");
                var playerScript = _mainDog.GetComponent<PlayerScript>();
                playerScript.gameOver.SetActive(true);
                playerScript._gameIsOver = true;
            }
        }
    }
}
