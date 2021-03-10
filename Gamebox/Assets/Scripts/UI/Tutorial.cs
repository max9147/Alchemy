using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image content;
    public Sprite[] tutorials;
    public Button helpButton;
    public GameObject[] helpScreens;
    public GameObject tutorial;
    public GameObject message;
    public TextMeshProUGUI buttonNextText;
    public Settings settings;
    public bool mainGame = false;
    public int helpStep;
    public bool[] helpShown = new bool[7];

    private int tutorialStep = 0;
    private bool messageShown = false;

    private void Start()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (!File.Exists(path))
        {
            GetComponent<Popups>().popupOpen = true;
            Time.timeScale = 0;
            tutorial.SetActive(true);
            content.sprite = tutorials[0];
            for (int i = 0; i < helpShown.Length; i++)
            {
                helpShown[i] = false;
            }
        }
    }

    public void TutorialNext()
    {
        switch (tutorialStep)
        {
            case 0:
                tutorialStep++;
                content.sprite = tutorials[1];
                break;
            case 1:
                tutorialStep++;
                content.sprite = tutorials[2];
                break;
            case 2:
                tutorialStep++;
                content.sprite = tutorials[3];
                break;
            case 3:
                tutorialStep++;
                content.sprite = tutorials[4];
                break;
            case 4:
                tutorialStep++;
                content.sprite = tutorials[5];
                buttonNextText.text = "Закрыть";
                break;
            case 5:
                GetComponent<Popups>().popupOpen = false;
                tutorial.SetActive(false);
                Time.timeScale = 1;
                break;
            default:
                break;
        }
    }

    public void ToggleMessage()
    {
        if (messageShown)
        {
            messageShown = false;
            message.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            messageShown = true;
            message.SetActive(true);
            Time.timeScale = 0;
            mainGame = true;
        }
    }

    public void GetHelp()
    {
        if (!mainGame) return;
        if (!helpShown[helpStep]) helpButton.interactable = true;
        helpShown[helpStep] = true;
    }

    public void ShowHelp()
    {
        switch (helpStep)
        {
            case 0:
                GetComponent<Popups>().PopupOpen(1);
                helpScreens[0].SetActive(true);
                break;
            case 1:
                GetComponent<Popups>().PopupOpen(2);
                helpScreens[1].SetActive(true);
                break;
            case 2:
                GetComponent<Popups>().PopupOpen(2);
                helpScreens[2].SetActive(true);
                break;
            case 3:
                GetComponent<Popups>().PopupOpen(1);
                helpScreens[3].SetActive(true);
                break;
            case 4:
                GetComponent<Popups>().PopupOpen(1);
                helpScreens[4].SetActive(true);
                break;
            case 5:
                GetComponent<Popups>().PopupOpen(2);
                helpScreens[5].SetActive(true);
                break;
            case 6:
                GetComponent<Popups>().PopupOpen(3);
                helpScreens[6].SetActive(true);
                break;
            default:
                break;
        }
        helpButton.interactable = false;
    }
}