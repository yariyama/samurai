using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButtonManager : MonoBehaviour
{
    public void ButtonPush()
    {
        SceneManager.LoadScene("MainScene");
    }
}
