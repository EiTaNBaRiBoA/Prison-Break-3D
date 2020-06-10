using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Game/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public bool isPickable;
    public enum typeItem {
        Hammer, // for the puzzle 1
        Stone, // for the puzzle 3
        Card, // puzzle 4
        Key, // for the elevator puzzle 5
        CopClothes
    }
    public typeItem currentItem;
}
