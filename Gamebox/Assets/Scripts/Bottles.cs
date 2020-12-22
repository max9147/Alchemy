using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bottles : MonoBehaviour
{
    public Camera cam;

    public Sprite bottleFull;
    public Sprite bottleEmpty;

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

    private Touch touch;
    private Transform toDrag;
    private Rigidbody2D toDragRB;

    private bool isDragging = false;
    private bool canTake = true;

    private float time = 1;

    public int bottleCount = 2;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;

                Vector3 pos = cam.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Bottle") && canTake)
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
                toDrag.GetComponent<BottlePotion>().AddPotion(cauldron.GetComponent<MixingSystem>().inCauldron, 
                    cauldron.GetComponent<MixingSystem>().inCauldronColored, cauldron.GetComponent<MixingSystem>().isRare);

                toDrag.GetComponent<SpriteRenderer>().sprite = bottleFull;
                toDrag.GetComponent<SpriteRenderer>().color = water.GetComponent<SpriteRenderer>().color;
                cauldron.GetComponent<MixingSystem>().TakePotion();
            }

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
