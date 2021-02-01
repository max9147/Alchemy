﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Potions : ScriptableObject
{
    public string potionName;
    public Sprite imageBottle;
    public Sprite imageWater;
    public Color waterColor;
    public Sprite rareEffect;
    public Color effectColor;
    public Sprite[] colored;
    public Sprite rare;
    public int id;
}