using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;
    //private void Awake()
    //{
    //    if (instance == null)
    //        instance = this;
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //    DontDestroyOnLoad(gameObject);
    //}
    public TMP_Text currencyText;


    private void Update()
    {
        currencyText.text = PlayerCurrency.instance.currency.ToString();
    }
}
