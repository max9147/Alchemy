using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColor : MonoBehaviour
{
    public Color[] colors;

    private Color startColor;
    public Color targetColor;
    private float time = 0.01f;

    private void Start()
    {
        startColor = GetComponent<SpriteRenderer>().color;
        targetColor = startColor;
    }

    public void ChangeColor(List<Resource> components, bool isWrong)
    {
        if (isWrong)
            targetColor = startColor;

        else
        {
            switch (components.Count)
            {
                case 1:
                    switch (components[0])
                    {
                        case Resource.Red:
                            targetColor = colors[0];
                            break;

                        case Resource.Blue:
                            targetColor = colors[1];
                            break;

                        case Resource.Yellow:
                            targetColor = colors[2];
                            break;

                        case Resource.White:
                            targetColor = colors[3];
                            break;

                        default:
                            break;
                    }
                    break;

                case 2:
                    if (components.Contains(Resource.Red) && components.Contains(Resource.Blue))
                        targetColor = colors[6];

                    if (components.Contains(Resource.Red) && components.Contains(Resource.Yellow))
                        targetColor = colors[7];

                    if (components.Contains(Resource.Blue) && components.Contains(Resource.Yellow))
                        targetColor = colors[8];

                    if (components.Contains(Resource.Red) && components.Contains(Resource.White))
                        targetColor = colors[12];

                    if (components.Contains(Resource.Blue) && components.Contains(Resource.White))
                        targetColor = colors[13];

                    if (components.Contains(Resource.Yellow) && components.Contains(Resource.White))
                        targetColor = colors[14];
                    break;

                case 3:
                    if (components.Contains(Resource.Red) && components.Contains(Resource.Blue) && components.Contains(Resource.Yellow))
                        targetColor = colors[4];

                    if (components.Contains(Resource.Red) && components.Contains(Resource.Blue) && components.Contains(Resource.White))
                        targetColor = colors[9];

                    if (components.Contains(Resource.Red) && components.Contains(Resource.Yellow) && components.Contains(Resource.White))
                        targetColor = colors[10];

                    if (components.Contains(Resource.Blue) && components.Contains(Resource.Yellow) && components.Contains(Resource.White))
                        targetColor = colors[11];
                    break;

                case 4:
                    targetColor = colors[5];
                    break;

                default:
                    break;
            }
        }
    }

    public void ClearWater()
    {
        targetColor = startColor;
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, targetColor, time);
    }
}
