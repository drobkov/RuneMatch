using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    public TextMeshProUGUI helpSendCounterText;
    public TextMeshProUGUI helpGetCounterText;
    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI PlayerLevelText;
    public int currentLevel = 1;
    [SerializeField] private GameData gameData;

[Header("Панель замены имени")]
    [SerializeField] private GameObject changeNamePanel; // Панель с полем ввода
    [SerializeField] private TMP_InputField nameInputField; // Поле ввода

    //[Header("Подключаем ServerManager для его обновления")]
    //[SerializeField] private ServerManager serverManager;

    
    // Start is called before the first frame update
    void Start()
    {
        Fields();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fields(){
        //int nextLevel = GetNextLevel();
    if (helpSendCounterText != null)
    {
        helpSendCounterText.text = "" + gameData.saveData.helpSendCounter;
        helpGetCounterText.text = "" + gameData.saveData.helpGetCounter;
        PlayerNameText.text = "" + gameData.saveData.newPlayerName;
        PlayerLevelText.text = "Уровень\n" + gameData.GetNextLevel(); 
        //PlayerLevelText.text = "Уровень\n" + (GetNextLevel() + 1); 

    }
    }

    public void OpenChangeNamePanel()
    {
        changeNamePanel.SetActive(true);
    }

    public void SaveChangeName()
    {
        gameData.saveData.newPlayerName = nameInputField.text; // Сохраняем имя в переменную
        gameData.Save(); // Вызываем метод сохранения
        Fields(); // Обновляем на странице аккаунта информацию. 
        //serverManager.SendInfoOnServer(); //Отправляем данные в базу лидерборда. ИСПРАВИТЬ! 
        changeNamePanel.SetActive(false); // Скрываем окно после сохранения
        
    }
}
