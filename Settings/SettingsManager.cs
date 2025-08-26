using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

// Система отправки сообщений при нажатии на кнопку "Поддержка" в настройках
    public void SendEmail()
    {
        string email = "r3.studio@yandex.ru";
        string subject = EscapeURL("Обратная связь");
        string body = EscapeURL("Здравствуйте, хочу сообщить...");
        
        string mailto = $"mailto:{email}?subject={subject}&body={body}";
        Application.OpenURL(mailto);
    }

    private string EscapeURL(string text)
    {
        return UnityEngine.Networking.UnityWebRequest.EscapeURL(text).Replace("+", "%20");
    }

//Открытие политики конфиденциальности на сайте при нажатии на кнопку 
public void OpenPolicy()
    {
        Application.OpenURL("https://runematch.ru/privacy.html");
    }
}
