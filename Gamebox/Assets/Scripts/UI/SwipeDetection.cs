﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetection : MonoBehaviour
{
    public GameObject detectTable;
    public GameObject detectOrders;
    public Camera cam;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            Touch touch = Input.GetTouch(0);

            Vector3 pos = cam.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit && hit.transform.CompareTag("TouchDetect"))
            {

                if (touch.phase == TouchPhase.Began)
                    firstPressPos = new Vector2(touch.position.x, touch.position.y);

                if (touch.phase == TouchPhase.Ended)
                {
                    secondPressPos = new Vector2(touch.position.x, touch.position.y);
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    if (currentSwipe.y < 100 && currentSwipe.y > -100)
                        return;

                    currentSwipe.Normalize();

                    if (currentSwipe.y > 0 && currentSwipe.x > -0.9f && currentSwipe.x < 0.9f)
                    {
                        if (GetComponent<CameraMovement>().dir==2)
                            GetComponent<CameraMovement>().MoveCam();
                    }

                    if (currentSwipe.y < 0 && currentSwipe.x > -0.9f && currentSwipe.x < 0.9f)
                    {
                        if (GetComponent<CameraMovement>().dir == 1)
                            GetComponent<CameraMovement>().MoveCam();
                    }
                }

            }
        }
    }
}