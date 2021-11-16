using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCurrency : MonoBehaviour
{
    public int playerCurrency;
    public TMP_Text currencyText;
    public static PlayerCurrency instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    public void Update()
    {
        currencyText.text = playerCurrency.ToString();
    }
    public void GainCurrecy(int increaseAmount)
    {
        print("player gain currency from enemy");
        playerCurrency += increaseAmount;
    }
}
