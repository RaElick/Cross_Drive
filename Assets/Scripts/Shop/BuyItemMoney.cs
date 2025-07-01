using UnityEngine;

public class BuyItemMoney : MonoBehaviour
{
    public enum Types
    {
        REMOVE_ADS, OPEN_CITY, OPEN_MEGAPOLIS
    }

    public Types type;
    public IAPManager iAPManager;

    public void BuyItem()
    {
        switch (type)
        {
            case Types.REMOVE_ADS:
                iAPManager.BuyAds();
                break;

            case Types.OPEN_CITY:
                iAPManager.BuyCity();
                break;

            case Types.OPEN_MEGAPOLIS:
                iAPManager.BuyMegapolis();
                break;
        }
    }
}
