using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public MouseHolder holder;
    public Minion minion;
    public Rigidbody rb;
    public LayerMask ground;

    private bool IsGrounded;
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
