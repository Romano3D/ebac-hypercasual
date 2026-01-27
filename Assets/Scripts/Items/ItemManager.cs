using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class ItemManager : Singleton<ItemManager>
{ 
    public SOint coins;
    public SOint coinsSpecial;

    public TextMeshProUGUI uiTextcoins;
    public TextMeshProUGUI uiTextcoinsSpecial;
    private void Start()
    {
        Reset();
    }
    private void Reset()
    {
        coins.value = 0;
        coinsSpecial.value = 0;
        UpdateUI();
    }
    public void RegisterCoin(ItemCollactableCoin coin)
    {
        AddCoins(1);

        // Aqui você pode disparar animação, som, VFX etc
        // Ex: StartCoroutine(AnimateCoinToUI(coin.transform));
    }
    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
        UpdateUI();
    }
    public void AddCoinsSpecial(int amount = 3)
    {
        coinsSpecial.value += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //uiTextcoins.text = coins.value.ToString();
        //uiTextcoinsSpecial.text = coinsSpecial.value.ToString();
        //UIInGameManager.UpdateTexCoins(coins.ToString());
       // UIInGameManager.UpdateTexCoins(coinsSpecial.ToString());
    }
}
