using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public bool isPickable;
    public enum typeItem {
        Hammer, // for the puzzle 1
        Key, // for the puzzle 2
        Gun // for puzzle 6
    }
    public typeItem currentItem;
}
