using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public int timeLeft = 30; //Seconds Overall
    public Text gameTime; //UI Text Object
    private bool timerIsActive = true; //Timer *must be* disabled until Countdown animation activates it with StartTimer function

    public GameControl game;
    
    void Update()
    {
        if (timerIsActive)
        {
            gameTime.text = ("" + timeLeft); //Showing the Score on the Canvas
            if (timeLeft <= 0) //Counter stops at 0
            {
                timeLeft = 0;
                timerIsActive = false;
                // StopTimer(); //*Remove it and enable the line above
                game.GameOver(); //Call GameOver function from GameControl
            }
        }
    }
    //Simple Coroutine
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    public void StartTimer() //*Doesn't work if "timerIsActive=false;" by default!
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1; //Just making sure that the timeScale is right
        print("Timer started");
    }

    public void StopTimer() //*It works!
    {
        timerIsActive = false;
        print("Timer stopped");
    }
}