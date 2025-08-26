using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;


public class MoneyManager : MonoBehaviour
{

    public static MoneyManager Instance { get; private set; }

    [SerializeField] private GameData gameData;
    public int money;
    //public TextMeshProUGUI moneyCounterText;
    //public TextMeshProUGUI moneyCounterTextInShop;
    //public TextMeshProUGUI moneyCounterTextInSpecOffer;
    

    

    private void Awake()
    {
    if (Instance == null)
        {
            Instance = this;
            //transform.parent = null; // Отсоединяем от любого родителя
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadMoney();   
    }

    private void Update(){
    }

    public void LoadMoney(){
         // Здесь загружаем количество монет
        if (gameData != null)
        {
            money = gameData.saveData.money;
        }
    }

    public void SaveMoney()
    {
        gameData.saveData.money = money;
        gameData.Save();
        Debug.Log("Сохранение завершено в Gamedata из MoneyManager");
    }

    public void AddMoney(int amount)
    {
        money += amount;
        SaveMoney();
    }

    
}
