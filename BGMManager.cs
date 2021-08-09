using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    //オーディオソース
    public AudioSource _audio;
    //BGM
    public AudioClip _bgm;

    // Start is called before the first frame update
    void Start()
    {
        _audio.clip = _bgm;
        _audio.Play();
    }
}
