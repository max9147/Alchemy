using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TempStuff : MonoBehaviour
{
    public void ClearSave()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, "data.save"));
        Application.Quit();
    }
}