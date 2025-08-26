using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;

    // Номер текущей сцены (где сейчас игрок)
    public int currentSceneIndex = 1;

    // Максимально доступная сцена (прогресс игрока)
    public int maxUnlockedScene = 1;
    
    [SerializeField]
    private LevelLoader levelLoader;

    private void Awake()
    {
        // Реализация Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликат, если он уже есть
        }

        // Загружаем сохраненный прогресс при запуске игры
        LoadProgress();
        
        // Загружаем актуальную сцену
        //LoadScene(currentSceneIndex);
    }

    // Переход на следующую сцену, если задания текущей сцены завершены
    public void UnlockNextScene()
    {
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings) // Проверяем, чтобы не выйти за пределы
        {
            // Обновляем максимальную доступную сцену
            maxUnlockedScene = Mathf.Max(maxUnlockedScene, currentSceneIndex + 1);
            currentSceneIndex++; // Увеличиваем индекс текущей сцены

            // Переходим на следующую сцену, если потребуется
            //LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("Все сцены завершены!");
        }
    }

    
    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex <= maxUnlockedScene) // Проверяем доступность сцены
        {
            currentSceneIndex = sceneIndex;
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("Эта сцена еще недоступна!");
        }
    }

    
    public void LoadCurrentScene()
    {
        if (levelLoader != null)
        {
            levelLoader.StartLoadLevelAnimation();
        }
        
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    
    private void SaveProgress()
    {
        PlayerPrefs.SetInt("CurrentScene", currentSceneIndex);
        PlayerPrefs.SetInt("MaxUnlockedScene", maxUnlockedScene);
        PlayerPrefs.Save();
    }

    
    private void LoadProgress()
    {
        currentSceneIndex = PlayerPrefs.GetInt("CurrentScene", 1); // По умолчанию — 1 Village
        maxUnlockedScene = PlayerPrefs.GetInt("MaxUnlockedScene", 1); // По умолчанию — 1 Village
    }

    [ContextMenu("Reset Progress")] // Добавляем кнопку в инспектор
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("CurrentScene");
        PlayerPrefs.DeleteKey("MaxUnlockedScene");

        currentSceneIndex = 1;
        maxUnlockedScene = 1;

        SaveProgress();
        //LoadScene(1);
        SceneManager.LoadScene("LoadingScene");

        Debug.Log("Прогресс сброшен!");
    }
}
