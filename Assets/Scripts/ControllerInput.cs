using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerInput : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    public Transform gunBarrelTransform;

    public GameControl game;

    public GameObject impactEffect;

    public ParticleSystem muzzle_R;
    public ParticleSystem muzzle_L;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
    }

    void Update()
    {
        if (game.GetComponent<GameControl>().shootingIsActive == true)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                audioSource.Play();
                RaycastGun();
                muzzle_L.Play();
                print("Left Shot");
        }

            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                audioSource.Play();
                RaycastGun();
                muzzle_R.Play();
                print("Right Shot");
        }
        }
    }

    private void RaycastGun()
    {
        RaycastHit hit;

        if (Physics.Raycast(gunBarrelTransform.position, gunBarrelTransform.forward, out hit))
        {
            print("hit: " + hit);
            if (hit.collider.gameObject.CompareTag("StartTarget"))
            {
                print("Start hit!");
                //Destroy(hit.collider.gameObject);
                hit.collider.gameObject.SetActive(false); /* Hiding, instead of destroying */
                game.StartGame();
            }
            if (hit.collider.gameObject.CompareTag("Target"))
            {
                print("Target hit!");
                //Destroy(hit.collider.gameObject);
                hit.collider.gameObject.SetActive(false); /* Hiding, instead of destroying */
                ScoreScript.scoreValue += 1;
            }
            if (hit.collider.gameObject.CompareTag("Reload"))
            {
                print("Reloading scene");
                //Destroy(hit.collider.gameObject);
                hit.collider.gameObject.SetActive(false); /* Hiding, instead of destroying */
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}