using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelManager : MonoBehaviour
{
    //オーディオソース★
    private AudioSource _audio;
    //効果音★
    public AudioClip _se1;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void ButtonPush()
    {
        PlayerPrefs.DeleteAll();

        //★
        _audio.clip = _se1;
        _audio.Play();
    }
}
