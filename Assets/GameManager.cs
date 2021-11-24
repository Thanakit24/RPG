using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text currencyText;
    public TMP_Text interactText;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        currencyText.text = PlayerCurrency.instance.currency.ToString();
    }
}
