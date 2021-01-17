using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popups : MonoBehaviour
{
    public GameObject popupShop;
    public GameObject popupGuilds;
    public GameObject popupRecipes;
    public GameObject popupPause;

    public bool popupOpen = false;

    public void PopupOpen(int id)
    {
        switch (id)
        {
            case 1:
                if (popupShop.activeInHierarchy)
                {
                    PopupClose(id);
                    popupOpen = false;
                    return;
                }
                popupOpen = true;
                popupShop.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 2:
                if (popupGuilds.activeInHierarchy)
                {
                    PopupClose(id);
                    popupOpen = false;
                    return;
                }
                popupOpen = true;
                popupGuilds.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 3:
                if (popupRecipes.activeInHierarchy)
                {
                    PopupClose(id);
                    popupOpen = false;
                    return;
                }
                popupOpen = true;
                popupRecipes.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                break;

            case 4:
                if (popupPause.activeInHierarchy)
                {
                    PopupClose(id);
                    popupOpen = false;
                    return;
                }
                popupOpen = true;
                popupPause.gameObject.SetActive(true);
                popupRecipes.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void PopupClose(int id)
    {
        popupOpen = false;

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

            case 4:
                popupPause.gameObject.SetActive(false);
                break;

            default:
                break;
        }
    }
}