using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bottles : MonoBehaviour
{
    public Camera cam;

    public Button drainButton;

    public Sprite bottleFull;
    public Sprite bottleEmpty;
    public Sprite effectFire;
    public Sprite effectSmoke;
    public Sprite effectGlow;
    public Sprite effectBoil;

    public GameObject bottle1Pos;
    public GameObject bottle2Pos;
    public GameObject bottle3Pos;
    public GameObject bottle4Pos;
    public GameObject bottle5Pos;
    public GameObject bottle6Pos;
    public GameObject bottle7Pos;
    public GameObject bottle8Pos;
    public GameObject bottle1;
    public GameObject bottle2;
    public GameObject bottle3;
    public GameObject bottle4;
    public GameObject bottle5;
    public GameObject bottle6;
    public GameObject bottle7;
    public GameObject bottle8;
    public GameObject cauldron;
    public GameObject UIControls;
    public GameObject water;
    public GameObject potionSystem;

    private Touch touch;
    private Transform toDrag;
    private Rigidbody2D toDragRB;

    private bool isDragging = false;
    private bool canTake = true;
    private bool justTook = false;

    private float time = 1;

    public int bottleCount = 2;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && canTake)
            {
                isDragging = true;

                Vector3 pos = cam.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Bottle"))
                {
                    time = 0;
                    canTake = false;
                    toDrag = hit.collider.gameObject.transform;

                    if (toDrag != null)
                        toDragRB = toDrag.GetComponent<Rigidbody2D>();
                }
            }
            time += Time.deltaTime;
            if (touch.phase == TouchPhase.Moved && isDragging)
            {
                if (toDrag != null)
                {
                    toDragRB.simulated = false;
                    toDrag.position = cam.ScreenToWorldPoint(touch.position);
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (time < 0.1f)
                    UIControls.GetComponent<CameraMovement>().MoveCam();
                time = 1;

                isDragging = false;
                if (toDrag != null)
                {
                    StartCoroutine(Wait());
                    toDragRB.simulated = true;
                }
            }
        }

        if (Input.touchCount == 0 && toDrag)
        {
            if (cauldron.GetComponent<MixingSystem>().isReady && cauldron.GetComponent<MixingSystem>().bottleIn)
            {
                justTook = true;
                toDrag.GetComponent<BottlePotion>().AddPotion(cauldron.GetComponent<MixingSystem>().inCauldron, 
                    cauldron.GetComponent<MixingSystem>().inCauldronColored, cauldron.GetComponent<MixingSystem>().isRare);

                toDrag.GetComponent<SpriteRenderer>().sprite = bottleFull;
                toDrag.GetComponent<SpriteRenderer>().color = water.GetComponent<SpriteRenderer>().color;

                switch (cauldron.GetComponent<MixingSystem>().GetBrewEffect())
                {
                    case PotionEffect.Glowing:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectGlow;
                        break;
                    case PotionEffect.Boiling:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectBoil;
                        break;
                    case PotionEffect.Burning:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectFire;
                        break;
                    case PotionEffect.Smoking:
                        toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = effectSmoke;
                        break;
                    default:
                        break;
                }

                cauldron.GetComponent<MixingSystem>().TakePotion();
            }

            if (cauldron.GetComponent<MixingSystem>().bottleIn && cauldron.GetComponent<MixingSystem>().inCauldron.Count == 0 && toDrag.GetComponent<BottlePotion>().potionColor != PotionColor.Empty && !justTook)
            {
                switch (toDrag.GetComponent<BottlePotion>().potionColor)
                {
                    case PotionColor.Black:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[4];
                        break;
                    case PotionColor.Gray:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[5];
                        break;
                    case PotionColor.Purple:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[6];
                        break;
                    case PotionColor.Orange:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[7];
                        break;
                    case PotionColor.Green:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[8];
                        break;
                    case PotionColor.Violet:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[9];
                        break;
                    case PotionColor.LightOrange:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[10];
                        break;
                    case PotionColor.Lime:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[11];
                        break;
                    case PotionColor.Pink:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Red);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[12];
                        break;
                    case PotionColor.LightBlue:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Blue);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[13];
                        break;
                    case PotionColor.Gold:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.White);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.Yellow);
                        cauldron.GetComponent<MixingSystem>().inCauldronColored.Add(Resource.White);
                        water.GetComponent<WaterColor>().targetColor = water.GetComponent<WaterColor>().colors[14];
                        break;
                    default:
                        break;
                }

                switch (toDrag.GetComponent<BottlePotion>().potionEffect)
                {
                    case PotionEffect.Glowing:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Ladan);
                        break;
                    case PotionEffect.Boiling:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Eye);
                        break;
                    case PotionEffect.Burning:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Stone);
                        break;
                    case PotionEffect.Smoking:
                        cauldron.GetComponent<MixingSystem>().inCauldron.Add(Resource.Sand);
                        break;
                    default:
                        break;
                }

                toDrag.Find("Effect").GetComponent<SpriteRenderer>().sprite = null;
                toDrag.GetComponent<SpriteRenderer>().sprite = bottleEmpty;
                toDrag.GetComponent<SpriteRenderer>().color = Color.white;
                toDrag.GetComponent<BottlePotion>().potionColor = PotionColor.Empty;
                toDrag.GetComponent<BottlePotion>().potionEffect = PotionEffect.Empty;
                drainButton.interactable = true;

                switch (toDrag.tag)
                {
                    case "Bottle1":
                        potionSystem.GetComponent<PotionSystem>().SetColor(0, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(0, PotionEffect.Empty);
                        break;
                    case "Bottle2":
                        potionSystem.GetComponent<PotionSystem>().SetColor(1, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(1, PotionEffect.Empty);
                        break;
                    case "Bottle3":
                        potionSystem.GetComponent<PotionSystem>().SetColor(2, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(2, PotionEffect.Empty);
                        break;
                    case "Bottle4":
                        potionSystem.GetComponent<PotionSystem>().SetColor(3, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(3, PotionEffect.Empty);
                        break;
                    case "Bottle5":
                        potionSystem.GetComponent<PotionSystem>().SetColor(4, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(4, PotionEffect.Empty);
                        break;
                    case "Bottle6":
                        potionSystem.GetComponent<PotionSystem>().SetColor(5, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(5, PotionEffect.Empty);
                        break;
                    case "Bottle7":
                        potionSystem.GetComponent<PotionSystem>().SetColor(6, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(6, PotionEffect.Empty);
                        break;
                    case "Bottle8":
                        potionSystem.GetComponent<PotionSystem>().SetColor(7, PotionColor.Empty);
                        potionSystem.GetComponent<PotionSystem>().SetEffect(7, PotionEffect.Empty);
                        break;
                    default:
                        break;
                }
            }
        }

        if (toDrag && !isDragging)
        {
            switch (toDrag.tag)
            {
                case "Bottle1":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle1Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle1Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                case "Bottle2":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle2Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle2Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                case "Bottle3":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle3Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle3Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                case "Bottle4":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle4Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle4Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                case "Bottle5":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle5Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle5Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                case "Bottle6":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle6Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle6Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                case "Bottle7":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle7Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle7Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                case "Bottle8":
                    toDrag.position = Vector3.MoveTowards(toDrag.position, bottle8Pos.transform.position, 0.3f);
                    if (toDrag.position == bottle8Pos.transform.position)
                    {
                        canTake = true;
                        toDrag = null;
                        toDragRB.simulated = true;
                        toDragRB = null;
                        justTook = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.02f);

        if (toDragRB)
            toDragRB.simulated = false;
    }

    public void AddBottle()
    {
        bottleCount++;

        switch (bottleCount)
        {
            case 1:
                bottle1.SetActive(true);
                break;

            case 2:
                bottle2.SetActive(true);
                break;

            case 3:
                bottle3.SetActive(true);
                break;

            case 4:
                bottle4.SetActive(true);
                break;

            case 5:
                bottle5.SetActive(true);
                break;

            case 6:
                bottle6.SetActive(true);
                break;

            case 7:
                bottle7.SetActive(true);
                break;

            case 8:
                bottle8.SetActive(true);
                break;

            default:
                break;
        }
    }

    public int GetBottleCount()
    {
        return bottleCount;
    }
}
