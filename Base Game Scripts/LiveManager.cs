using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using TMPro;
using YG;

public class LiveManager : MonoBehaviour
{
    public static LiveManager Instance { get; private set; }
    public int maxLives = 5;
    public int currentLives;
    private long nextLifeRestoreTime;      // Время для восстановления следующей жизни (в миллисекундах)
    private const int restoreInterval = 20 * 60 * 1000; // Интервал восстановления (1 минута в миллисекундах)

    private GameData gameData;
    private DateTime lastLifeAddedTime;

    public string livesCounterText;
    public string livesCounterTimerText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //transform.parent = null; // Отсоединяем от любого родителя
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        InitializeLives();
        InvokeRepeating("UpdateLives", 1, 1); // Проверка восстановления каждую секунду
    }

    private void InitializeLives()
    {
        // Проверяем, был ли запуск игры первым
        if (!PlayerPrefs.HasKey("currentLives"))
        {
            // Если это первый запуск, устанавливаем максимальное количество жизней и обнуляем таймер восстановления
            currentLives = maxLives;
            nextLifeRestoreTime = 0;
            PlayerPrefs.SetInt("currentLives", currentLives);
            PlayerPrefs.SetString("nextLifeRestoreTime", nextLifeRestoreTime.ToString());
            PlayerPrefs.Save();
        }
        else
        {
            // Если это не первый запуск, загружаем сохраненные значения
            currentLives = PlayerPrefs.GetInt("currentLives", maxLives);
            nextLifeRestoreTime = long.Parse(PlayerPrefs.GetString("nextLifeRestoreTime", "0"));
        }

        UpdateUI(YandexGame.ServerTime());
    }

    private void UpdateLives()
    {
        // Получаем текущее серверное время
        long currentTime = YandexGame.ServerTime();
        
        if (currentLives < maxLives)
        {
            // Проверяем, прошло ли время восстановления
            while (currentTime >= nextLifeRestoreTime && currentLives < maxLives)
            {
                currentLives++;
                nextLifeRestoreTime += restoreInterval; // Устанавливаем время для следующего восстановления
            }

            // Сохраняем текущее количество жизней и время следующего восстановления
            PlayerPrefs.SetInt("currentLives", currentLives);
            PlayerPrefs.SetString("nextLifeRestoreTime", nextLifeRestoreTime.ToString());
            PlayerPrefs.Save();
        }

        UpdateUI(currentTime);
    }

    // Метод для обновления UI
    private void UpdateUI(long currentTime)
    {
        // Обновляем текст количества жизней
        livesCounterText = "" + currentLives.ToString();

        // Проверка на максимальное количество жизней
        if (currentLives == maxLives)
        {
            livesCounterTimerText = "Все";
        }
        else
        {
            // Расчет оставшегося времени до следующей жизни
            long timeRemaining = nextLifeRestoreTime - currentTime;
            if (timeRemaining > 0)
            {
                // Конвертируем оставшееся время в минуты и секунды
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeRemaining);
                livesCounterTimerText = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            }
            else
            {
                livesCounterTimerText = "Cкоро!";
            }
        }
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;

            // Если жизни меньше максимума, запускаем таймер на восстановление
            if (currentLives < maxLives)
            {
                long currentTime = YandexGame.ServerTime();
                nextLifeRestoreTime = currentTime + restoreInterval;
                PlayerPrefs.SetString("nextLifeRestoreTime", nextLifeRestoreTime.ToString());
            }

            PlayerPrefs.SetInt("currentLives", currentLives);
            PlayerPrefs.Save();
        }
    }

    public void AddLife()
    {
         if (currentLives < maxLives)
        {
            currentLives++;
            PlayerPrefs.SetInt("currentLives", currentLives);
            PlayerPrefs.Save();
        }
    }

    [ContextMenu("Reset Progress")] // Добавляем кнопку в инспектор
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("currentLives");
        PlayerPrefs.Save();
        InitializeLives();
        Debug.Log("Прогресс сброшен!");
    }

    

}
