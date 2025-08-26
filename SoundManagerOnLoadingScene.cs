using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerOnLoadingScene : MonoBehaviour
{

    //public Image soundButton;
    //public Sprite musicOnSprite;
    //public Sprite musicOffSprite;

    public GameObject soundOn;
    public GameObject soundOff;
    private SoundManager sound;
    public AudioSource backgroundMusic;
    public AudioSource clickMusic;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Sound", 0);
        //In Player Prefs, the "Sound" key is for sound
        // If sound == 0. then mute, if sound == 1, then unmute
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                //soundButton.sprite = musicOffSprite;
                soundOff.SetActive(true);
                soundOn.SetActive(false);
                backgroundMusic.Play();
                backgroundMusic.volume = 0;
                
            }
            else{
                //soundButton.sprite = musicOnSprite;
                soundOn.SetActive(true);
                soundOff.SetActive(false);
                backgroundMusic.Play();
                backgroundMusic.volume = 1;
                PlayerPrefs.SetInt("Sound", 1);
                   
                
            }
        }else{
            /*soundButton.sprite = musicOnSprite;
            backgroundMusic.Play();
            backgroundMusic.volume = 1;*/
             // По умолчанию звук включен
            //soundButton.sprite = musicOffSprite;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            backgroundMusic.Play();
            backgroundMusic.volume = 1;
            PlayerPrefs.SetInt("Sound", 1);
            
            
            


             
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void adjustVolume(){
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                backgroundMusic.volume = 0;
                
            }
            else{
                backgroundMusic.volume = 1;
                
            }
        }
    }

    public void SoundButton(){
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                //soundButton.sprite = musicOnSprite;
                soundOn.SetActive(true);
                soundOff.SetActive(false);
                PlayerPrefs.SetInt("Sound", 1);
                adjustVolume();
            }
            else{
                //soundButton.sprite = musicOffSprite;
                soundOn.SetActive(false);
                soundOff.SetActive(true);
                PlayerPrefs.SetInt("Sound", 0);
                adjustVolume();
            }
        }else{
            //soundButton.sprite = musicOffSprite;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            PlayerPrefs.SetInt("Sound", 1);
            adjustVolume();

        }
    }

    public void clickOnButtonSound(){
         
         if (clickMusic != null)
        {
            clickMusic.Play();
        }
        else
        {
            Debug.LogError("AudioSource is not assigned!");
        }
    }

    [ContextMenu("Reset Progress")] // Добавляем кнопку в инспектор
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("Sound");

        Debug.Log("Сохранения музыки сброшены!");
    }

}
