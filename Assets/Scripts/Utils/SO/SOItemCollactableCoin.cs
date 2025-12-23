using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOItemCollactableCoin : MonoBehaviour
{
    public enum ItemType
    {
        Coin,
        CoinSpecial
    }

    [CreateAssetMenu(menuName = "Items/Item Collectable")]
    public class SOItemCollectable : ScriptableObject
    {
        public ItemType itemType;
        public int value = 1;
    }
}
