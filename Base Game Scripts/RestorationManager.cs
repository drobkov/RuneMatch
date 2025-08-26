using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;

public class RestorationManager : MonoBehaviour
{   
    [Header("Настройки")]
    public int sceneIndex;
    public GameObject[] destroyedObjects;  // Разрушенные здания
    public GameObject[] restoreWindows;    // Окна восстановления (с кнопкой и ценой)
    public Button taskButton;              // Кнопка "Задания"
    public GameObject taskWindow;          // Главное окно заданий
    public Transform cameraTransform;      // Камера (двигаем экран)
    public GameObject NotEnoughStarsPanel; // Окно "Недостаточно звезд для восстановления"

    private bool[] isRestored;
    private int currentTaskIndex = 0;  
    private GameData gameData;
    [SerializeField] private GameWorldController worldController;

    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        isRestored = new bool[destroyedObjects.Length];

        // Загружаем данные
        for (int i = 0; i < destroyedObjects.Length; i++)
        {
            isRestored[i] = LoadRestorationState(sceneIndex, i);
            destroyedObjects[i].SetActive(!isRestored[i]);
            restoreWindows[i].SetActive(false); // Скрываем все окна восстановления
        }

        FindNextTask();
        taskButton.onClick.AddListener(ShowNextTask);
    }

    // Определяем, какое задание следующее
    private void FindNextTask()
    {
        for (int i = 0; i < isRestored.Length; i++)
        {
            if (!isRestored[i])
            {
                currentTaskIndex = i;
                return;
            }
        }
        currentTaskIndex = -1; // Все здания восстановлены
    }

    // Открывает следующее задание
    private void ShowNextTask()
    {
        if (currentTaskIndex == -1) return;

        taskWindow.SetActive(true); // Показываем окно заданий
        restoreWindows[currentTaskIndex].SetActive(true); // Показываем окно восстановления над зданием
        //MoveCameraToBuilding(currentTaskIndex);
    }

    // Попытка восстановления здания
    public void AttemptRestoreBuilding()
    {
        if (currentTaskIndex == -1) return;

        if (StarManager.Instance.star >= 3)
        {
            RestoreBuilding(currentTaskIndex);
        }
        else
        {
            NotEnoughStarsPanel.SetActive(true);
            Debug.Log("Недостаточно звезд!");
        }
    }

    // Восстанавливаем здание
    private void RestoreBuilding(int index)
    {
        if (!isRestored[index])
        {
            StarManager.Instance.SubtractStars(3);

            // Вызов метода восстановления с анимацией 
            RestoreWithDelay(index); 

            // destroyedObjects[index].SetActive(false);

            restoreWindows[index].SetActive(false); // Закрываем окно восстановления
            isRestored[index] = true;
            SaveRestorationState(sceneIndex, index, true);
            
            taskWindow.SetActive(false); // Закрываем главное окно
            FindNextTask();
        }
    }

    public void RestoreWithDelay(int index)
{
    destroyedObjects[index].GetComponent<Animator>().SetTrigger("Restore");
    //await Task.Delay(460); // Задержка в миллисекундах
    DOVirtual.DelayedCall(0.46f, () => destroyedObjects[index].SetActive(false));
    //destroyedObjects[index].SetActive(false); // Отключаем спрайт разрушенного здания
}


