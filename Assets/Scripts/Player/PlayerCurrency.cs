using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCurrency : MonoBehaviour
{
    public int currency;
    //public TMP_Text currencyText;
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
   
    public void GainCurrecy(int increaseAmount)
    {
        //print("player gain currency from enemy");
        currency += increaseAmount;
    }
    public void DecreaseCurrency(int decreaseAmount)
    {
        currency -= decreaseAmount;
    }
}