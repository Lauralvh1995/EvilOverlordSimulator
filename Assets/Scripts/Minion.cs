using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : MonoBehaviour
{
    public int Loyalty { get; private set; }
    public int Happiness { get; private set; }

    [SerializeField]
    Event dayTick;
    [SerializeField]
    IntEvent getPaid;

    public float wanderRadius;
    private NavMeshAgent agent;

    // Use this for initialization
    void OnEnable()
    {
        dayTick.AddListener(SetNewDestination);
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnDisable()
    {
        dayTick.RemoveListener(SetNewDestination);
    }
    void SetNewDestination()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
