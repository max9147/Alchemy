using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image part1;
    public Image part2;
    public Image pauseIcon;
    public Sprite help1;
    public Sprite help2;
    public Sprite help3;
    public Sprite help4;
    public Sprite help5;
    public Sprite help6;
    public GameObject tutorial;
    public GameObject help;
    public Settings settings;
    public TextMeshProUGUI buttonNextText;
    public Button helpButton;
    public Camera cam;
    public int helpStep = 0;

    private float timePassed = 0;
    private bool showingHelp = false;

    private void Start()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (!File.Exists(path))
        {
            GetComponent<Popups>().popupOpen = true;
            Time.timeScale = 0;
            pauseIcon.gameObject.SetActive(true);
            tutorial.SetActive(true);
            part1.gameObject.SetActive(true);
        }

        if (helpStep == 0)
            GetHelp();
    }

    public void TutorialNext()
    {
        if (part1.IsActive())
        {
            part1.gameObject.SetActive(false);
            part2.gameObject.SetActive(true);
            buttonNextText.text = "Закрыть";
        }
        else if (part2.IsActive())
        {
            GetComponent<Popups>().popupOpen = false;
            tutorial.SetActive(false);
            Time.timeScale = 1;
            pauseIcon.gameObject.SetActive(false);
        }
    }

    public void ShowHelp()
    {
        help.SetActive(true);
        helpButton.interactable = false;
        showingHelp = true;

        switch (helpStep)
        {
            case 1:
                help.GetComponent<Image>().sprite = help1;
                break;
            case 2:
                help.GetComponent<Image>().sprite = help2;
                break;
            case 3:
                help.GetComponent<Image>().sprite = help3;
                break;
            case 4:
                help.GetComponent<Image>().sprite = help4;
                break;
            case 5:
                help.GetComponent<Image>().sprite = help5;
                break;
            case 6:
                help.GetComponent<Image>().sprite = help6;
                break;
            default:
                break;
        }
    }

    public void GetHelp()
    {
        helpStep++;
        helpButton.interactable = true;
    }

    private void Update()
    {
        if (showingHelp)
        {
            timePassed += Time.deltaTime;
            if (Input.touchCount > 0 && timePassed > settings.helpTime)
            {
                timePassed = 0;
                help.SetActive(false);
                showingHelp = false;
            }
        }
    }
}