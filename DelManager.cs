using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelManager : MonoBehaviour
{
    public void ButtonPush()
    {
        PlayerPrefs.DeleteAll();
    }
}
