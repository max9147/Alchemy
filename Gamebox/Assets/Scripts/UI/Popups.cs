using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Popups : MonoBehaviour
{
    public Image pauseIcon;

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
                    PopupClose();
                    popupOpen = false;
                    return;
                }
                Time.timeScale = 1;
                pauseIcon.gameObject.SetActive(false);
                popupOpen = true;
                popupShop.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 2:
                if (popupGuilds.activeInHierarchy)
                {
                    PopupClose();
                    popupOpen = false;
                    return;
                }
                Time.timeScale = 1;
                pauseIcon.gameObject.SetActive(false);
                popupOpen = true;
                popupGuilds.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 3:
                if (popupRecipes.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    pauseIcon.gameObject.SetActive(false);
                    PopupClose();
                    popupOpen = false;
                    return;
                }
                Time.timeScale = 0;
                pauseIcon.gameObject.SetActive(true);
                popupOpen = true;
                popupRecipes.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                break;

            case 4:
                if (popupPause.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    pauseIcon.gameObject.SetActive(false);
                    PopupClose();
                    popupOpen = false;
                    return;
                }
                Time.timeScale = 0;
                pauseIcon.gameObject.SetActive(true);
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

    public void PopupClose()
    {
        popupOpen = false;
        Time.timeScale = 1;
        pauseIcon.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        popupShop.gameObject.SetActive(false);
        popupGuilds.gameObject.SetActive(false);
        popupRecipes.gameObject.SetActive(false);
        popupPause.gameObject.SetActive(false);
    }
}