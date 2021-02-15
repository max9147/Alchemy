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
    public Image pauseIcon;
    public Sprite help1;
    public Sprite help2;
    public Sprite help3;
    public Sprite help4;
    public Sprite help5;
    public Sprite help6;
    public GameObject tutorial;
    public GameObject message;
    public TextMeshProUGUI buttonNextText;
    public Settings settings;
    public int helpStep = 0;

    private int tutorialStep = 0;

    private bool messageShown = false;

    private void Start()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (!File.Exists(path))
        {
            GetComponent<Popups>().popupOpen = true;
            Time.timeScale = 0;
            pauseIcon.gameObject.SetActive(true);
            tutorial.SetActive(true);
            content.sprite = help1;
        }
    }

    public void TutorialNext()
    {
        switch (tutorialStep)
        {
            case 0:
                tutorialStep++;
                content.sprite = help2;
                break;
            case 1:
                tutorialStep++;
                content.sprite = help3;
                break;
            case 2:
                tutorialStep++;
                content.sprite = help4;
                break;
            case 3:
                tutorialStep++;
                content.sprite = help5;
                break;
            case 4:
                tutorialStep++;
                content.sprite = help6;
                buttonNextText.text = "Закрыть";
                break;
            case 5:
                GetComponent<Popups>().popupOpen = false;
                tutorial.SetActive(false);
                Time.timeScale = 1;
                pauseIcon.gameObject.SetActive(false);
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
            pauseIcon.gameObject.SetActive(false);
        }
        else
        {
            messageShown = true;
            message.SetActive(true);
            Time.timeScale = 0;
            pauseIcon.gameObject.SetActive(true);
        }
    }
}