using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NavigationBar : MonoBehaviour
{
    [SerializeField] public GameObject[] panels;
     void Start()
    {
        
    }

    public void OpenMainScreen(){
    NavigationBarClick(2); //При запуске открывается главный экран. 2 это индекс главной панели.
    }
    public void NavigationBarClick(int indexActivePanel){
        
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[indexActivePanel].SetActive(true);
    }
    

}
