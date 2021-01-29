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

    public GameObject buttonShop;
    public GameObject buttonGuilds;
    public GameObject buttonRecipes;
    public GameObject buttonPause;

    public AudioClip openRecipes;
    public AudioClip openGuilds;
    public AudioClip openShop;

    public bool popupOpen = false;
    private int popupOpenID = 0;

    private void Update()
    {
        if (popupOpen)
        {
            switch (popupOpenID)
            {
                case 1:
                    EventSystem.current.SetSelectedGameObject(buttonShop);
                    break;
                case 2:
                    EventSystem.current.SetSelectedGameObject(buttonGuilds);
                    break;
                case 3:
                    EventSystem.current.SetSelectedGameObject(buttonRecipes);
                    break;
                case 4:
                    EventSystem.current.SetSelectedGameObject(buttonPause);
                    break;
                default:
                    EventSystem.current.SetSelectedGameObject(null);
                    break;
            }
        }
        else
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void PopupOpen(int id)
    {
        switch (id)
        {
            case 1:
                popupOpenID = 1;
                if (popupShop.activeInHierarchy)
                {
                    PopupClose();
                    popupOpen = false;
                    return;
                }

                GetComponent<AudioSource>().clip = openShop;
                GetComponent<AudioSource>().Play();

                Time.timeScale = 1;
                pauseIcon.gameObject.SetActive(false);
                popupOpen = true;
                popupShop.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 2:
                popupOpenID = 2;
                if (popupGuilds.activeInHierarchy)
                {
                    PopupClose();
                    popupOpen = false;
                    return;
                }

                GetComponent<AudioSource>().clip = openGuilds;
                GetComponent<AudioSource>().Play();

                Time.timeScale = 1;
                pauseIcon.gameObject.SetActive(false);
                popupOpen = true;
                popupGuilds.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupRecipes.gameObject.SetActive(false);
                break;

            case 3:
                popupOpenID = 3;
                if (popupRecipes.activeInHierarchy)
                {
                    Time.timeScale = 1;
                    pauseIcon.gameObject.SetActive(false);
                    PopupClose();
                    popupOpen = false;
                    return;
                }

                GetComponent<AudioSource>().clip = openRecipes;
                GetComponent<AudioSource>().Play();

                Time.timeScale = 0;
                pauseIcon.gameObject.SetActive(true);
                popupOpen = true;
                popupRecipes.gameObject.SetActive(true);
                popupPause.gameObject.SetActive(false);
                popupShop.gameObject.SetActive(false);
                popupGuilds.gameObject.SetActive(false);
                break;

            case 4:
                popupOpenID = 4;
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
        popupOpenID = 0;
        popupOpen = false;
        Time.timeScale = 1;
        pauseIcon.gameObject.SetActive(false);
        popupShop.gameObject.SetActive(false);
        popupGuilds.gameObject.SetActive(false);
        popupRecipes.gameObject.SetActive(false);
        popupPause.gameObject.SetActive(false);
    }
}