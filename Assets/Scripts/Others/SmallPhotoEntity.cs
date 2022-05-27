using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SmallPhotoEntity", menuName = ">> Create SmallPhotoEntity")]

public class SmallPhotoEntity : ScriptableObject
{
    public new string name;
    public int photoId;
    public Sprite photoSprite;

}
