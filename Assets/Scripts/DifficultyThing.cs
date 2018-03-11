using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyThing : MonoBehaviour
{

    public void StartGame()
    {
     SceneManager.LoadScene("Level_1");   
    }

   public void SetDiffOne()
    {
        PlayerPrefs.SetInt("difficulty", 0);
    }

    public void SetDiffTwo()
    {
        PlayerPrefs.SetInt("difficulty", 1);
    }
}
