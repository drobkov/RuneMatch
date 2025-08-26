using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackToSplash : MonoBehaviour
{
    private GameData gameData;
    private Board board;
    
    [SerializeField] private LevelLoader levelLoader;
    
    public void WinOK(){
        if (gameData != null)
        {
            gameData.saveData.isActive[board.level + 1] = true;
            Debug.Log("Уровень пройден и теперь он: " + gameData.GetNextLevel());
            gameData.saveData.money += 65; 
            gameData.saveData.starCounter += 1;
            MoneyManager.Instance.LoadMoney();
            StarManager.Instance.LoadStar();
            Debug.Log("Добавлено 65 монет и 1 звезда!");
            Debug.Log("И теперь всего звезд: " + gameData.saveData.starCounter);

            if (gameData.saveData.keyEvent < gameData.saveData.maxKeyEvent)
            {
                gameData.saveData.keyEvent++; // Добавляем ключ к евенту. 
            }
            gameData.Save();
        }
        //gameData.saveData.money += 200; 
        //gameData.saveData.starCounter += 1;
        //gameData.Save();
        //MoneyManager.Instance.LoadMoney();
        //StarManager.Instance.LoadStar();
        //StarManager.Instance.AddStars(1); // Добавляем 1 звезду за прохождение уровня.
        //MoneyManager.Instance.AddMoney(200); // Добавляем 200 монет за прохождение уровня.
        LocationManager.Instance.LoadCurrentScene(); 
        

/*/ Проверяем кратность уровня 2 и показываем рекламу
    if ((board.level + 1) % 2 == 0)
    {
        Debug.Log("Показываем рекламу");
        //YandexGame.FullscreenShow(); // Показываем рекламу. 
        //MoneyManager.Instance.AddMoney(100); // Добавляем 100 монет за просмотр рекламы.
        //gameData.saveData.money += 100; // Добавляем 100 монет за просмотр рекламы.
        //gameData.Save();

    }
    */

        
    }

    public void LoseOK(){
        LiveManager.Instance.LoseLife(); // Убираем 1 жизнь.
        if (levelLoader != null)
        {
            levelLoader.StartLoadLevelAnimation();
        }
        LocationManager.Instance.LoadCurrentScene();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        board= FindObjectOfType<Board>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
