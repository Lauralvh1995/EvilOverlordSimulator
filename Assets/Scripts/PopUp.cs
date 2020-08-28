using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField]
    private RectTransform rTransform;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Text status;
    [SerializeField]
    private Image minionPortrait;
    [SerializeField]
    private Text minionName;

    public void SetPopUpInfo(Vector3 position, string buildingName, string buildingDescription, bool status, Minion minion)
    {
        rTransform.position = new Vector3(position.x, 1f, position.z);
        title.text = buildingName;
        description.text = buildingDescription;
        if (status)
        {
            this.status.text = "Status: Active";
        }
        else
        {
            this.status.text = "Status: Inactive";
        }

        if (minion != null)
        {
            Debug.Log("This building is owned by: " + minion.GetName());
            minionPortrait.enabled = true;
            minionName.enabled = true;
            minionPortrait.sprite = minion.portrait;
            minionName.text = minion.GetName();

        }
        else
        {
            minionPortrait.enabled = false;
            minionName.enabled = false;
        }

        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
