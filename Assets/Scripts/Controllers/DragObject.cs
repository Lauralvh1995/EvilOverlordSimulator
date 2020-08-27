using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public MouseHolder holder;
    public Minion minion;
    public Rigidbody rb;
    private void Start()
    {
        minion = GetComponent<Minion>();
        holder = FindObjectOfType<MouseHolder>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(rb.velocity.y < 0.1f)
        {
            minion.SetNavMeshAgentStatus(enabled);
        }
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
    private void OnMouseUp()
    {
        minion.SetNavMeshAgentStatus(true);
    }
}
