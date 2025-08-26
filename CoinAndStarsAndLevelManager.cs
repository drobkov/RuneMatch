using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinAndStarsManager : MonoBehaviour
{
    public TextMeshProUGUI stars;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI level;
    private GameData gameData;
    public int currentStars = 0;
    public int currentCoins = 0;
    
    public int currentLevel = 0;

    int lastIndexTrue = -1;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        
        if (gameData != null)
        {
            for (int i = 0; i < gameData.saveData.stars.Length; i++)
            {
                currentStars += gameData.saveData.stars[i];
            }

            //Тестовый цикл для монет. Сейчас считается игровой счёт вместо монет.  ЗАМЕНИТЬ.
             for (int i = 0; i < gameData.saveData.highScores.Length; i++)
            {
                currentCoins += gameData.saveData.highScores[i];
            }

            //Показываем уровень из gameData(Сохранений)
        
            for (int i = gameData.saveData.isActive.Length - 1; i >= 0; i--)
            {
                if (gameData.saveData.isActive[i])
                {
                    lastIndexTrue = i;
                    break;
                }
            }

            if (lastIndexTrue != -1)
            {
                
                currentLevel = lastIndexTrue + 1;
            }
            else
            {
                Debug.Log("В массиве нет элементов, равных true.");
            }
                        
                    }
    }

    // Update is called once per frame
    void Update()
    {
        stars.text = currentStars.ToString();
        coins.text = currentCoins.ToString();
        level.text = currentLevel.ToString();
    }
}
