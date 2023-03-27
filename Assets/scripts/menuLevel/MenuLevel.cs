using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject startBtn, exitBtn;



    void Start()
    {
        FadeOut();
    }

   void FadeOut()
    {
        startBtn.GetComponent<CanvasGroup>().DOFade(1, 1.7f);
        // alpha düzeyinin nerede duracaðý ilk deðiþken yani --> 1
        // alpha düzeyinin ne kadar sürede sonuca ulaþacaðýný belirleyen deðiþken yani --> 0.7f

        exitBtn.GetComponent<CanvasGroup>().DOFade(1, 2f);
    }

    public void ExitGame()
    {
       Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
