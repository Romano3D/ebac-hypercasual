using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : ItemCollactableBase
{
    protected override void OnCollect()
    {
        ItemManager.Instance.AddCoins(1);
    }
}