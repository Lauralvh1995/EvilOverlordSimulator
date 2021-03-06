﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public Minion minionPrefab;

    [SerializeField]
    MinionEvent minionRecruited;


    public void RecruitMinion()
    {
        Minion newMinion = Instantiate(minionPrefab);
        newMinion.transform.SetParent(transform);
        newMinion.transform.localPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);
        newMinion.GenerateName();
        newMinion.name = newMinion.GetName();

        minionRecruited.Invoke(newMinion);
    }
}
