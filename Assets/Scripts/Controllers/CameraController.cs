using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float cameraMoveSpeed = 5f;
    public int maxDistanceFromOrigin = 25;
    public int margin = 4;
    int horizontalMargin;

    public int minZoom = 1;
    public int maxZoom = 6;

    private bool isAllowedToMove = true;
    private void Awake()
    {
        horizontalMargin = margin * 2;
    }
    private void Update()
    {
        if (isAllowedToMove)
        {
            Vector2 mouseEdge = MouseScreenEdge(20);

            if (!Mathf.Approximately(mouseEdge.x, 0f))
            {
                //Move your camera depending on the sign of mouse.Edge.x
                if (mouseEdge.x < 0)
                {
                    if (transform.position.x > -maxDistanceFromOrigin + horizontalMargin)
                        transform.Translate(Vector3.left * Time.deltaTime * cameraMoveSpeed);
                }
                else
                {
                    if (transform.position.x < maxDistanceFromOrigin - horizontalMargin)
                        transform.Translate(Vector3.right * Time.deltaTime * cameraMoveSpeed);
                }

            }

            if (!Mathf.Approximately(mouseEdge.y, 0f))
            {
                //Move your camera depending on the sign of mouse.Edge.y
                if (mouseEdge.y < 0)
                {
                    if (transform.position.z > -maxDistanceFromOrigin + margin)
                        transform.Translate(Vector3.back * Time.deltaTime * cameraMoveSpeed);
                }
                else
                {
                    if (transform.position.z < maxDistanceFromOrigin - margin)
                        transform.Translate(Vector3.forward * Time.deltaTime * cameraMoveSpeed);
                }
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                if (mainCamera.orthographicSize < maxZoom)
                    mainCamera.orthographicSize++;
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                if (mainCamera.orthographicSize > minZoom)
                    mainCamera.orthographicSize--;
            }
        }
    }
    Vector2 MouseScreenEdge(int margin)
    {
        //Margin is calculated in px from the edge of the screen

        Vector2 half = new Vector2(Screen.width / 2, Screen.height / 2);

        //If mouse is dead center, (x,y) would be (0,0)
        float x = Input.mousePosition.x - half.x;
        float y = Input.mousePosition.y - half.y;

        //If x is not within the edge margin, then x is 0;
        //In another word, not close to the edge
        if (Mathf.Abs(x) > half.x - margin)
        {
            x += ((half.x - margin) * x) < 0 ? 1 : -1;
        }
        else
        {
            x = 0f;
        }

        if (Mathf.Abs(y) > half.y - margin)
        {
            y += ((half.y - margin) * y) < 0 ? 1 : -1;
        }
        else
        {
            y = 0f;
        }

        return new Vector2(x, y);
    }

    public void EnableMovement(bool status)
    {
        isAllowedToMove = status;
    }
}
