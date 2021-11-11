using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
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
        int _count = 0;
        for (int i=0;i<GameManager._block_c;++i)
        {
            if (GameManager._block_st[i]==true)
            {
                ++_count;
                //★
                PlayerPrefs.SetInt("BlockTp" + _count, GameManager._block_tp[i]);
                PlayerPrefs.SetFloat("BlockPx"+ _count, GameManager._block_px[i]);
                PlayerPrefs.SetFloat("BlockPy" + _count, GameManager._block_py[i]);
                PlayerPrefs.SetFloat("BlockPz" + _count, GameManager._block_pz[i]);
                //★
                PlayerPrefs.SetInt("BlockAngle" + _count, GameManager._block_angle[i]);
            }
        }
        PlayerPrefs.SetInt("BlockCount", _count);
        PlayerPrefs.Save();

        _audio.clip = _se1;
        _audio.Play();
    }
}
