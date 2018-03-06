using UnityEngine;

namespace Assets.Scripts
{
    public class KeyCollectingScript : MonoBehaviour
    {
        public bool HasKey;

        private GameObject _cageApproached;

        public GameObject PressKText;

        public GameObject KeyStatus;

        // Use this for initialization
        void Start()
        {
            HasKey = false;
            _cageApproached = null;
        }

        // Update is called once per frame
        void Update()
        {
            if (_cageApproached == null)
                return;

            if (Input.GetKey("k"))
                UnlockTheDog();
            else
            {
                PressKText.SetActive(true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Cage"))
            {
                FindObjectOfType<AudioManager>().Play("HitCage");
                if(HasKey)
                    _cageApproached = collision.gameObject;
            }
            if (collision.gameObject.tag.Equals("Key"))
            {
                HasKey = true;
                KeyStatus.SetActive(true);
                Destroy(GameObject.FindGameObjectWithTag("Key"));
            }

        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _cageApproached = null;
            PressKText.SetActive(false);

        }

        private void UnlockTheDog()
        {
            FindObjectOfType<AudioManager>().Play("Barking");

            var dogFromCage = _cageApproached.transform.GetChild(0).gameObject;
            var theDogScript = dogFromCage.GetComponent<OtherDogFolowingMainDogScript>();
            theDogScript.StartFollowingMainDog();

            _cageApproached = null;
        }
    }
}

