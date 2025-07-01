using UnityEngine;
using UnityEngine.UI;

public class CheckMaps : MonoBehaviour
{
    private BuyMapCoins mapCoins;
    public Image[] maps;
    public Sprite selected, notSelected;

    void Start()
    {
        WhichMapSelected();
        mapCoins = GetComponent<BuyMapCoins>();
        if (PlayerPrefs.GetString("City") == "Open")
        {
            mapCoins.coins1000.SetActive(false);
            mapCoins.money0_99.SetActive(false);
            mapCoins.cityBtn.SetActive(true);
        }

        if (PlayerPrefs.GetString("Megapolis") == "Open")
        {
            mapCoins.coins5000.SetActive(false);
            mapCoins.money1_99.SetActive(false);
            mapCoins.megapolisBtn.SetActive(true);
        }
    }

    public void WhichMapSelected()
    {
        switch (PlayerPrefs.GetInt("NowMap"))
        {
            case 2:
                maps[0].sprite = notSelected;
                maps[1].sprite = selected;
                maps[2].sprite = notSelected;
                break;

            case 3:
                maps[0].sprite = notSelected;
                maps[1].sprite = notSelected;
                maps[2].sprite = selected;
                break;

            default:
                maps[0].sprite = selected;
                maps[1].sprite = notSelected;
                maps[2].sprite = notSelected;
                break;
        }


    }















}
