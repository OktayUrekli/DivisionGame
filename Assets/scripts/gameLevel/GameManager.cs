using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform karelerpaneli;

    [SerializeField]
    private GameObject karePrefab;

    [SerializeField]
    private Transform soruPaneli;

    [SerializeField]
    private Text soruText;

    [SerializeField]
    private Sprite[] kareSprites;

    [SerializeField]
    private GameObject sonucPaneli;

    private GameObject[] kareler=new GameObject[25];

    List<int> bolumdegerleriListesi=new List<int>();

    int bolensayi, bolunensayi,kacincisoru,butonDegeri;
    bool butonabasildimi;
    int dogruSonuc;
    int kalanhak;

    string zorlukSeviyesi;

    hakManager hakManager;

    puanManager puanManager;

    GameObject gecerliKare;
    private void Awake()
    {
        kalanhak = 3;
        hakManager=FindObjectOfType<hakManager>();

        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        puanManager=FindObjectOfType<puanManager>();


        hakManager.kalanHakKontrol(kalanhak);
    }
    void Start()
    {
        butonabasildimi=false;

        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        // soru panelinin ba�lang�� b�y�kl��� s�f�r yap�ld� 
        // animasyonla a��lacak

        kareolustur();
        
    }

    

    private void kareolustur()
    {
        for(int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerpaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0,kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => butonaBasildi());
            // karelere on click fonksiyonu ekleme
            kareler[i]= kare;
        }
        degerYazdir();
        StartCoroutine(DoFadeRoutine());
        Invoke("soruPanelAc", 3f);
    }

    void butonaBasildi()
    {
        if (butonabasildimi)
        {
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            // karelerin i�indeki de�erleri stringden integer a �evirir
            gecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            sonucKontrol();
        }
        

        // Debug.Log(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
        // bas�lan karelerin de�erlerini yazd�ran fonksiyon
    }

    void sonucKontrol()
    {
        if (butonDegeri == dogruSonuc)
        {
            gecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true; 
            gecerliKare.transform.GetChild(0).GetComponent<Text>().text ="";
            gecerliKare.transform.GetComponent<Button>().interactable = false;

            puanManager.puanArttir(zorlukSeviyesi);
            bolumdegerleriListesi.RemoveAt(kacincisoru);
            if(bolumdegerleriListesi.Count > 0)
            {
                soruPanelAc();
            }
            else
            {
                oyunBitti();
            }
        }
        else
        {
            kalanhak--;
            hakManager.kalanHakKontrol(kalanhak);
            
        }

        if (kalanhak <= 0)
        {
            oyunBitti();
        }

    }

    void oyunBitti()
    {
        butonabasildimi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1,0.3f).SetEase(Ease.OutBack);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach(var kare in kareler)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.07f); // karelerin ka� saniyede olu�aca�� belirlendi
            yield return new WaitForSeconds(0.1f); // iki karenin aras�ndaki olu�ma s�resi
        }
        
    }

    void degerYazdir()
    {
        foreach (var kare in kareler)
        {
            int rasgeleDeger = Random.Range(1, 13); // karelerin i�ine [1,12] aras�nde rasgele de�erler al�nd�
            bolumdegerleriListesi.Add(rasgeleDeger);   

            kare.transform.GetChild(0).GetComponent<Text>().text = rasgeleDeger.ToString(); 
            // al�nan de�erler Text b�l�m�nden string olarak karelerin i�ine yazd�r�ld�
        }
    }

    void soruPanelAc()
    {
        soruSor();
        butonabasildimi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, 0.5f); 
        
    }

    void soruSor()
    {
        bolensayi=Random.Range(2, 11);
        kacincisoru=Random.Range(0, bolumdegerleriListesi.Count);

        dogruSonuc =bolumdegerleriListesi[kacincisoru];
       
        bolunensayi = bolensayi * bolumdegerleriListesi[kacincisoru];
        if (bolunensayi < 40)
        {
            zorlukSeviyesi = "kolay";
        }
        else if (bolunensayi >40 && bolunensayi<80)
        {
            zorlukSeviyesi = "orta";
        }
        else
        {
            zorlukSeviyesi = "zor";
        }

        soruText.text = bolunensayi.ToString()+":"+bolensayi.ToString();
    }
}
