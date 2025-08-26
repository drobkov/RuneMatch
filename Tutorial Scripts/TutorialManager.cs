using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialSteps; // Массив шагов туториала
    public GameObject hand;
    public GameObject hand2;
    public GameObject taskWindow;
    private int currentStep = 0; // Индекс текущего шага

    void Start()
    {
       if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 0)
        {
            ShowStep(0); // Начать обучение
        }
    }

    // Показать текущий шаг с анимацией появления
    public void ShowStep(int stepIndex)
    {
        // Скрыть все шаги
        foreach (GameObject step in tutorialSteps)
        {
            if (step != null)
            {
                step.SetActive(false);
            }
        }

        // Если шаг есть, активируем его и запускаем анимацию
        if (stepIndex < tutorialSteps.Length)
        {
            GameObject currentStepObject = tutorialSteps[stepIndex];
            currentStepObject.SetActive(true);

            // Анимация появления (fade-in и scale)
            CanvasGroup canvasGroup = currentStepObject.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0; // Начальная прозрачность
                canvasGroup.DOFade(1, 0.5f); // Плавное появление за 0.5 секунды
            }

            RectTransform rectTransform = currentStepObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.localScale = Vector3.zero; // Начальный масштаб
                rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); // Увеличение до нормального размера
            }

            currentStep = stepIndex; // Обновляем текущий шаг
        }
    }

    // Переход к следующему шагу
    public void NextStep()
    {
        // Отключаем текущий шаг с анимацией исчезновения
        if (currentStep < tutorialSteps.Length)
        {
            GameObject lastStep = tutorialSteps[currentStep];
            CanvasGroup canvasGroup = lastStep.GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                // Плавное исчезновение
                canvasGroup.DOFade(0, 0.5f).OnComplete(() => lastStep.SetActive(false));
            }
            else
            {
                lastStep.SetActive(false);
            }
        }

        // Переходим к следующему шагу
        currentStep++;

        if (currentStep < tutorialSteps.Length)
        {
            // Показываем следующий шаг
            ShowStep(currentStep);
           
           if (hand.activeSelf)
        {
            StartPulseAnimation(hand); // Включаем анимацию, если объект активен
            taskWindow.SetActive(true);
            //StartPulseAnimation(taskWindow);

        }
        else
        {
            Debug.Log("Объект hand не активен, анимация не запущена."); // Для отладки
        }

        if (hand2.activeSelf)
        {
            taskWindow.SetActive(false);
            //DOTween.KillAll();
            StartPulseAnimation(hand2); // Включаем анимацию, если объект активен
        }
        else
        {
            Debug.Log("Объект hand2 не активен, анимация не запущена."); // Для отладки
        }

        }
        else
        {
            DOTween.KillAll();
            // Если шагов больше нет, завершаем туториал
            Debug.Log("Tutorial completed!");
            PlayerPrefs.SetInt("TutorialCompleted", 1); // Сохраняем прогресс
            SkipTutorial();
            gameObject.SetActive(false); // Отключаем TutorialManager
        }
    }

    public IEnumerator Wait(float seconds){
        yield return new WaitForSeconds(seconds);
    }

    // Пропустить туториал
    public void SkipTutorial()
    {
        // Скрыть все шаги
        foreach (GameObject step in tutorialSteps)
        {
            if (step != null)
            {
                step.SetActive(false);
            }
        }

        // Сохраняем состояние, что обучение завершено
        PlayerPrefs.SetInt("TutorialCompleted", 1);
        gameObject.SetActive(false); // Отключаем TutorialManager
    }

    // Метод для запуска пульсации объекта
    public void StartPulseAnimation(GameObject targetObject)
    {
        RectTransform rectTransform = targetObject.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Начинаем бесконечную пульсацию
            rectTransform.DOScale(1.4f, 0.5f) // Увеличиваем масштаб до 1.1
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); // Плавное уменьшение и увеличение
        }
    }


    [ContextMenu("Reset Progress")] // Добавляем кнопку в инспектор
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("TutorialCompleted");
        PlayerPrefs.Save();
        Debug.Log("Туториал сброшен!");
    }
}

