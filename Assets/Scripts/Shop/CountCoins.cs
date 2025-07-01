using UnityEngine.UI;
using UnityEngine;

public class CountCoins : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
    }


}
