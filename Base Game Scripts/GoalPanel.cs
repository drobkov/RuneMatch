using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalPanel : MonoBehaviour
{

    public Image thisImage;
    public Sprite thisSprite;
    public TextMeshProUGUI thisText;
    public string thisString;
    
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Setup()
    {
        thisImage.sprite = thisSprite;
        thisText.text = thisString;

    }
}
