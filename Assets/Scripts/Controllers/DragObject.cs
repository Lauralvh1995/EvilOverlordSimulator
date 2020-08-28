using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public MouseHolder holder;
    public Minion minion;
    public Rigidbody rb;
    public LayerMask ground;
    //public LayerMask minionLayer;

    private bool IsGrounded;
    //private bool isDragged;
    private void Start()
    {
        minion = GetComponent<Minion>();
        holder = FindObjectOfType<MouseHolder>();
        rb = GetComponent<Rigidbody>();
    }

    //TODO: Check if they hit the floor
    private void Update()
    {
        IsGrounded = Physics.CheckSphere(transform.position, 0.04f, ground);
        minion.SetNavMeshAgentStatus(IsGrounded);
        /*

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100f, minionLayer))
            {
                if (hitInfo.transform.GetComponent<DragObject>())
                {
                    isDragged = true;
                    minion.SetNavMeshAgentStatus(false);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragged = false;
        }

        if (isDragged)
        {
            minion.transform.position = holder.transform.position;
        }
        */
    }

    public bool IsFalling()
    {
        return !IsGrounded;
    }

    void OnMouseDown()
    {
        minion.transform.position = holder.transform.position;
        minion.SetNavMeshAgentStatus(false);
    }

    void OnMouseDrag()
    {
        minion.transform.position = holder.transform.position;
        rb.velocity = Vector3.zero;
    }
}
