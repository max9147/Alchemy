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
    public GameObject tutorial;
    public TextMeshProUGUI buttonNextText;

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
    }

    public void TutorialShow()
    {
        GetComponent<Popups>().PopupClose();
        Time.timeScale = 0;
        pauseIcon.gameObject.SetActive(true);
        GetComponent<Popups>().popupOpen = true;
        part1.gameObject.SetActive(true);
        part2.gameObject.SetActive(false);
        tutorial.SetActive(true);
        buttonNextText.text = "Дальше";
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
}