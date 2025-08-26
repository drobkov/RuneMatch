using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using YG;
using System.Linq;

[Serializable]
public class SaveData {
    //public string playerId;  // Уникальный ID игрока
    //public int avatarId; //Аватар
    public bool[] isActive; // Активный уровень
    public int[] highScores; // Набранные очки. 
    public int[] stars;     // Звезды за уровень (до 3 звезд за прохождение). 

    // Счетчики звезд, денег и жизней.
    [Header("Счетчики")]
    public int starCounter; // Количество звезд
    public int money;       // Количество монет
    public int lives;       // Количество жизней

    [Header("Счетчики передающиеся в лидерборд и на страничку")]
    public int helpSendCounter; //Счетчик отправленной помощи
    public int helpGetCounter; //Счетчик полученной помощи
    public int level; // Номер уровня в int.
    public string newPlayerName;     // Имя игрока

    // Массив для сохранения состояния объектов
    [Header("Состояния восстановления объектов")]
    public bool[] scene1Buildings;         // Состояние зданий на сцене 1
    public bool[] scene2Buildings;         // Состояние зданий на сцене 2
    public bool[] scene3Buildings;         // Состояние зданий на сцене 3

    // Счетчики бустеров
    [Header("Счетчики для бустеров")]
    public int colorBombsCount;
    public int adjacentBombsCount;
    public int horizontalRocketsCount;
    public int verticalRocketsCount;
    public int hammersCount;
    public int shuffleBoardCount;

    // Счетчики для ивентов
    [Header("Счетчики для ивентов")]
    public int keyEvent;
    public bool stateKeyEvent;
    public bool isKeyEventShown;
    public int maxKeyEvent = 7;

    //[Header("Время последнего обновления")]
    //public long lastUpdatedTimestamp; // Время последнего обновления

    //[Header("Дата создания сохранений")]
    //public long creationTimeAccount;  // Дата и время создания аккаунта и первых сохранений
}

