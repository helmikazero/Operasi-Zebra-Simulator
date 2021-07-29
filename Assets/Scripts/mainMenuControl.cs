using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainMenuControl : MonoBehaviour
{
    public SceneManage sm;

    public playerProfile pp;

    public Text[] tx_harike;
    public Text[] tx_dateNow;
    public Text[] tx_moneyAmount;
    public Text[] tx_energiAmount;
    public Text[] tx_rationAmount;

    [System.Serializable]
    public class RationVer
    {
        public string namaRation;
        public int hargaRation;
    }

    public RationVer[] rationSupply;


    public int pageIndex = 0;

    public GameObject[] pages;


    public int introIndex;
    public Animator introAnimator;
    public float introTIme = 6f;
    public float outroTime = 2f;

    public Slider mmSlider;

    // Start is called before the first frame update
    void Start()
    {
        pp = GameObject.Find("PLAYER_PROFILE").GetComponent<playerProfile>();



    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;

        pp.mouseSensitivityMultiplier = mmSlider.value;


        foreach(Text txdat in tx_dateNow)
        {
            txdat.text = "TGL : "+pp.dateNow[0]+" - "+pp.dateNow[1]+" - "+pp.dateNow[2];
        }
        foreach (Text txhar in tx_harike)
        {
            txhar.text = "PLAY HARI KE-"+pp.daySurvived;
        }
        foreach (Text txmon in tx_moneyAmount)
        {
            txmon.text = "MONEY : RP"+pp.moneyAmount;
        }
        foreach (Text tex in tx_energiAmount)
        {
            tex.text = "ENERGI : "+pp.foodAmount;
        }
        foreach (Text tex in tx_rationAmount)
        {
            tex.text = "makanan : " + pp.foodRation;
        }

        introAnimator.SetInteger("phase", introIndex);

        

        if (introIndex == 0)
        {
            if (introTIme > 0)
            {
                introTIme -= 1f * Time.deltaTime;
            }
            else
            {
                introIndex = 1;
            }
        }

        if (introIndex == 2)
        {
            if (outroTime > 0)
            {
                outroTime -= 1f * Time.deltaTime;
            }
            else
            {
                sm.DYN_LOADSCENE(2);
            }
        }
    }


    public void BuyRation(int rationIndex)
    {
        if(pp.moneyAmount > rationSupply[rationIndex].hargaRation)
        {
            pp.foodRation++;
            pp.moneyAmount -= rationSupply[rationIndex].hargaRation;
        }
    }


    public void OpenPage(int deindex)
    {
        pageIndex = deindex;
        RefreshPage();
    }

    public void RefreshPage()
    {
        foreach (GameObject peg in pages)
        {
            peg.SetActive(false);
        }

        pages[pageIndex].SetActive(true);
    }

    public void PLAYDEGAME()
    {
        introIndex = 2;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
