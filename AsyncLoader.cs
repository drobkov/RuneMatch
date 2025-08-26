using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class AsyncLoader : MonoBehaviour
{
    public Button startButton;
    public TextMeshProUGUI loadingText;
    public Slider loadingBar;

    private AsyncOperation asyncOperation;

    void Start()
    {
        // Запуск асинхронной загрузки основной сцены
        //StartCoroutine(LoadGameSceneAsync());
    
    }

    public void OpenNextScene(){
        SceneManager.LoadScene("Village");
    }

    IEnumerator LoadGameSceneAsync()
    {
        // Начинаем асинхронную загрузку основной сцены
        asyncOperation = SceneManager.LoadSceneAsync("Village");
        asyncOperation.allowSceneActivation = false; // Отключаем автоматическую активацию сцены

        // Пока сцена загружается, обновляем UI элементы
        while (!asyncOperation.isDone)
        {
            // Получаем прогресс загрузки (в диапазоне от 0.0 до 0.9)
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.value = progress;
            loadingText.text = "Загрузка... " + (progress * 100) + "%";

            yield return null;
        }
    }

    public void OnStartButtonClicked()
    {
        // Активируем загруженную сцену при нажатии на кнопку "Начать игру"
        asyncOperation.allowSceneActivation = true;
        //YandexGame.GameplayStart();
    }
}
