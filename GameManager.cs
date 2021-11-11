using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //オーディオソース
    private AudioSource _audio;
    //BGM
    public AudioClip _BGM1;

    void Awake()
    {
        _audio=GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _audio.clip = _BGM1;
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
