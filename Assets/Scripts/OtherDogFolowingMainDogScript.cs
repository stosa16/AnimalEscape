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
            speed = 0.5f;
        }

        void Update()
        {
            if (IsFree)
            {
                var mainDogPosition = mainDog.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, mainDogPosition, speed * Time.deltaTime);

            }
        }

    }
}
