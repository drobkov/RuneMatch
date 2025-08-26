using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostersManager : MonoBehaviour
{
    //public static BoostersManager Instance { get; private set; }
    
    

    /*[Header("Hammer Booster Settings")]
    public Button hammerButton; // Кнопка для активации молотка
    public int hammerCount = 2; // Начальное количество молотков
    public TextMeshProUGUI hammerCountText; // Текст для отображения количества молотков
    */

    [Header("Shuffle Booster Settings")]
    public Button shuffleButton; // Кнопка для активации перемешивания
    public int shuffleCount; // Начальное количество перемешиваний
    public TextMeshProUGUI shuffleCountText; // Текст для отображения количества перемешиваний
    private Board board;
    private GameData gameData;

    //private bool isHammerActive = false; // Флаг, активен ли молоток
   
   private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();

        GameObject boardObject = GameObject.FindWithTag("Board");
        if (boardObject != null)
        {
            board = boardObject.GetComponent<Board>();
            if (board == null)
            {
                Debug.Log("Компонент Board не найден на объекте с тегом 'Board'.");
            }
        }
        else
        {
            Debug.Log("Объект с тегом 'Board' не найден.");
        }
        
        Load();


//        board = GameObject.FindWithTag("Board").GetComponent<Board>();

        // Инициализация текста количества бустеров
        //UpdateHammerCountText();
        UpdateShuffleCountText();

        // Назначаем действия кнопкам
        //hammerButton.onClick.AddListener(ActivateHammerMode);
        if (shuffleButton != null)
        {
            shuffleButton.onClick.AddListener(ActivateShuffle);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Временное решение. 
        Load(); 
        UpdateShuffleCountText();
        /*if (isHammerActive && Input.GetMouseButtonDown(1)) // Проверяем нажатие левой кнопки мыши
        {
            Hammer();
        }
        */
    }

    

    
    // Методы для молотка
    /*
    void ActivateHammerMode()
    {
        if (hammerCount > 0)
        {
            isHammerActive = true;
        }
    }
    */

    

    /*/Бустер Молоток
    public void Hammer(){
        hammerCount--;
        UpdateHammerCountText();
        Destroy(gameObject);
        board.DestroyMatches();
        isHammerActive = false; // Деактивируем режим молотка
        

        if (hammerCount <= 0)
        {
            hammerButton.interactable = false; // Отключаем кнопку, если молотков больше нет
        }
    /*
        if (GameData.Instance.hammersCount > 0) // Если есть молотки
        {
            isHammerActive = true;
            Destroy(gameObject);
            board.DestroyMatches();
            GameData.Instance.hammersCount--; // Уменьшаем количество молотков
            GameData.Instance.Save(); // Сохраняем изменения

            Debug.Log("Молоток активирован!");
        }
        else
        {
            Debug.Log("Нет доступных молотков!");
        }

    

    

    void UpdateHammerCountText()
    {

        hammerCountText.text = hammerCount.ToString();
    }
    */

    // Методы для перемешивания

    public void Load(){
         // Здесь загружаем из gamedata
        if (gameData != null)
        {
            //shuffleCount = GameData.Instance.saveData.shuffleBoardCount;
            shuffleCount = gameData.saveData.shuffleBoardCount;
        }
    }

    public void Save()
    {
        gameData.saveData.shuffleBoardCount = shuffleCount;
        gameData.Save();
        //GameData.Instance.saveData.shuffleBoardCount = shuffleCount;
        //GameData.Instance.Save();
    }


     // Метод для добавления перемешиваний
    public void AddShuffle(int amount)
    {
        shuffleCount += amount;              // Увеличиваем количество
        gameData.saveData.shuffleBoardCount = shuffleCount;
        //GameData.Instance.saveData.shuffleBoardCount = shuffleCount; // Обновляем сохраненное значение
        Save();                 // Сохраняем данные в GameData
    }
    void ActivateShuffle()
    {
        if (shuffleCount > 0)
        {
            Shuffle();
            shuffleCount--;
            Save();
            UpdateShuffleCountText();

            if (shuffleCount <= 0)
            {
                shuffleButton.interactable = false; // Отключаем кнопку, если перемешиваний больше нет
            }
        }
    }

    void UpdateShuffleCountText()
    {
        if (shuffleCountText != null)
        {
          shuffleCountText.text = shuffleCount.ToString();  
        }
        
    }


    //Бустер Перемешивания всех фишек на поле
    public void Shuffle(){
        StartCoroutine(board.ShuffleBoard());
    }

    //Бустер стрела по вертикали
    public void ArrowColum(){

    }

    //Бустер стрела по горизонтали
    public void ArrowRow(){

    }
    

    

}
