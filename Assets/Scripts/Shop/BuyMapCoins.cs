using UnityEngine;
using UnityEngine.UI;

public class BuyMapCoins : MonoBehaviour
{
    public Animation coinsText;
    public GameObject coins1000, coins5000, money0_99, money1_99, cityBtn, megapolisBtn;
    public Text coinsCount;
    public AudioClip success, fail;
    private int slowTimeCounter;
    public Text slowTimeCounterText;

    void Start()
    {
        slowTimeCounterText.text = PlayerPrefs.GetInt("Time Slow").ToString();
    }
    public void BuyNewMap(int needCoins)
    {

        int coins = PlayerPrefs.GetInt("Coins");
        if (coins < needCoins)
        {
            if (PlayerPrefs.GetString("Music") != "No")
            {
                GetComponent<AudioSource>().clip = fail;
                GetComponent<AudioSource>().Play();
            }


            coinsText.Play();
        }
        else
        {
            // Buy map and Slow Time
            switch (needCoins)
            {
                case 1000:
                    PlayerPrefs.SetString("City", "Open");
                    PlayerPrefs.SetInt("NowMap", 2);
                    GetComponent<CheckMaps>().WhichMapSelected();
                    coins1000.SetActive(false);
                    money0_99.SetActive(false);
                    cityBtn.SetActive(true);

                    break;

                case 5000:
                    PlayerPrefs.SetString("Megapolis", "Open");
                    PlayerPrefs.SetInt("NowMap", 3);
                    GetComponent<CheckMaps>().WhichMapSelected();
                    coins5000.SetActive(false);
                    money1_99.SetActive(false);
                    megapolisBtn.SetActive(true);
                    break;

                case 100:
                    slowTimeCounter = PlayerPrefs.GetInt("Time Slow");
                    slowTimeCounter++;
                    PlayerPrefs.SetInt("Time Slow", slowTimeCounter);
                    PlayerPrefs.Save();
                    slowTimeCounterText.text = PlayerPrefs.GetInt("Time Slow").ToString();

                    break;

            }

            int nowCoins = coins - needCoins;
            coinsCount.text = nowCoins.ToString();
            PlayerPrefs.SetInt("Coins", nowCoins);


            if (PlayerPrefs.GetString("Music") != "No")
            {
                GetComponent<AudioSource>().clip = success;
                GetComponent<AudioSource>().Play();
            }

        }
    }

















}
