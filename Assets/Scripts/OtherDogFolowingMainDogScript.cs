using System;
using System.Linq;
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

        private Animator _animator;
        private Vector2 _oldPosition;

        void Start()
        {
            IsFree = false;
            _mainDog = GameObject.FindGameObjectWithTag("Spieler");
            Speed = 1.5f;
            _character = _mainDog.GetComponent<CharacterScript>();
            FollowingDistance = 30;
            _animator = GetComponent<Animator>();
        }

        public void MakeItSimulated()
        {
            //has to be false while dog is in the cage, otherwise it collides with cage itself.
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }

        internal void StartFollowingMainDog()
        {
            _character.MaxNumberOfStoredPositions += FollowingDistance;

            IsFree = true;

            var numberOfSavedDogs = GameObject.FindGameObjectsWithTag("OtherDog").Count(dog => dog.GetComponent<OtherDogFolowingMainDogScript>().IsFree == true);
            DogPosition = numberOfSavedDogs * 30;
            MakeItSimulated();
        }

        void Update()
        {
            try
            {
                if (IsFree)
                {
                    if (Vector2.Distance(transform.position, _character.transform.position) <= 0.6)
                        return;
                    var nextState = _character.PreviousPositions[_character.MaxNumberOfStoredPositions - DogPosition];
                    transform.position = new Vector2(nextState.Position.x, nextState.Position.y);
                    if (_oldPosition.x != transform.position.x || _oldPosition.y != transform.position.y)
                        _animator.SetInteger("Direction", nextState.Direction);
                    else
                        _animator.SetInteger("Direction", _animator.GetInteger("Direction") * 10);

                    _oldPosition = transform.position;                                  
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
