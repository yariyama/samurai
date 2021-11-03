using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //★
    public void ButtonPush()
    {
        int _count = 0;
        for (int i = 0; i < GameManager._block_c; ++i)
        {
            if (GameManager._block_st[i] == true)
            {
                ++_count;
                PlayerPrefs.SetFloat("BlockPx" + _count, GameManager._block_px[i]);
                PlayerPrefs.SetFloat("BlockPy" + _count, GameManager._block_py[i]);
                PlayerPrefs.SetFloat("BlockPz" + _count, GameManager._block_pz[i]);
            }
        }
        PlayerPrefs.SetInt("BlockCount", _count);
        PlayerPrefs.Save();
    }
}
