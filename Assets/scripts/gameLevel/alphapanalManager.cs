using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class alphapanalManager : MonoBehaviour
{
    public GameObject alphaPanel;



    // Start is called before the first frame update
    void Start()
    {
        alphaPanel.GetComponent<CanvasGroup>().DOFade(0,2.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
