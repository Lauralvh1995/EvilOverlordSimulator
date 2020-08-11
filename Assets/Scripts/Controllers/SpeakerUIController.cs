using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakerUIController : MonoBehaviour
{
    public Image portrait;
    public Text speakerName;
    public Text dialogue;

    private Character speaker;
    public Character Speaker
    {
        get { return speaker; }
        set { speaker = value;
            portrait.sprite = speaker.portrait;
            speakerName.text = speaker.FullName;
        }
    }

    public void SetPortrait(Sprite sprite)
    {
        portrait.sprite = sprite;
    }

    public string Dialogue
    {
        set { dialogue.text = value; }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLeft()
    {
        portrait.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-260, 50);
    }

    public void SetRight()
    {
        portrait.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(260, 50);
    }
    public void SetCenter()
    {
        portrait.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 50);
    }
}
