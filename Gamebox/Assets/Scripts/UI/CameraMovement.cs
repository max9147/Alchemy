using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;

    public GameObject tableUI;
    public GameObject tableBG;
    public GameObject ordersBG;

    public AnimationCurve curve;

    public int dir = 1;
    public float moveTime = 0.3f;

    private Vector3 tablePos;
    private Vector3 ordersPos;
    private Vector3 destination;

    private float timer = 0f;
    private bool isMoving = false;

    private Touch touch;

    private void Start()
    {
        tablePos = tableBG.transform.position;
        ordersPos = ordersBG.transform.position;
    }

    public void MoveCam()
    {
        if (!isMoving)
        {
            if (dir == 1)
            {
                tableUI.SetActive(false);
                destination = ordersPos;
            }

            if (dir == 2)
            {
                tableUI.SetActive(true);
                destination = tablePos;
            }

            timer = 0f;
            isMoving = true;
        }
    }

    private void Update()
    {
        GetComponent<SwipeDetection>().Swipe();

        if (isMoving)
        {
            timer += Time.deltaTime;
            float ratio = timer / moveTime;

            Vector3 position = curve.Evaluate(ratio) * ordersPos;

            if (destination.y == ordersPos.y)
                cam.transform.position = position;
            else
                cam.transform.position = ordersPos - position;
        }

        if (destination.y == tablePos.y)
        {
            if (cam.transform.position.y == tablePos.y)
            {
                isMoving = false;
                dir = 1;
            }
        }

        if (destination.y == ordersPos.y)
        {
            if (cam.transform.position.y == ordersPos.y)
            {
                isMoving = false;
                dir = 2;
            }
        }

        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 pos = cam.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (Physics2D.Raycast(pos, Vector2.zero) && hit.collider.gameObject==this.gameObject)
                {
                    MoveCam();
                }
            }
        }
    }
}