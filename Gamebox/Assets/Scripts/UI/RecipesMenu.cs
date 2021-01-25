using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipesMenu : MonoBehaviour
{
    public GameObject potionData;
    public GameObject content;
    public Potions[] potions;

    public bool[] addedArr = new bool[55];
    public bool pass = false;

    public void AddPotionData(Potions potion)
    {
        Debug.Log(pass);
        if (!addedArr[potion.id] || pass)
        {
            addedArr[potion.id] = true;

            RectTransform rt = content.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y + 250);

            GameObject data = Instantiate(potionData);
            data.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = potion.potionName;
            data.transform.Find("Image").GetComponent<Image>().sprite = potion.image;

            if (potion.rare)
                data.transform.Find("Rare").gameObject.SetActive(true);
            data.transform.Find("Rare").GetComponent<Image>().sprite = potion.rare;

            switch (potion.colored.Length)
            {
                case 2:
                    data.transform.Find("Duo1").gameObject.SetActive(true);
                    data.transform.Find("Duo1").GetComponent<Image>().sprite = potion.colored[0];
                    data.transform.Find("Duo2").gameObject.SetActive(true);
                    data.transform.Find("Duo2").GetComponent<Image>().sprite = potion.colored[1];
                    break;

                case 3:
                    data.transform.Find("Triple1").gameObject.SetActive(true);
                    data.transform.Find("Triple1").GetComponent<Image>().sprite = potion.colored[0];
                    data.transform.Find("Triple2").gameObject.SetActive(true);
                    data.transform.Find("Triple2").GetComponent<Image>().sprite = potion.colored[1];
                    data.transform.Find("Triple3").gameObject.SetActive(true);
                    data.transform.Find("Triple3").GetComponent<Image>().sprite = potion.colored[2];
                    break;

                case 4:
                    data.transform.Find("Quad1").gameObject.SetActive(true);
                    data.transform.Find("Quad1").GetComponent<Image>().sprite = potion.colored[0];
                    data.transform.Find("Quad2").gameObject.SetActive(true);
                    data.transform.Find("Quad2").GetComponent<Image>().sprite = potion.colored[1];
                    data.transform.Find("Quad3").gameObject.SetActive(true);
                    data.transform.Find("Quad3").GetComponent<Image>().sprite = potion.colored[2];
                    data.transform.Find("Quad4").gameObject.SetActive(true);
                    data.transform.Find("Quad4").GetComponent<Image>().sprite = potion.colored[3];
                    break;

                default:
                    break;
            }

            data.transform.SetParent(content.transform);
        }
    }
}