using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHolder : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, new Vector3(0, 2, 0));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance);
            }
        }
    }
}
