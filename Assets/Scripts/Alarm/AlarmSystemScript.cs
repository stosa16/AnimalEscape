using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Alarm
{
    public class AlarmSystemScript : MonoBehaviour {

        public GameObject AlarmActiveLightLeft;
        public GameObject AlarmActiveLightRight;
        public GameObject AlarmActiveLightMiddle;
        public GameObject AlarmActiveLightBackside;
        public GameObject AlarmNotActive;
        public GameObject PressEContainer;
        private bool _deactivationIsPossible;

        public Text TimerText;

        // Use this for initialization
        void Start () {
            _deactivationIsPossible = false;
            InvokeRepeating("UpdateAlarmSystem", 1.0f, 1.0f);
            
            gameObject.GetComponent<AudioManager>().Play("AlarmSound");
        }

        private void Update()
        {
            if(_deactivationIsPossible)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("User deactivates Alarm system!");
                    CancelInvoke("UpdateAlarmSystem");
                    AlarmActiveLightLeft.SetActive(false);
                    AlarmActiveLightRight.SetActive(false);
                    AlarmActiveLightMiddle.SetActive(false);
                    AlarmActiveLightBackside.SetActive(false);
                    AlarmNotActive.SetActive(true);
                    TimerText.SendMessage("AlarmDeactivated");
                    FindObjectOfType<AudioManager>().Stop("AlarmSound");
                    HelperFunctions.ShutDownLaserWalls();
                }
            }
        }

        void UpdateAlarmSystem()
        {
            if(AlarmActiveLightLeft.activeSelf)
            {
                AlarmActiveLightMiddle.SetActive(true);
                AlarmActiveLightLeft.SetActive(false);
                return;
            }

            if (AlarmActiveLightMiddle.activeSelf)
            {
                AlarmActiveLightRight.SetActive(true);
                AlarmActiveLightMiddle.SetActive(false);
                return;
            }

            if (AlarmActiveLightRight.activeSelf)
            {
                AlarmActiveLightBackside.SetActive(true);
                AlarmActiveLightRight.SetActive(false);
                return;
            }

            if (AlarmActiveLightBackside.activeSelf)
            {
                AlarmActiveLightLeft.SetActive(true);
                AlarmActiveLightBackside.SetActive(false);
                return;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Alarm System Script - Alarm collides with " + collision.transform.tag);
            if(AlarmNotActive.activeSelf)
            {
                return;
            }
            if(collision.transform.tag.Equals("Spieler"))
            {
                _deactivationIsPossible = true;
                PressEContainer.SetActive(true);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Debug.Log("Alarm System Script - ALarm is collision exit");

            if (collision.transform.tag.Equals("Spieler"))
            {
                _deactivationIsPossible = false;
                PressEContainer.SetActive(false);

            }
        }
    }
}
