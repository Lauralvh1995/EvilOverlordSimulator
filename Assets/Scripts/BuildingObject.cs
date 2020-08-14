using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public Building content;

    [SerializeField]
    UnityEngine.Events.UnityEvent BuildingIsBuilt;

    [SerializeField]
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        BuildingIsBuilt?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool status)
    {
        active = status;
    }
}