public class GameData : MonoBehaviour {
    public static GameData Instance;
    public SaveData saveData;

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }

        if (YandexGame.SDKEnabled == true)
		{
            Debug.Log("SDK Enabled");
				// Если запустился, то выполняем метод для загрузки
		    Load();

				// Если плагин еще не прогрузился, то метод не выполнится в методе,
				// но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }
       // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable()
    {
            YandexGame.GetDataEvent += Load;
            Debug.Log("OnEnable GetDataEvent");
    }

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable()
    {
            YandexGame.GetDataEvent -= Load;
            Debug.Log("OnDisable GetDataEvent");
    }   


[ContextMenu("Reset Progress")] // Добавляем кнопку в инспектор
public void ResetSaves(){
    YandexGame.ResetSaveProgress();
    YandexGame.SaveProgress();
    Debug.Log("Все сохранения из YG удалены!");
    PlayerPrefs.DeleteAll();
    PlayerPrefs.Save();
    Debug.Log("Все PlayerPrefs удалены!");
    
    // После удаления файла можно заново инициализировать данные
    InitializeDefaultValues();
}


    public void Save() {
        saveData.level = GetNextLevel(); //Обновляем уровень.
        
        if (YandexGame.savesData == null)
        {
        YandexGame.savesData = new YG.SavesYG();
        }   

        YandexGame.savesData.isActive = saveData.isActive;
        YandexGame.savesData.highScores = saveData.highScores;
        YandexGame.savesData.stars = saveData.stars;
        // Сохраняем показатели счетчиков
        YandexGame.savesData.starCounter = saveData.starCounter;
        YandexGame.savesData.money = saveData.money;
        YandexGame.savesData.lives = saveData.lives;

        //Сохраняем счетчики для странички
        YandexGame.savesData.helpSendCounter = saveData.helpSendCounter;
        YandexGame.savesData.helpGetCounter = saveData.helpGetCounter;
        YandexGame.savesData.level = saveData.level;
        YandexGame.savesData.newPlayerName = saveData.newPlayerName;
        
        
       
        YandexGame.savesData.scene1Buildings = saveData.scene1Buildings;
        YandexGame.savesData.scene2Buildings = saveData.scene2Buildings;
        YandexGame.savesData.scene3Buildings = saveData.scene3Buildings;

        //Сохраняем количество бустеров 
        YandexGame.savesData.colorBombsCount = saveData.colorBombsCount;
        YandexGame.savesData.adjacentBombsCount = saveData.adjacentBombsCount;
        YandexGame.savesData.horizontalRocketsCount = saveData.horizontalRocketsCount;
        YandexGame.savesData.verticalRocketsCount = saveData.verticalRocketsCount;
        YandexGame.savesData.hammersCount = saveData.hammersCount;
        YandexGame.savesData.shuffleBoardCount = saveData.shuffleBoardCount;

        //Сохраняем данные по ивентам
        YandexGame.savesData.keyEvent = saveData.keyEvent;
        YandexGame.savesData.stateKeyEvent = saveData.stateKeyEvent;
        YandexGame.savesData.isKeyEventShown = saveData.isKeyEventShown;

        

        YandexGame.SaveProgress();
        Debug.Log("Сохранение завершено в YandexGame.savesData");
    }

    public void Load() {
        if (YandexGame.savesData.isActive[0] == true)
        {
            saveData.isActive = YandexGame.savesData.isActive;
            saveData.highScores = YandexGame.savesData.highScores;
            saveData.stars= YandexGame.savesData.stars;
            // Загружаем показатели счетчиков
            saveData.starCounter = YandexGame.savesData.starCounter;
            saveData.money = YandexGame.savesData.money;
            saveData.lives = YandexGame.savesData.lives;
            //Загружаем показатели счетчика для аккаунта
            saveData.helpSendCounter = YandexGame.savesData.helpSendCounter;
            saveData.helpGetCounter = YandexGame.savesData.helpGetCounter;
            saveData.level = YandexGame.savesData.level;
            saveData.newPlayerName = YandexGame.savesData.newPlayerName;
            //Загружаем восстановленные объекты.
            saveData.scene1Buildings = YandexGame.savesData.scene1Buildings;
            saveData.scene2Buildings = YandexGame.savesData.scene2Buildings;
            saveData.scene3Buildings = YandexGame.savesData.scene3Buildings;

            //Загружаем количество бустеров
            saveData.colorBombsCount = YandexGame.savesData.colorBombsCount;
            saveData.adjacentBombsCount = YandexGame.savesData.adjacentBombsCount;
            saveData.horizontalRocketsCount = YandexGame.savesData.horizontalRocketsCount;
            saveData.verticalRocketsCount = YandexGame.savesData.verticalRocketsCount;
            saveData.hammersCount = YandexGame.savesData.hammersCount;
            saveData.shuffleBoardCount = YandexGame.savesData.shuffleBoardCount;

            //Загружаем данные по ивентам
             saveData.keyEvent = YandexGame.savesData.keyEvent;
             saveData.stateKeyEvent = YandexGame.savesData.stateKeyEvent;
             saveData.isKeyEventShown = YandexGame.savesData.isKeyEventShown;
            
            Debug.Log("Loaded from YG!");
        }else{
            InitializeDefaultValues();
            Debug.Log("Initialize Default Values.");
        }
    }


    private void InitializeDefaultValues() {
        saveData = new SaveData();
        //saveData.avatarId = 1;
        saveData.isActive = new bool[100]; // Создает файл где максимум 100 уровней.
        saveData.stars = new int[100];
        saveData.highScores = new int[100];
        saveData.isActive[0] = true; // 1 уровень всегда будет работать, даже если файл сохранения не создан.
        // Загружаем дефолтные показатели счетчиков в случае если нет сохранений.
        saveData.money = 1000;
        saveData.lives = 5;
        saveData.starCounter = 0;

        //Показатели, которые передаются в другую базу. 
        saveData.helpSendCounter = 0; //Счетчик отправленной помощи
        saveData.helpGetCounter = 0; //Счетчик полученной помощи
        saveData.level = GetNextLevel(); //Счетчик уровней. 
        saveData.newPlayerName = "Игрок"; // Имя игрока. Стандартно.


        // Загружаем дефолтные значения объектов на локациях
        saveData.scene1Buildings = new bool[5]; // 5 зданий на сцене 1
        saveData.scene2Buildings = new bool[5]; // 5 объектов на сцене 2
        saveData.scene3Buildings = new bool[5]; // 5 объектов на сцене 3
        // Загружаем дефолтные значения бустеров
        saveData.colorBombsCount = 0;
        saveData.adjacentBombsCount = 0;
        saveData.horizontalRocketsCount = 0;
        saveData.verticalRocketsCount = 0;
        saveData.hammersCount = 0;
        saveData.shuffleBoardCount = 2;
        // Загружаем дефолтные значения ивентов.
        saveData.keyEvent = 0;
        saveData.stateKeyEvent = true;
        saveData.isKeyEventShown = true;
       
       Debug.Log("InitializeDefaultValues loaded");
        Save();
        Debug.Log("Save GameData after Default");
       
    }

    //Получение номера уровня в виде int.
public int GetNextLevel()
{
    int level = 1;
    // Найдем последний активный уровень
    for (int i = 0; i < saveData.isActive.Length; i++)
    {
        if (saveData.isActive[i])
        {
            level = i;
        }
    }

    // Возвращаем следующий уровень
    return level + 1;
}
}

