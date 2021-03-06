﻿using UnityEngine;
using System.Collections;

public class IntroGameController : MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject RedWoman;

    public PlayerController playerController;
    public Animator CameraAnimator;

    public void ChooseYet()
    {
        playerController.ICanMove = true;
        SpawnPoint.SetActive(true);
        RedWoman.SetActive(true);
    }


    internal void Chosen()
    {
        if(playerController.Chosing == 3)
        {
            CameraAnimator.SetTrigger("Good Election");
            playerController.animator.SetTrigger("PirulaCorrecta");
        }else if (playerController.Chosing == 4)
        {
            CameraAnimator.SetTrigger("Bad Election");
        }



    }
}
