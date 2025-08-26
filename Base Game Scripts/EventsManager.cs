using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventsManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject keyCollectingEventWindow;
    public Button keyCollectingEventButton;
    public Slider keySlider;
    public GameObject congratsPanel;
    public TextMeshProUGUI CounterTextKeyCollecting;
    //public TextMeshProUGUI keyCollectingText;

    [Header("Game Variables")]
    public int currentKeyEvent = 0;
    public int maxKeyEvent = 4;
    public bool StateKeyEvent;
    public bool isKeyEventShown;

    private GameData gameData;
    private GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        gameManager = FindObjectOfType<GameManager>();
        Load();
            //if (StateKeyEvent == true && gameManager.currentlevel == 2)
        //{
            if (keySlider != null || congratsPanel != null || keyCollectingEventButton != null || keyCollectingEventWindow != null)
            {
                keySlider.maxValue = maxKeyEvent;
                keySlider.value = currentKeyEvent;
                congratsPanel.SetActive(false);
                keyCollectingEventButton.gameObject.SetActive(true);
            }
            if (isKeyEventShown == true)
            {
                keyCollectingEventWindow.SetActive(false);
                isKeyEventShown = false;
                Save();
            //}
            
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        CounterTextKeyCollecting.text = currentKeyEvent + "/4";
    }

    public void AddKeyForEvent(){
        if (currentKeyEvent < maxKeyEvent)
        {
            currentKeyEvent++;
            keySlider.value = currentKeyEvent;

            if (currentKeyEvent == maxKeyEvent)
            {
                ShowCongrats();
            }
        }
        
        Save();
        
    }

    private void ShowCongrats()
    {
        // Показать окно поздравлений
        congratsPanel.SetActive(true);
        MoneyManager.Instance.AddMoney(500); // Начисляем бонус 500 монет за евент.
        keyCollectingEventButton.gameObject.SetActive(false);
        keyCollectingEventWindow.SetActive(false);
        StateKeyEvent = false; //Отключаем набор ключей после прохождения.
        Save();

    }

    public void CloseCongrats()
    {
        congratsPanel.SetActive(false);  
    }

    //Показываем оффер при достижении какого либо уровня.
    public void ShowKeyEvent()
{
    if (keyCollectingEventWindow != null)
    {
        keyCollectingEventButton.gameObject.SetActive(true);
        keyCollectingEventWindow.SetActive(true);
    }
}

    public void Load(){
    currentKeyEvent = gameData.saveData.keyEvent;
    StateKeyEvent = gameData.saveData.stateKeyEvent;
    isKeyEventShown = gameData.saveData.isKeyEventShown;
    }
    public void Save(){
    gameData.saveData.keyEvent = currentKeyEvent;
    gameData.saveData.stateKeyEvent = StateKeyEvent;
    gameData.saveData.isKeyEventShown = isKeyEventShown;
    gameData.Save();
    }
}
