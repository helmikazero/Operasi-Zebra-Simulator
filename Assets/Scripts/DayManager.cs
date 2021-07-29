using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    public SceneManage sm;

    public playerProfile pp;
    public Text tx_FOODAMOUNT;
    public Text tx_RATIONAMOUNT;

    public DriverSet dset;

    public float timeRemain = 120f;
    public Text tx_detikRamin;
    public Slider sl_detikRamin;

    public GameObject cinemPanel;
    public Animator ingameCinem;
    public int inGamePhase;

    public float introTime;
    public float outroTime;



    public int benarAmount;
    public int salahAmount;

    public int benarMin = 10;
    public Text tx_benar;

    public int salahLimit = 5;
    public Text tx_salah;


    public Text[] outroText = new Text[7];
    public Text[] text_tanggal;

    public Text pda_tanggal;

    public AudioSource outroMusic;
    public AudioSource makan_sfx;

    // Start is called before the first frame update
    void Start()
    {
        pp = GameObject.Find("PLAYER_PROFILE").GetComponent<playerProfile>();
        sl_detikRamin.maxValue = timeRemain;
        dset.dateNow = pp.dateNow;

        text_tanggal[0].text = "HARI KE-" + pp.daySurvived;
        text_tanggal[1].text = "TGL : " + pp.dateNow[0] + "-" + pp.dateNow[1] + "-" + pp.dateNow[2];

        pda_tanggal.text = "TGL : " + pp.dateNow[0] + "-" + pp.dateNow[1] + "-" + pp.dateNow[2];
    }

    // Update is called once per frame
    void Update()
    {
        tx_FOODAMOUNT.text = "ENERGI:"+Mathf.Floor(pp.foodAmount);
        tx_RATIONAMOUNT.text = "MAKANAN:" + pp.foodRation;
        ingameCinem.SetInteger("cinemPhase", inGamePhase);

        tx_benar.text = "BENAR:" + benarAmount + "/" + benarMin;
        tx_salah.text = "SALAH:" + salahAmount + "/" + salahLimit;

        if (inGamePhase == 0)
        {
            if(introTime > 0)
            {
                introTime -= 1f * Time.deltaTime;
            }
            else
            {
                /*cinemPanel.SetActive(false);*/
                dset.MATCH_START();
                inGamePhase = 1;
            }
        }

        if (inGamePhase == 1)
        {
            if(timeRemain > 0)
            {
                timeRemain -= 1f * Time.deltaTime;
            }
            else
            {
                EndGame(0);
                
            }

            if(pp.foodAmount > 0)
            {
                pp.foodAmount -= 1f * Time.deltaTime;
            }
            else
            {
                //HABIS MAKANAN
                EndGame(1);
            }

            if(salahAmount >= salahLimit)
            {
                //BANYAK SALAH
                EndGame(3);
            }

        }

        if(inGamePhase == 2)
        {
            if(outroTime > 0)
            {
                outroTime -= 1f * Time.deltaTime;
            }
            else
            {
                sm.DYN_LOADSCENE(1);
            }
        }

        tx_detikRamin.text = "TIMER: " + Mathf.Floor(timeRemain) + "s";
        sl_detikRamin.value = timeRemain;
    }

    public void BOLOS()
    {
        EndGame(2);
    }


    public void EndGame(int dayEndResult)
    {

        /*cinemPanel.SetActive(true);*/
        pp.dateNow[0]++;
        pp.daySurvived++;

        pp.moneyAmount = pp.moneyAmount +(benarAmount * 10000 - salahAmount * 5000);

        outroMusic.Play();
        inGamePhase = 2;

        outroText[0].text = "PENANGANAN TEPAT = " + benarAmount + " x Rp10000 = +Rp"+(benarAmount * 10000);
        outroText[1].text = "PENANGANAN SALAH = " + salahAmount + " x Rp5000 = -Rp" + (salahAmount * 5000);
        outroText[2].text = "UANG DITERIMA = Rp" + (benarAmount * 10000 - salahAmount * 5000);
        outroText[3].text = "UANG SEKARANG = Rp" + pp.moneyAmount;


        if (dayEndResult == 0)
        {
            if(benarAmount > benarMin)
            {
                outroText[4].text = "OPERASI ZEBRA KALI INI BERHASIL";
                outroText[5].text = "ANDA BERHASIL MELEWATI HARI";
                outroText[6].text = "UNTUK BERTUGAS ESOK HARI";
            }
            else
            {
                outroText[4].text = "OPERASI ZEBRA KALI INI KURANG OPTIMAL";
                outroText[5].text = "PENANGANAN ANDA BELUM SIGAP";
                outroText[6].text = "ANDA DI SANKSI RP25.000, TETAPI MASIH DAPAT BERTUGAS ESOK HARI";
                pp.moneyAmount -= 250000;
            }
            
        }
        else
        {
            switch (dayEndResult)
            {
                case 1:
                    outroText[4].text = "ANDA KEHABISAN ENERGI HINGGA PINGSAN";
                    outroText[5].text = "ANDA DIBAWA KE RUMAH SAKIT DAN PERLU MEMBAYAR BIAYA PERAWATAN";
                    outroText[6].text = "DAN DIBEBAS TUGASKAN UNTUK SEKARANG";
                    pp.PLAYER_RESET();
                    break;
                case 2:
                    outroText[4].text = "ANDA BOLOS DARI TUGAS HARI INI";
                    outroText[5].text = "SIKAP INI DIANGGAP TIDAK DISIPLIN";
                    if(pp.moneyAmount - 50000 > 0)
                    {
                        pp.moneyAmount -= 50000;
                        outroText[6].text = "ANDA MEMBAYAR SANKSI RP50.0000, TETAPI MASIH BISA BERTUGAS UNTUK ESOK HARI";
                    }
                    else
                    {
                        outroText[6].text = "ANDA TIDAK MAMPU MEMBAYAR SANKSI DAN DIBEBAS TUGASKAN UNTUK SEKARANG";
                        pp.PLAYER_RESET();
                    }
                    break;
                case 3:
                    outroText[4].text = "ANDA TERLALU BANYAK MENGALAMI KESALAHAN PENANGANAN";
                    outroText[5].text = "HAL INI DIANGGAP KECEROBOHAN DALAM MENANGANI MASYARAKAT";
                    outroText[6].text = "ANDA DIBEBAS TUGASKAN UNTUK SEKARANG";
                    pp.PLAYER_RESET();
                    break;
            }
        }
    }

    public void MAKAN()
    {
        if(pp.foodRation > 0)
        {
            makan_sfx.Play();
            pp.foodAmount = Mathf.Clamp(pp.foodAmount+35f,0f,100f);
            pp.foodRation--;
        }
    }
}
