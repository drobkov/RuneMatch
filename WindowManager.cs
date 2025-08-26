using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
   
    private bool isSettingWindowActive = false; // Флаг состояния окна настроек
    public GameObject UIFirst; //  Элементы с главного экрана.
    public GameObject Tutorial;
    public void ToggleSettingWindow() // Переключатель окна настроек
    {
        isSettingWindowActive = !isSettingWindowActive; // Меняем состояние флага
        gameObject.SetActive(isSettingWindowActive); // Устанавливаем видимость панели
        
    }

    public void CloseWindow(){
        gameObject.SetActive(false);
        if (UIFirst != null)
        {
             UIFirst.SetActive(true); // Активируем элементы с главного экрана.
        }
    }

    public void OpenWindow(){
        gameObject.SetActive(true);
        if (UIFirst != null)
        {
             UIFirst.SetActive(false); // Деактивируем элементы с главного экрана.
        }
    }

    // Метод для открытия окна и одновременного закрытия UIFront.
    public void OpenWithCloseUIFirst()
    {
        gameObject.SetActive(true); 
        if (UIFirst != null)
        {
             UIFirst.SetActive(false); // Деактивируем элементы с главного экрана.
        }
        if (Tutorial != null)
        {
            Tutorial.SetActive(false);
        }
    }

    // Метод для закрытия.
    public void CloseWithOpenUIFirst()
    {
        gameObject.SetActive(false); 
        if (UIFirst != null)
        {
             UIFirst.SetActive(true); // Активируем элементы с главного экрана.
        }
       
    }

    

    

}
