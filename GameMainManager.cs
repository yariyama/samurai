using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainManager : MonoBehaviour
{
    //オーディオソース
    private AudioSource _audio;
    //BGM
    public AudioClip _bgm;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _audio.clip = _bgm;
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BGMStop()
    {
        _audio.Stop();
    }
}
