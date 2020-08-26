using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePopUp : MonoBehaviour
{
    [SerializeField]
    private RectTransform rTransform;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Button recruitMinion;

    public void SetPopUpInfo(Vector3 position, string buildingName, string buildingDescription)
    {
        rTransform.position = new Vector3(position.x, 1f, position.z);
        title.text = buildingName;
        description.text = buildingDescription;

        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
