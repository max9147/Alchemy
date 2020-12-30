using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Potions : ScriptableObject
{
    public string potionName;
    public Sprite image;
    public Sprite[] recipe;
    public bool status = false;
}
