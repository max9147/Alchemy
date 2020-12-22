using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popups : MonoBehaviour
{
    public GameObject popupShop;
    public GameObject popupGuilds;
    public GameObject popupRecipes;

    public void PopupOpen(int id)
    {
        switch (id)
        {
            case 1:
                if (popupShop.activeInHierarchy)
                {
                    PopupClose(id);
                    return;
                }
                popupShop.gameObject.SetActive(true);
                popupGuilds.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 2:
                if (popupGuilds.activeInHierarchy)
                {
                    PopupClose(id);
                    return;
                }
                popupGuilds.gameObject.SetActive(true);
                popupShop.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 3:
                if (popupRecipes.activeInHierarchy)
                {
                    PopupClose(id);
                    return;
                }
                popupRecipes.gameObject.SetActive(true);
                popupShop.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void PopupClose(int id)
    {
        switch (id)
        {
            case 1:
                popupShop.gameObject.SetActive(false);
                break;

            case 2:
                popupGuilds.gameObject.SetActive(false);
                break;

            case 3:
                popupRecipes.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }
}