using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puanManager : MonoBehaviour
{
    private int toplamPuan;
    private int puanArtisi;

    [SerializeField]
    private Text puantext;

    void Start()
    {
        puantext.text = toplamPuan.ToString();
    }

    public void puanArttir(string zorluk)
    {
        switch ( zorluk)
        {
            case "kolay": puanArtisi = 5; break;
            case "orta": puanArtisi = 10; break;
            case "zor": puanArtisi = 15; break;
        }
        toplamPuan += puanArtisi;

        puantext.text = toplamPuan.ToString();

    }

}
