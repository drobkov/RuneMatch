using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Подключаем DoTween

public enum GameType
{
    Moves,
    Time
}
[System.Serializable]
public class EndGameRequirements{ //Требования к завершению игры
    public GameType gameType;
    public int counterValue;

}

public class EndGameManager : MonoBehaviour
{
    public GameObject movesLabel;
    public GameObject timeLabel;
    public GameObject youWinPanel;
    private CanvasGroup canvasGroup; // Это канвас груп youWinPanel. Он нужен для плавного появления.
    public GameObject tryAgainPanel;
    public TextMeshProUGUI counter;
    public EndGameRequirements requirements;
    public int currentCounterValue;
    [SerializeField] private Board board;
    
    private float timerSeconds;

    [Header("Extra Moves")]
    public int extraMoves = 6; // Количество дополнительных ходов
    public int initialCost = 800; // Начальная стоимость
    public float growthFactor = 2.0f; // Коэффициент увеличения стоимости
    public int purchaseCount = 0; // Количество покупок на уровне
    public TextMeshProUGUI moneyCounter;
    public GameObject extraMovesPanel; // Окно с возможностью покупки дополнительных ходов
    public GameObject ShopPanel; //Окно магазина
    public GameObject LostLifePanel; //Окно с предупреждением о потери жизни если нажмет выход.
    public TextMeshProUGUI costText; // Текст на кнопке с суммой
    public Button purchaseButton; // Кнопка покупки
    public int CurrentCost => Mathf.CeilToInt(initialCost * Mathf.Pow(growthFactor, purchaseCount)); // Текущая стоимость


    void Start()
    {
        //board = FindObjectOfType<Board>();
        
        SetGameType();
        SetupGame();
        purchaseCount = 0;

        canvasGroup = youWinPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; // Начинаем с невидимого состояния
        youWinPanel.SetActive(false);
    }

    void SetGameType(){
        if (board.world != null)
        {
            if (board.level < board.world.levels.Length)
            {
                if (board.world.levels[board.level] != null)
                {
                    requirements = board.world.levels[board.level].endGameRequirements;
                }
            }
        }
    }

    void SetupGame(){
        currentCounterValue = requirements.counterValue;
        if (requirements.gameType == GameType.Moves)
        {
            movesLabel.SetActive(true);
            timeLabel.SetActive(false);
        }else{
            timerSeconds = 1;
            movesLabel.SetActive(false);
            timeLabel.SetActive(true);
        }
        counter.text = "" +  currentCounterValue;
    }

    public void DecreaseCounterValue(){ //Уменьшить значение счетчика
        if (board.currentState != GameState.pause)
        {
            currentCounterValue--;
            counter.text = "" + currentCounterValue;
            if (currentCounterValue <= 0)
            {
                //LoseGame();
                
                //TEST 
                ExtraMoves();
                //MoneyForMoves(); //Когда закончились ходы, предлагаем за монетки добавить 6 ходов.
                
                
            }
        }
        
    }

    //TEST
    //Если закончились ходы, предлагаем за монетки добавить 6 ходов.
    public void ExtraMoves(){
        extraMovesPanel.SetActive(true); // Открываем окно и предлагаем добавить 6 ходов и продолжить за монетки.
        UpdateUI(); // Обновляем текст стоимости
        //MoneyManager.Instance.money
        // Сравниваем, если достаточно монет, то списываем со счета MoneyManager. Если нет, то открываем магазин.

    }

    public void PurchaseExtraMoves()
    {
        int cost = CurrentCost;

        if (MoneyManager.Instance.money >= cost) // Проверяем, достаточно ли денег
        {
            MoneyManager.Instance.money -= cost; // Списываем деньги
            MoneyManager.Instance.SaveMoney();
            currentCounterValue += extraMoves; // Добавляем ходы
            
            purchaseCount++; // Увеличиваем счётчик покупок
            CloseExtraMovesPanel(); // Закрываем окно
            counter.text = "" + currentCounterValue;
        }
        else
        {
            OpenShop(); // Если денег недостаточно, открываем магазин
        }
    }

    private void OpenShop()
    {
        ShopPanel.SetActive(true);
        Debug.Log("Открывается магазин!"); 
        // Реализуйте логику открытия магазина здесь
    }

    private void UpdateUI()
    {
        if (costText != null)
        {
            costText.text = $"Играть за {CurrentCost}";
        }
        
    }
    public void CloseExtraMovesPanel()
    {
        if (extraMovesPanel != null)
        {
            extraMovesPanel.SetActive(false); // Закрываем окно
        }
    }


    public void WinGame(){
        //youWinPanel.SetActive(true);
        StartCoroutine(ShowWinPanelWithDelay(2f)); 
        DOTween.KillAll();
        DOTween.Clear();
        board.currentState = GameState.win;
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }
    public void LoseGame(){
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        //Debug.Log("You Lose!");
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

    private IEnumerator ShowWinPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Ждём 2 секунды
        youWinPanel.SetActive(true); // Активируем объект
        canvasGroup.DOFade(1, 1f).SetEase(Ease.Linear); // Плавное проявление за 1 сек
    }

    // Update is called once per frame
    void Update()
    {
        if (requirements.gameType == GameType.Time && currentCounterValue > 0)
        {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
       if (moneyCounter != null && extraMovesPanel.activeSelf)
       {
            moneyCounter.text = $"{MoneyManager.Instance.money}";
       }
        
        

    }
}
