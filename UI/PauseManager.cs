using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject pausePanel;
    public GameObject lostLifePanel;
    public Board board;
    public bool paused = false;
    //public Image soundButton;
    //public Sprite musicOnSprite;
    //public Sprite musicOffSprite;
    private SoundManager sound;
    


    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<SoundManager>();
        //In Player Prefs, the "Sound" key is for sound
        // If sound == 0. then mute, if sound == 1, then unmute
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                //soundButton.sprite = musicOffSprite;
                soundOff.SetActive(true);
                soundOn.SetActive(false);
            }
            else{
                soundOff.SetActive(false);
                soundOn.SetActive(true);
                //soundButton.sprite = musicOnSprite;
            }
        }else{
            //soundButton.sprite = musicOnSprite;
            soundOff.SetActive(false);
            soundOn.SetActive(true);
        }

        pausePanel.SetActive(false);
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        //YandexGame.GameplayStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused && !pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            board.currentState = GameState.pause;
        }
        if (!paused && pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            board.currentState = GameState.move;
        }
    }

    public void SoundButton(){
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                //soundButton.sprite = musicOnSprite;
                soundOff.SetActive(false);
                soundOn.SetActive(true);
                PlayerPrefs.SetInt("Sound", 1);
                sound.adjustVolume();
            }
            else{
                //soundButton.sprite = musicOffSprite;
                soundOff.SetActive(true);
                soundOn.SetActive(false);
                PlayerPrefs.SetInt("Sound", 0);
                sound.adjustVolume();
            }
        }else{
            //soundButton.sprite = musicOffSprite;
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            PlayerPrefs.SetInt("Sound", 1);
            sound.adjustVolume();

        }
    }

    public void PauseGame(){
        paused = !paused;

    }

    public void OpenLostLifePanel(){
        lostLifePanel.SetActive(true);
    }
    public void ExitGame(){
        
        LiveManager.Instance.LoseLife();
        //YandexGame.GameplayStop();
        LocationManager.Instance.LoadCurrentScene();
        
        
    }


}
