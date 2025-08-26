using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeWindow : MonoBehaviour
{
    public GameObject instructionWindow;

    void Start()
    {
        // Проверяем, существует ли ключ "FirstLaunch"
        if (!PlayerPrefs.HasKey("FirstLaunch"))
        {
            // Если ключа нет, значит это первый запуск
            ShowInstructionWindow();

            // Устанавливаем "FirstLaunch" в 1, чтобы не показывать окно в следующий раз
            PlayerPrefs.SetInt("FirstLaunch", 1);

            // Сохраняем изменения
            PlayerPrefs.Save();
        }
        else
        {
            // Если ключ существует, скрываем окно
            instructionWindow.SetActive(false);
        }
    }

    public void ShowInstructionWindow()
    {
        // Показываем окно с инструкцией
        instructionWindow.SetActive(true);
    }

    public void CloseInstructionWindow()
    {
        // Закрываем окно с инструкцией
        instructionWindow.SetActive(false);
    }


}

