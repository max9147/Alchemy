using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button helpButton;
    public GameObject[] helpScreens;
    public Settings settings;
    public int helpStep;
    public bool[] helpShown = new bool[7];
    public bool readingHelp = false;

    private float helptime = 0;

    private void Start()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (!File.Exists(path))
        {
            for (int i = 0; i < helpShown.Length; i++)
                helpShown[i] = false;
        }
    }

    public void GetHelp()
    {
        if (!helpShown[helpStep]) helpButton.interactable = true;
        helpShown[helpStep] = true;
    }

    public void ShowHelp()
    {
        readingHelp = true;
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
                if (GetComponent<CameraMovement>().dir == 1)
                    GetComponent<CameraMovement>().MoveCam();
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

    private void Update()
    {
        if (readingHelp)
            helptime += Time.deltaTime;

        if (helptime >= settings.helpTime && Input.touchCount > 0)
        {
            readingHelp = false;
            helptime = 0;
            foreach (var item in helpScreens)
                item.SetActive(false);
        }
    }
}