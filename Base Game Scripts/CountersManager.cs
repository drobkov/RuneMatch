using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountersManager : MonoBehaviour
{

    [Header("Money Counters")]
    public TextMeshProUGUI moneyCounterText;
    public TextMeshProUGUI moneyCounterTextInShop;
    public TextMeshProUGUI moneyCounterTextInSpecOffer;

    [Header("Star counter")]
    public TextMeshProUGUI starCounterText;
    
    [Header("Lives counter")]
    public TextMeshProUGUI livesCounterText;
    public TextMeshProUGUI livesCounterTimerText;


    void Update()
    {
        moneyCounterText.text = "" + MoneyManager.Instance.money;
        moneyCounterTextInShop.text = "" + MoneyManager.Instance.money;;
        moneyCounterTextInSpecOffer.text = "" + MoneyManager.Instance.money;

        starCounterText.text = "" + StarManager.Instance.star;

        // Обновляем текст количества жизней
        livesCounterText.text = "" + LiveManager.Instance.currentLives.ToString();
        livesCounterTimerText.text = "" + LiveManager.Instance.livesCounterTimerText.ToString();
    }
}
