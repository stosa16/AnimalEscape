using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameScript : MonoBehaviour {

    private void OnMouseDown()
    {
        //if()
        Debug.Log("Retry");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
