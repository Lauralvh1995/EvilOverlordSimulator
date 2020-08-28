using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Minion : MonoBehaviour
{
    [SerializeField]
    string minionName = "Hank";
    private MinionNameGenerator generator;
    public Sprite portrait;
    public Canvas nametag;
    public Text nametagText;

    [Range(0, 1), SerializeField]
    private float loyalty = 0.5f;
    [Range(0, 1), SerializeField]
    private float happiness = 0.5f;

    private float happinessFloor = 0f;

    [SerializeField]
    Event dayTick;
    [SerializeField]
    IntEvent getPaid;
    [SerializeField]
    Event weekTick;

    public float wanderRadius;
    public NavMeshAgent agent;

    public BuildingObject house;
    public BuildingObject workplace;
    private void Awake()
    {
        generator = new MinionNameGenerator();
        nametag.worldCamera = Camera.main;
    }

    public float GetHappiness()
    {
        return happiness;
    }

    public void SetNavMeshAgentStatus(bool status)
    {
        agent.enabled = status;
    }

    public void GenerateName()
    {
        minionName = generator.GenerateName();
        nametagText.text = minionName;
    }

    public float GetLoyalty()
    {
        return loyalty;
    }
    public string GetName()
    {
        return minionName;
    }

    void OnEnable()
    {
        dayTick.AddListener(SetNewDestination);
        weekTick.AddListener(RollForEvent);
        dayTick.AddListener(UpdateScores);
        getPaid.AddListener(ReceivePayment);
    }
    private void OnDisable()
    {
        dayTick.RemoveListener(SetNewDestination);
        weekTick.RemoveListener(RollForEvent);
        dayTick.RemoveListener(UpdateScores);
        getPaid.RemoveListener(ReceivePayment);
    }
    void SetNewDestination()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        if(agent.enabled == true)
            agent.SetDestination(newPos);
    }
    void RollForEvent()
    {
        float rando = UnityEngine.Random.Range(0, 100) / 100f;
        float stabilityFactor = Player.GetStability() / 100f;
        float ppFactor = Player.GetPowerProjection() / 100f;
        if (rando - stabilityFactor - ppFactor > loyalty + happiness)
        {
            Debug.Log(minionName + " thinks you're not a good Overlord");
        }
        else
        {
            Debug.Log(minionName + " is very happy with you");
        }
    }
    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;


        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

        return navHit.position;
    }
    void UpdateScores()
    {
        if (Player.GetPowerProjection() > loyalty * 100)
        {
            loyalty += 0.1f;
        }
        else
        {
            loyalty -= 0.1f;
        }

        if(Player.GetFood() > Player.GetMinionCount())
        {
            happiness += 0.1f;
        }
        else
        {
            happiness -= 0.2f;
            if(happiness < happinessFloor)
            {
                happiness = happinessFloor;
            }
        }

        if(house != null)
        {
            happinessFloor = 0.3f;
        }
        else
        {
            happinessFloor = 0f;
        }
    }
    void ReceivePayment(int amount)
    {
        if(amount > 0)
        {
            happiness += 0.1f;
            loyalty += 0.1f;
        }
        else
        {
            happiness -= 0.1f;
            loyalty -= 0.2f;
            if (happiness < happinessFloor)
            {
                happiness = happinessFloor;
            }
        }
    }
}
