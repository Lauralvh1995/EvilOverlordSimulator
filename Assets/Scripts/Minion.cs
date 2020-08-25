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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
