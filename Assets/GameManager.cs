using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public TMP_Text currencyText;


    private void Update()
    {
        currencyText.text = PlayerCurrency.instance.currency.ToString();
    }
}
