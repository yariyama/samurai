using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//★
using UnityEngine.SceneManagement;

public class ReplayButtonManager : MonoBehaviour
{
    //オーディオソース
    private AudioSource _audio;
    //効果音
    public AudioClip _se1;

    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonPush()
    {
        SceneManager.LoadScene("MainScene");

        _audio.clip = _se1;
        _audio.Play();
    }
}
