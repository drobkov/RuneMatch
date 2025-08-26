using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Подключаем пространство имен для работы с UI.


     public class ShopMenu : MonoBehaviour
     {
        [Header("Панели")]
         public GameObject ShopPanel; // Панель магазина.
         public GameObject OfferPanel; // Панель офферов.
         public GameObject SpecOfferPanel; //Панель специального предложения.
         public GameObject UIFirst; //  Элементы с главного экрана.

         [Header("Вывод текста")] 
         public TextMeshProUGUI moneyCounterText;
         public TextMeshProUGUI priceStartSetText;
         public TextMeshProUGUI priceProfSetText;
         public TextMeshProUGUI priceSpecSetText;

         void Start()
        {
        
        }
        void Update(){
            if (moneyCounterText != null)
            {
               moneyCounterText.text = $"{MoneyManager.Instance.money}"; 
            }
            if (priceStartSetText != null && priceSpecSetText != null && priceProfSetText != null)
            {
                //priceStartSetText.text = YandexGame.purchases[0].price;
                //priceSpecSetText.text = YandexGame.purchases[2].price;
                //priceProfSetText.text = YandexGame.purchases[1].price;
            }

            
        }

    


         // Метод для открытия магазина.
         public void OpenShop()
         {
             ShopPanel.SetActive(true); // Активируем панель магазина.
             if (UIFirst != null)
             {
                UIFirst.SetActive(false); // Деактивируем элементы с главного экрана.
             }
             
           
         }

         // Метод для закрытия магазина.
         public void CloseShop()
         {
             ShopPanel.SetActive(false); // Деактивируем панель магазина.
             if (UIFirst != null)
             {
                UIFirst.SetActive(true); // Активируем элементы с главного экрана.
             }
             
             
         }
        

        // Метод для открытия специального предложения.
        public void OpenSpecOffer()
         {
             SpecOfferPanel.SetActive(true); // Активируем панель спецпредложения.
             OfferPanel.SetActive(false); // Деактивируем панель офферов.
           
         }

         // Метод для закрытия специального предложения.
         public void CloseSpecOffer()
         {
             SpecOfferPanel.SetActive(false); // Деактивируем панель спецпредложения.
             OfferPanel.SetActive(true); // Аактивируем панель офферов.
             
         }


         // Метод для открытия магазина при покупке дополнительных монет во время игры без UIFIRST.
         public void OpenShopInGame()
         {
             ShopPanel.SetActive(true); // Активируем панель магазина.
         }

         // Метод для закрытия магазина.
         public void CloseShopInGame()
         {
             ShopPanel.SetActive(false); // Деактивируем панель магазина.
             
         }

         



         



     }

