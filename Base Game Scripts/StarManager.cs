using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StarManager : MonoBehaviour
{
     public static StarManager Instance { get; private set; }

    private GameData gameData;
    public int star;
    //public TextMeshProUGUI starCounterText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        LoadStar();
    }

    private void Update(){
    }

    public void LoadStar(){
         // Здесь загружаем количество монет
        if (gameData != null)
        {
            star = gameData.saveData.starCounter;
        }
    }

    private void SaveStars()
    {
        gameData.saveData.starCounter = star;
        gameData.Save();
        Debug.Log("Сохранение завершено в GameData из StarManager");
        
    }


     // Метод для добавления звезд
    public void AddStars(int amount)
    {
        star += amount;              // Увеличиваем количество звезд
        SaveStars();                 // Сохраняем данные в GameData
    }

    // Метод для убавления звезд
    public void SubtractStars(int amount)
    {
        if (star >= amount)          // Проверяем, достаточно ли звезд для списания
        {
            star -= amount;          // Уменьшаем количество звезд
            //gameData.saveData.starCounter = star; // Обновляем сохраненное значение
            SaveStars();             // Сохраняем данные в GameData
        }
        else
        {
            Debug.Log("Недостаточно звезд для выполнения действия.");
        }
    }

   
}