// Плавно двигаем экран к зданию
private void MoveCameraToBuilding(int index)
{
    if (worldController != null)
    {
        // Просто берем мировые координаты здания
        Vector3 targetPosition = destroyedObjects[index].transform.position;

        // Оставляем Z-координату как у gameWorld (чтобы не было странных смещений)
        targetPosition.z = worldController.gameWorld.position.z;

        Debug.Log($"Перемещение камеры к зданию {index}: {targetPosition}");

        // Перемещение
        worldController.MoveToPosition(targetPosition);
    }
}




    // Загружаем сохраненное состояние зданий
    private bool LoadRestorationState(int sceneIndex, int index)
    {
        switch (sceneIndex)
        {
            case 0: return gameData.saveData.scene1Buildings[index];
            case 1: return gameData.saveData.scene2Buildings[index];
            case 2: return gameData.saveData.scene3Buildings[index];
            default: return false;
        }
    }

    // Сохраняем состояние зданий
    private void SaveRestorationState(int sceneIndex, int index, bool state)
    {
        switch (sceneIndex)
        {
            case 0: gameData.saveData.scene1Buildings[index] = state; break;
            case 1: gameData.saveData.scene2Buildings[index] = state; break;
            case 2: gameData.saveData.scene3Buildings[index] = state; break;
        }
        gameData.Save();
    }

    /*
    [Header("Индекс сцены")]
    public int sceneIndex;                   // Индекс текущей сцены
    public GameObject[] destroyedObjects;  // Массив разрушенных зданий
    public Button[] taskListUI;              // Массив кнопок для заданий
    public Button taskButton;                // Кнопка "Задания"
    //public Text starsText;                   // UI элемент для отображения звезд
    public GameObject taskWindow;           //Окно заданий

    [Header("Окно поздравлений UI")]
    public GameObject congratsWindow;        // Окно поздравлений
    public Button nextLocationButton;        // Кнопка для перехода на следующую локацию

    [Header("Окно нехватки звезд UI")]
    public GameObject notEnoughStarsWindow;  // Окно нехватки звезд

    //private StarManager starManager;         // Ссылка на StarManager
    private GameData gameData;
    private bool[] isRestored;               // Состояние каждого здания (восстановлено/не восстановлено)

    private AnimationManager animationManager;
    void Start()
    {
        //starManager = FindObjectOfType<StarManager>(); // Находим StarManager
        gameData = FindObjectOfType<GameData>();
        animationManager = FindObjectOfType<AnimationManager>();
        isRestored = new bool[destroyedObjects.Length];

        
        // Загрузка состояния зданий из GameData
        for (int i = 0; i < destroyedObjects.Length; i++)
        {
            isRestored[i] = LoadRestorationState(sceneIndex, i);
            destroyedObjects[i].SetActive(!isRestored[i]); // Отключаем разрушенное здание, если оно восстановлено
            
        }

        UpdateTaskListUI();

        // Подписываем кнопку "Задания" на функцию показа/скрытия списка
        taskButton.onClick.AddListener(ToggleTaskList);

        // Подписываем каждую кнопку задания на восстановление соответствующего здания
        for (int i = 0; i < taskListUI.Length; i++)
        {
            int index = i;  // Копия переменной для замыкания в лямбде
            taskListUI[i].onClick.AddListener(() => AttemptRestoreBuilding(index));
        }

        // Подписываем кнопку перехода на следующую локацию
        //nextLocationButton.onClick.AddListener(LoadNextLocation);
        nextLocationButton.onClick.AddListener(() => LocationManager.Instance.LoadCurrentScene());

        // Изначально скрываем окна поздравлений и нехватки звезд
        congratsWindow.SetActive(false);
        notEnoughStarsWindow.SetActive(false);
        
    }


    // Функция показа или скрытия списка заданий
    void ToggleTaskList()
    {
        bool isActive = taskListUI[0].transform.parent.gameObject.activeSelf;
        taskListUI[0].transform.parent.gameObject.SetActive(!isActive);
    }

    // Попытка восстановления здания по индексу
    public void AttemptRestoreBuilding(int index)
    {
        Debug.Log("Перед восстановленем. Отстаток звезд: " + StarManager.Instance.star);
        if (StarManager.Instance.star > 0)
        {
            taskWindow.SetActive(false); // Закрываем окно заданий
            RestoreBuilding(index); // Достаточно звезд, восстанавливаем здание
            Debug.Log("Восстанавливаем здание. Остаток звезд: " + StarManager.Instance.star);
        }
        else
        {
            ShowNotEnoughStarsWindow(); // Недостаточно звезд, показываем окно с сообщением
            taskWindow.SetActive(false); // Закрываем окно заданий после нехватки звезд.
            Debug.Log("Недостаточно звезд. Остаток: " + StarManager.Instance.star);
        }
    }

    // Функция восстановления здания по индексу
    public void RestoreBuilding(int index)
    {
        // Проверка достаточного количества звезд и состояния здания
        if (!isRestored[index])
        {
            StarManager.Instance.SubtractStars(1);      // Списываем одну звезду через StarManager
            
            // Вызов метода восстановления с анимацией 
            RestoreWithDelay(index); 

            isRestored[index] = true;          // Помечаем здание как восстановленное
            SaveRestorationState(sceneIndex, index, isRestored[index]);
            gameData.Save();
            gameData.Load();
            gameData.Save();
            UpdateTaskListUI();                // Обновляем список заданий

            // Проверяем, все ли здания восстановлены
            if (AllBuildingsRestored())
            {
                taskWindow.SetActive(false); // Закрываем окно заданий после завершения всех заданий.
                LocationManager.Instance.UnlockNextScene(); //Открываем доступ к новой сцене
                ShowCongratsWindow(); // Показываем окно поздравлений
                
            }
        }
    }

    public void RestoreWithDelay(int index)
{
    destroyedObjects[index].GetComponent<Animator>().SetTrigger("Restore");
    //await Task.Delay(460); // Задержка в миллисекундах
    DOVirtual.DelayedCall(0.46f, () => destroyedObjects[index].SetActive(false));
    //destroyedObjects[index].SetActive(false); // Отключаем спрайт разрушенного здания
}


    // Проверка, все ли здания восстановлены
    bool AllBuildingsRestored()
    {
        foreach (bool restored in isRestored)
        {
            if (!restored)
                return false;
        }
        return true;
    }

    // Показать окно поздравлений
    void ShowCongratsWindow()
    {
        congratsWindow.SetActive(true);
    }

     // Показать окно нехватки звезд
    void ShowNotEnoughStarsWindow()
    {
        notEnoughStarsWindow.SetActive(true);  // Показываем окно нехватки звезд
    }

    // Переход на следующую локацию
    void LoadNextLocation()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Kitchen"); // Имя новой сцены. Меняем в зависимости куда должны перейти по завершению заданий в данной локации.
    }

    public void SaveRestorationState(int sceneIndex, int index, bool isRestored)
    {
        switch (sceneIndex)
        {
            case 0:
                gameData.saveData.scene1Buildings[index] = isRestored;
                gameData.Save();
                break;
            case 1:
                gameData.saveData.scene2Buildings[index] = isRestored;
                gameData.Save();
                break;
            case 2:
                gameData.saveData.scene3Buildings[index] = isRestored;
                gameData.Save();
                break;
            default:
                Debug.LogWarning("Invalid scene index for saving building state.");
                return;
        }

        gameData.Save();
        Debug.Log("Сохранили в GameData");
    }

    public bool LoadRestorationState(int sceneIndex, int index)
    {
    switch (sceneIndex)
    {
        case 0:
            if (index >= 0 && index < gameData.saveData.scene1Buildings.Length)
                return gameData.saveData.scene1Buildings[index];
            break;

        case 1:
            if (index >= 0 && index < gameData.saveData.scene2Buildings.Length)
                return gameData.saveData.scene2Buildings[index];
            break;

        case 2:
            if (index >= 0 && index < gameData.saveData.scene3Buildings.Length)
                return gameData.saveData.scene3Buildings[index];
            break;

        default:
            Debug.LogError($"Invalid scene index {sceneIndex} or building index {index}.");
            break;
    }
    return false; // Вернуть false, если индекс здания или сцены неверен.
}

    public void AddMoney(int amount)
    {
        gameData.saveData.money += amount;
        gameData.Save();
    }
    

  

    // Обновление списка заданий в UI
    void UpdateTaskListUI()
    {
        for (int i = 0; i < taskListUI.Length; i++)
        {
            if (i < destroyedObjects.Length)
            {
                if (isRestored[i])
                {
                    //taskListUI[i].GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1) + " Восстановлено!"; // Убрать описание и сделать всё в UI
                    taskListUI[i].gameObject.SetActive(false); // Скрываем выполненное задание
                }
                else
                {
                    //taskListUI[i].GetComponentInChildren<TextMeshProUGUI>().text = "Восстановить" + (i + 1); // Убрать описание и сделать всё в UI
                    taskListUI[i].gameObject.SetActive(true); // Показываем невыполненное задание
                }
            }
        }
    }
    

    //Функция закрытия окна заданий
    public void TaskWindowCloses()
    {
        taskWindow.SetActive(false);
    }

    //Функция открытия окна заданий
    public void TaskWindowOpen()
    {
        taskWindow.SetActive(true);
    }

    */
}
