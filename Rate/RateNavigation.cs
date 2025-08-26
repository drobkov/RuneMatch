using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateNavigation : MonoBehaviour
{
    [SerializeField] public GameObject[] panels;
     void Start()
    {
        
    }

    public void OpenRateFriendsPanel(){
    RateNavigationClick(1); //При запуске открывается рейтинг Всех людей. 1 это индекс всех людей.
    }

    public void RateNavigationClick(int indexActivePanel){
        
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[indexActivePanel].SetActive(true);
    }
    
}
