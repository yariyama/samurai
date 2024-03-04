using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : MonoBehaviour
{
    //�I�[�f�B�I�\�[�X
    private AudioSource _audio;
    //BGM
    public AudioClip _bgm;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void BGMStrat()
    {
        _audio.clip = _bgm;
        _audio.Play();
    }

    public void BGMStop()
    {
        _audio.Stop();
    }
}
