using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public ControllerInput guns;
    public TimerScript gameTimer;
    public ScoreScript score; /* NOT WORKING */

    public GameObject Title;
    public GameObject CountDown;
    public GameObject FinalScore;
    public GameObject ReloadTarget;
    public GameObject StartTarget;
    public GameObject ShootingTargets; /*bad, static solution*/

    public AudioClip clip;
    public AudioSource myMusic;

    OVRHapticsClip myHapticsClip;
    public AudioClip HapticsAudioClip;

    public volatile bool shootingIsActive = true;

    void Start()
    {
        myHapticsClip = new OVRHapticsClip(HapticsAudioClip);
        myMusic = GetComponent<AudioSource>();
        Title.SetActive(true);
        ReloadTarget.SetActive(false);
        StartTarget.SetActive(true);
        ShootingTargets.SetActive(false); /*bad, static solution*/
        //HideTargets();
        CountDown.SetActive(false);
        FinalScore.SetActive(false);
    }

    void Update()
    {
        if (shootingIsActive == true)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                OVRHaptics.LeftChannel.Mix(myHapticsClip);
            }
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                OVRHaptics.RightChannel.Mix(myHapticsClip);
            }
        }
    }

    public void StartGame()
    {
        FinalScore.SetActive(false);
        score.ResetScore(); /* NOT WORKING */
        DisableGuns();
        Title.SetActive(false);
        CountDown.SetActive(true);
        print("Countdown displayed");
        myMusic.Play();
        print("Music started");
        StartCoroutine(TargetDelay());
    }

    IEnumerator TargetDelay()
    {
        print(Time.time);
        yield return new WaitForSeconds(4);
        print("Waiting 4 sec...");
        ShootingTargets.SetActive(true); /*bad, static solution*/
        //ActivateTargets();
        shootingIsActive = true;
        print("Shooting activated");
        gameTimer.StartTimer();
    }

    /* DYNAMIC TARGET DISPLAYING SOLUTION
    public void HideTargets()
    {
        GameObject[] targetsArray = GameObject.FindGameObjectsWithTag("Target");
        print("Targets found");

        foreach (GameObject go in targetsArray)
        {
            go.SetActive(false);
        }
        print("Targets hided");
    }

    public void ActivateTargets()
    {
        GameObject[] targetsArray = GameObject.FindGameObjectsWithTag("Target");
        print("Targets found");

        foreach (GameObject go in targetsArray)
        {
            go.SetActive(true);
        }
        print("Targets activated");
    }
    */

    public void GameOver()
    {
        print("Game Over!");
        ShootingTargets.SetActive(false); /*bad, static solution*/
        //HideTargets();
        FinalScore.SetActive(true);
        ReloadTarget.SetActive(true);
    }

    public void DisableGuns()
    {
        shootingIsActive = false;
        print("Guns Disabled");
    }

    public void EnableGuns()
    {
        shootingIsActive = true;
        print("Guns Enabled");
    }
}