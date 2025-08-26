using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour
{
    // Start is called before the first frame update
   

    public GameObject ImageMessage;

    
    public void ShowMessage1()
    {
        if (!ImageMessage.gameObject.activeSelf)
        {
            ImageMessage.gameObject.SetActive(true);
            Invoke("HideMessage", 2f); // скрыть через 2 секунды
        }
    }

    private void HideMessage()
    {
       // Проверка, чтобы отключение было безопасным (вдруг уже кто-то отключил)
        if (ImageMessage.gameObject.activeSelf)
        {
            ImageMessage.gameObject.SetActive(false);
        }
    }
}
