using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public Text stopwatch;
    public GameObject timeLabel;
    public GameObject _stopwatch;
    public Text stopwatchEnd;

    bool watchActive = false;
    float currentTime;

    public GameObject crosshair;
    public static bool isPaused;
    public static bool playMode;

    public GameObject panelStart;
    public GameObject panelInit;
    public GameObject panelPause;
    public GameObject panelPause2;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;

    public GameObject RedBall;
    public GameObject BlueBall;
    public GameObject GreenBall;
    public GameObject Blue;
    public GameObject Red;
    public GameObject Green;

    public enum State { START, INIT, PAUSE, PAUSE2, PLAY, END};
    State _state;

    public GameObject StartPosition;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
       currentTime = 0.0f;
       watchActive = false;

       SwitchState(State.START); 
       
       Cursor.visible=true;
       Cursor.lockState = CursorLockMode.Confined;
       
       GameManager.isPaused=true;
    }

    public void SwitchState(State newState)
    {
        EndState();
        BeginState(newState);

    }
       
    public void StartClicked()
    {
        SwitchState(State.INIT);
        Cursor.visible=false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.isPaused=false;
    }

    void BeginState(State newState)
    {
        switch(newState)
        {
            case State.START:
                _state=State.START;
                panelStart.SetActive(true);
                playMode=false;
                break;
            case State.INIT:
                _state=State.INIT;
                panelInit.SetActive(true);
                break;
            case State.PAUSE:
                _state=State.PAUSE;
                panelPause.SetActive(true);
                Pause();
                break;
            case State.PLAY:
                _state=State.PLAY;
                panelPlay.SetActive(true);
                crosshair.SetActive(true);  
                playMode=true;
                watchActive=true;              
                break;
            case State.PAUSE2:
                _state=State.PAUSE2;
                panelPause2.SetActive(true);
                playMode=false;
                Pause2();
                break;
            case State.END:
                _state=State.END;
                playMode=false;
                isPaused=true;
                panelLevelCompleted.SetActive(true);
                break;    
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case State.START:
                break;
            case State.INIT:
                if (Input.GetKeyDown(KeyCode.Space))
                SwitchState(State.PAUSE);
                break;
            case State.PAUSE:
                if (Input.GetKeyDown(KeyCode.Space))
                Resume();
                break;
            case State.PLAY:
                if (Input.GetKeyDown(KeyCode.Space))
                SwitchState(State.PAUSE2);
                break;
            case State.PAUSE2:
                if (Input.GetKeyDown(KeyCode.Space))
                Resume2();
                break;
            case State.END:
                break;
        }    

        if(watchActive == true)
        {
            currentTime = currentTime + Time.deltaTime; 
        }  
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //stopwatch.text=time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds;
        stopwatch.text=time.ToString(@"mm\:ss\:fff");

        if(ObjectClicker.AllDestroyed==true)
        {
            FinishMaze();
            ObjectClicker.AllDestroyed=false;
        }
    }

    void EndState()
    {
        switch(_state)
        {
            case State.START:
                panelStart.SetActive(false);
                break;
            case State.INIT:
                panelInit.SetActive(false);
                break;
            case State.PAUSE:
                panelPause.SetActive(false);
                isPaused=false; 
                break;
            case State.PLAY:
                playMode=false;
                break;
            case State.PAUSE2:
                panelPause2.SetActive(false);
                isPaused=false;
                break;
            case State.END:
                panelLevelCompleted.SetActive(false);                
                break;    
        }
    }
    public void Resume()
    {
       SwitchState(State.INIT); 
       Cursor.visible=false;       
       Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Pause()
    {   
        panelInit.SetActive(false);
        isPaused=true;
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Resume2()
    {
       SwitchState(State.PLAY); 
       Cursor.visible=false;       
       Cursor.lockState = CursorLockMode.Locked;
       watchActive=true;
    }    
    void Pause2()
    {           
        crosshair.SetActive(false);
        isPaused=true;
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.Confined;
        watchActive=false;
    }

    public void ResetClicked()
    { 
       SwitchState(State.START);
       panelPlay.SetActive(false);
       Pause();
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartTrialClicked()
    {
       Pause2(); 
       StartMazeClicked();
    }

    public void ReplayClicked()
    {
       SwitchState(State.START);
       panelLevelCompleted.SetActive(false);
       Pause();
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartMazeClicked()
    {
        Player.transform.position = StartPosition.transform.position; 
        Player.transform.rotation = StartPosition.transform.rotation;
        currentTime=0;
        watchActive=false;       
        isPaused=false;
        panelInit.SetActive(false);

        RedBall.SetActive(true);
        BlueBall.SetActive(true);
        GreenBall.SetActive(true);        
        Blue.SetActive(true);        
        Red.SetActive(true);        
        Green.SetActive(true);        

        SwitchState(State.PLAY);
        Cursor.visible=false;       
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FinishMaze()
    {
        watchActive=false;
        panelPlay.SetActive(false);
        SwitchState(State.END);
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.Confined;
        stopwatchEnd.text=stopwatch.text;
    }
    
} 
