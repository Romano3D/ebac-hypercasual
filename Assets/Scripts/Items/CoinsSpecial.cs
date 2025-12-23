using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpecial : ItemCollactableBase
{
    protected override void OnCollect()
    {
        ItemManager.Instance.AddCoinsSpecial(3);
        }
    }


