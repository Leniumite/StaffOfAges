using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private int maxHp;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxHp = hp;
    }

    public void getHit(int damage)
    {
        audioSource.clip = audioClip;
        if (audioSource.isPlaying)
            return;
        audioSource.Play();
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, maxHp);
        LevelUiManager.Instance.UpdateSlider((float)hp / maxHp);


        if (hp == 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("you're deaad");
    }
}