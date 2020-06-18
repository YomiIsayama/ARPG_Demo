﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea]
    public string itemInfo;
    public enum itemType
    {
        None,
        Equip,
        expendable
    }
    public itemType itype;

    public static implicit operator List<object>(Item v)
    {
        throw new NotImplementedException();
    }
}