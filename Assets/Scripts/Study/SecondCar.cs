using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondCar : MonoBehaviour
{
    void OnDestroy()
    {
        PlayerPrefs.SetString("First Game", "No");
        SceneManager.LoadScene("Game");
    }
}
