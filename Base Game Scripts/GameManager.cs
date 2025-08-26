using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playButtonLevelText;
    public TextMeshProUGUI OpenNextLevelPanelText;
    public GameObject NotEnoughLivesWindow;
    public GameObject OpenNextLevelPanel;
    [SerializeField] private GameData gameData;

    [Header("Level Information")]
    public string levelToLoad;
    public int currentlevel = 1;

 private void Start()
    {
        //gameData = FindObjectOfType<GameData>();
        //FillFields();

        
        
        //SaveLeaderBoard();
    }
private void Awake(){
    //FillFields();
    
}

/*public void SaveLeaderBoard(){
    YandexGame.NewLeaderboardScores("Rating", GetNextLevel() + 1);
}*/

public void Go() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        SceneManager.LoadScene("Splash");
    }

public void GoToHome() {
    //SceneManager.LoadScene("MainGameScene"); 

}

void Update(){
    if (playButtonLevelText != null)
    {
        GetNextLevelForText();
    }
    
    /* if (playButtonLevelText != null && OpenNextLevelPanelText != null)
    {
        GetNextLevelForText(); // Обновляем номер уровня на кнопках
    } */
}

//Заполняем поля
public void FillFields(){
    //currentlevel = gameData.GetNextLevel();
   
}

//Обновляем на кнопке и в панеле номер уровня
public void GetNextLevelForText(){
    int nextLevel = GetNextLevel();
    if (playButtonLevelText != null)
    {
        playButtonLevelText.text = "Уровень " + (nextLevel + 1);
        OpenNextLevelPanelText.text = "Уровень " + (nextLevel + 1);
    }
}

// Метод для получения следующего уровня
int GetNextLevel()
{
    // Найдем последний активный уровень
    for (int i = 0; i < gameData.saveData.isActive.Length; i++)
    {
        if (gameData.saveData.isActive[i])
        {
            currentlevel = i;
        }
    }

    // Возвращаем следующий уровень
    return currentlevel;
}

//Тестирую скрипт из levelButton.cs
public void Play(){

    if (PlayerPrefs.HasKey("currentLives"))
    {
        int currentLives = PlayerPrefs.GetInt("currentLives");

        if (currentLives > 0)
        {
            // Если есть жизни, запускаем уровень
            PlayerPrefs.SetInt("Current Level", currentlevel);
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            // Если жизней нет, показываем сообщение
            if (NotEnoughLivesWindow != null)
            {
                NotEnoughLivesWindow.SetActive(true); 
                OpenNextLevelPanel.SetActive(false);
            }
            
        }
    }
    else
    {
        // Если ключа нет, выводим сообщение об ошибке или проводим инициализацию
        Debug.Log("Ошибка: количество жизней не инициализировано! ");
    }

    /*
        //Проверяем достаточно ли жизней
        if (gameData.saveData.lives > 0)
        {
           PlayerPrefs.SetInt("Current Level", currentlevel);
            SceneManager.LoadScene(levelToLoad); 
        }else{
            //Открываем окно с таймером до восстановления жизни, когда можно запустить следующий уровень.
            NotEnoughLivesWindow.SetActive(true); 
        }
        */
        
    }

}
