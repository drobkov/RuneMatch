using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public void CloseWindow(){
        gameObject.SetActive(false);
    }

    public void OpenWindow(){
        gameObject.SetActive(true);
    }
}
