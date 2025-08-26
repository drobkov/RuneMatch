using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class ReceivingPurchase : MonoBehaviour
{   
    [SerializeField] private GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake(){

    }

    // Подписываемся на ивенты успешной/неуспешной покупки
private void OnEnable()
{
    //YandexGame.PurchaseSuccessEvent += AddMoney;
    YandexGame.PurchaseSuccessEvent += SuccessPurchased;
    YandexGame.PurchaseFailedEvent += FailedPurchased; // Необязательно
}

private void OnDisable()
{
    //YandexGame.PurchaseSuccessEvent += AddMoney;
    YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
    YandexGame.PurchaseFailedEvent -= FailedPurchased; // Необязательно
}




// Покупка успешно совершена, выдаём товар
void SuccessPurchased(string id)
{
     // Ваш код для обработки покупки. Например:
     if (id == "money3000")
            money3000();
         //YandexGame.savesData.money += 3000;
     else if (id == "money7000")
            money7000();
         //YandexGame.savesData.money += 7000;

     //YandexGame.SaveProgress();
}



void money3000(){
    gameData.saveData.money += 3000; 
    gameData.saveData.shuffleBoardCount += 5;
    MoneyManager.Instance.LoadMoney();
    gameData.Save();
    
    //BoostersManager.Instance.Load();
}
void money7000(){
    gameData.saveData.money += 7000; 
    gameData.saveData.shuffleBoardCount += 20;
    MoneyManager.Instance.LoadMoney();
    gameData.Save();
    
    //BoostersManager.Instance.Load();
}

// Покупка не была произведена
void FailedPurchased(string id)
{
    Debug.Log("Неуспешная покупка");
     // Например, можно открыть уведомление о неуспешности покупки.
}
}
