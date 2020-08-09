using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string FullName;
    public Sprite portrait;
    public Sprite angryPortrait;
    public Sprite happyPortrait;
    public Sprite unhappyPortrait;
    public Sprite sadPortrait;
    public Sprite confusedPortrait;
    public Sprite sillyPortrait;

}
