using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TempStuff : MonoBehaviour
{
    public Image warning;

    public void ClearSave()
    {
        Time.timeScale = 0;
        warning.gameObject.SetActive(true);
        File.Delete(Path.Combine(Application.persistentDataPath, "data.save"));
        Application.Quit();
    }
}