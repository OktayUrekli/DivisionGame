using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sonucManager : MonoBehaviour
{
    public void oyunYeniden()
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void anaMenu()
    {
        SceneManager.LoadScene("menuLevel");
    }
}
