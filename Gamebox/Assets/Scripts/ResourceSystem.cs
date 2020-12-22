using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Resource
{
    Red,
    Blue,
    Yellow,
    White,
    Ladan,
    Eye,
    Stone,
    Sand
};

public enum Rarity
{
    Common,
    Rare,
};

public class ResourceData
{
    public Resource resourceName;
    public int amount;
    public Rarity resourceRarity;

    public ResourceData(Resource _resourceName, Rarity _resourceRarity)
    {
        resourceName = _resourceName;
        amount = 0;
        resourceRarity = _resourceRarity;
    }
}

public class ResourceSystem : MonoBehaviour
{
    public TextMeshProUGUI textRed;
    public TextMeshProUGUI textBlue;
    public TextMeshProUGUI textYellow;
    public TextMeshProUGUI textWhite;
    public TextMeshProUGUI textLadan;
    public TextMeshProUGUI textEye;
    public TextMeshProUGUI textStone;
    public TextMeshProUGUI textSand;

    public List<ResourceData> resources = new List<ResourceData>();

    private void Awake()
    {
        ResourceData red = new ResourceData(Resource.Red, Rarity.Common);
        resources.Add(red);
        ResourceData blue = new ResourceData(Resource.Blue, Rarity.Common);
        resources.Add(blue);
        ResourceData yellow = new ResourceData(Resource.Yellow, Rarity.Common);
        resources.Add(yellow);
        ResourceData white = new ResourceData(Resource.White, Rarity.Common);
        resources.Add(white);
        ResourceData ladan = new ResourceData(Resource.Ladan, Rarity.Rare);
        resources.Add(ladan);
        ResourceData eye = new ResourceData(Resource.Eye, Rarity.Rare);
        resources.Add(eye);
        ResourceData stone = new ResourceData(Resource.Stone, Rarity.Rare);
        resources.Add(stone);
        ResourceData sand = new ResourceData(Resource.Sand, Rarity.Rare);
        resources.Add(sand);
    }

    public void AddResource(Resource resourceType, int amount)
    {
        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
            {
                res.amount += amount;

                switch (res.resourceName)
                {
                    case Resource.Red:
                        textRed.text = $"{GetAmount(Resource.Red)}";
                        break;

                    case Resource.Blue:
                        textBlue.text = $"{GetAmount(Resource.Blue)}";
                        break;

                    case Resource.Yellow:
                        textYellow.text = $"{GetAmount(Resource.Yellow)}";
                        break;

                    case Resource.White:
                        textWhite.text = $"{GetAmount(Resource.White)}";
                        break;

                    case Resource.Ladan:
                        textLadan.text = $"{GetAmount(Resource.Ladan)}";
                        break;

                    case Resource.Eye:
                        textEye.text = $"{GetAmount(Resource.Eye)}";
                        break;

                    case Resource.Stone:
                        textStone.text = $"{GetAmount(Resource.Stone)}";
                        break;

                    case Resource.Sand:
                        textSand.text = $"{GetAmount(Resource.Sand)}";
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void RemoveResource(Resource resourceType, int amount)
    {
        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
            {
                res.amount -= amount;

                switch (res.resourceName)
                {
                    case Resource.Red:
                        textRed.text = $"{GetAmount(Resource.Red)}";
                        break;

                    case Resource.Blue:
                        textBlue.text = $"{GetAmount(Resource.Blue)}";
                        break;

                    case Resource.Yellow:
                        textYellow.text = $"{GetAmount(Resource.Yellow)}";
                        break;

                    case Resource.White:
                        textWhite.text = $"{GetAmount(Resource.White)}";
                        break;

                    case Resource.Ladan:
                        textLadan.text = $"{GetAmount(Resource.Ladan)}";
                        break;

                    case Resource.Eye:
                        textEye.text = $"{GetAmount(Resource.Eye)}";
                        break;

                    case Resource.Stone:
                        textStone.text = $"{GetAmount(Resource.Stone)}";
                        break;

                    case Resource.Sand:
                        textSand.text = $"{GetAmount(Resource.Sand)}";
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public int GetAmount(Resource resourceType)
    {
        int returnAmount = 0;

        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
                returnAmount = res.amount;
        }
        return returnAmount;
    }

    public Rarity GetRarity(Resource resourceType)
    {
        Rarity returnRarity = Rarity.Common;

        foreach (ResourceData res in resources)
        {
            if (res.resourceName == resourceType)
                returnRarity = res.resourceRarity;
        }
        return returnRarity;
    }
}