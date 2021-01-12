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
    public GameObject tutorial;
    public TextMeshProUGUI buttonNextText;

    private void Start()
    {
        string path = Application.persistentDataPath + "/data.save";
        if (!File.Exists(path))
        {
            Time.timeScale = 0;
            tutorial.SetActive(true);
            part1.gameObject.SetActive(true);
        }
    }

    public void TutorialShow()
    {
        part1.gameObject.SetActive(true);
        part2.gameObject.SetActive(false);
        tutorial.SetActive(true);
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
            tutorial.SetActive(false);
            Time.timeScale = 1;
        }
    }
}