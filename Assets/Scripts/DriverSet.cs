using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverSet : MonoBehaviour
{

    public int driverIndex;

    public GameObject driverOb;
    public int statein;
    public Transform[] statepos = new Transform[3];

    public int rightAnswer;


    [System.Serializable]
    public class WarnaMotor
    {
        public string warnaText;
        public Material warnaMat;
    }

    public WarnaMotor[] warna;

    

    [System.Serializable]
    public class ModelMotor
    {
        public string modelText;
        public GameObject modelparent;
    }

   
    public ModelMotor[] motor;



    [System.Serializable]
    public class DriverProperties
    {

        public bool isAman;

        [Header("SIM C")]
        public string sim_nama = "HARI MONOLO";
        public Sprite sim_foto;
        public string sim_ttl = "BANDUNG, 16-01-1987";
        public string sim_bgen = "O - PRIA";
        public string sim_alamat = "JL. KADAL RT 14/93 KELURAHAN, KECEMATAN, BANDUNG";
        public string sim_pekerjaan = "MAHASISWA";
        public int[] sim_kadaluarsa = new int[3] { 31, 7, 2021 };

        [Header("STNK")]
        public string stnk_noplat = "D 4912 GX";
        public string stnk_nama = "HARI MONOLO";
        public string stnk_alamat = "JL. KADAL RT 14/93 KELURAHAN, KECEMATAN, BANDUNG";
        public int stnk_modelIndex = 0;
        public string stnk_jenis = "SEPEDA MOTOR";
        public int stnk_tahunpembuatan = 2017;
        public string stnk_cc = "00150 CC";
        public string stnk_rangka = "MH1KC82193184";
        public string stnk_mesin = "KC82E123455";
        public int stnk_warnaIndex = 0;
        public int[] stnk_kadaluarsa = new int[3] { 31, 7, 2021 };

        [Header("MOTOR PROPS")]
        public int motor_model = 0;
        public string motor_noplat = "D 4912 GX";
        public int motor_warnaIndex = 0;
        public string motor_mesin = "KC82E123455";
        public bool motor_spion = true;
        public bool motor_lampudepan = true;
        public bool motor_lampubelakang = true;
    }

    public DriverProperties currentDriver;
    public DriverProperties[] drivers;

    

    public Text wsim_nama;
    public Image wsim_foto;
    public Text wsim_ttl;
    public Text wsim_bgen;
    public Text wsim_alamat;
    public Text wsim_pekerjaan;
    public Text wsim_kadaluarsa;

    [Header("WORLD STNK")]
    public Text wstnk_noplat;
    public Text wstnk_nama;
    public Text wstnk_alamat;
    public Text wstnk_merk;
    public Text wstnk_jenis;
    public Text wstnk_tahunpembuatan;
    public Text wstnk_cc;
    public Text wstnk_rangka;
    public Text wstnk_mesin;
    public Text wstnk_warna;
    public Text wstnk_kadaluarsa;

    [Header("WORLD MOTOR PROPS")]
    public int wmotor_modelIndex;
    public Text[] wmotor_noplat;
    public Text[] wmotor_mesin;
    public GameObject[] wmotor_spion;
    public GameObject[] wmotor_lampudepan;
    public GameObject[] wmotor_lampubelakang;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplytoWorld()
    {
        wsim_nama.text = currentDriver.sim_nama;
        wsim_foto.sprite = drivers[driverIndex].sim_foto;
        wsim_ttl.text = drivers[driverIndex].sim_ttl;
        wsim_bgen.text = drivers[driverIndex].sim_bgen;
        wsim_alamat.text = drivers[driverIndex].sim_alamat;
        wsim_pekerjaan.text = drivers[driverIndex].sim_pekerjaan;
        wsim_kadaluarsa.text = drivers[driverIndex].sim_kadaluarsa[0] + "-" + drivers[driverIndex].sim_kadaluarsa[1] + "-" + drivers[driverIndex].sim_kadaluarsa[2];

        wstnk_noplat.text = drivers[driverIndex].stnk_noplat;
        wstnk_nama.text= drivers[driverIndex].stnk_nama ;
        wstnk_alamat.text= drivers[driverIndex].stnk_alamat;
        wstnk_merk.text= motor[drivers[driverIndex].stnk_modelIndex].modelText;
        wstnk_jenis.text= drivers[driverIndex].stnk_jenis;
        wstnk_tahunpembuatan.text= drivers[driverIndex].stnk_tahunpembuatan.ToString();
        wstnk_cc.text= drivers[driverIndex].stnk_cc;
        wstnk_rangka.text= drivers[driverIndex].stnk_rangka;
        wstnk_mesin.text= drivers[driverIndex].stnk_mesin;
        wstnk_warna.text = warna[drivers[driverIndex].stnk_warnaIndex].warnaText;
        wstnk_kadaluarsa.text= drivers[driverIndex].stnk_kadaluarsa.ToString();

        foreach(ModelMotor mot in motor)
        {
            mot.modelparent.SetActive(false);
        }

        motor[wmotor_modelIndex].modelparent.SetActive(true);
        wmotor_noplat[wmotor_modelIndex].text = drivers[driverIndex].motor_noplat;
        wmotor_mesin[wmotor_modelIndex].text = drivers[driverIndex].motor_mesin;
        wmotor_spion[wmotor_modelIndex].SetActive(drivers[driverIndex].motor_spion);
        wmotor_lampudepan[wmotor_modelIndex].SetActive(drivers[driverIndex].motor_lampudepan);
        wmotor_lampubelakang[wmotor_modelIndex].SetActive(drivers[driverIndex].motor_lampubelakang);
    }

    /*public void ApplytoWorld()
    {
        wsim_nama.text = drivers[driverIndex].sim_nama;
        wsim_foto.sprite = drivers[driverIndex].sim_foto;
        wsim_ttl.text = drivers[driverIndex].sim_ttl;
        wsim_bgen.text = drivers[driverIndex].sim_bgen;
        wsim_alamat.text = drivers[driverIndex].sim_alamat;
        wsim_pekerjaan.text = drivers[driverIndex].sim_pekerjaan;
        wsim_kadaluarsa.text = drivers[driverIndex].sim_kadaluarsa[0] + "-" + drivers[driverIndex].sim_kadaluarsa[1] + "-" + drivers[driverIndex].sim_kadaluarsa[2];

        wstnk_noplat.text = drivers[driverIndex].stnk_noplat;
        wstnk_nama.text = drivers[driverIndex].stnk_nama;
        wstnk_alamat.text = drivers[driverIndex].stnk_alamat;
        wstnk_merk.text = motor[drivers[driverIndex].stnk_modelIndex].modelText;
        wstnk_jenis.text = drivers[driverIndex].stnk_jenis;
        wstnk_tahunpembuatan.text = drivers[driverIndex].stnk_tahunpembuatan.ToString();
        wstnk_cc.text = drivers[driverIndex].stnk_cc;
        wstnk_rangka.text = drivers[driverIndex].stnk_rangka;
        wstnk_mesin.text = drivers[driverIndex].stnk_mesin;
        wstnk_warna.text = warna[drivers[driverIndex].stnk_warnaIndex].warnaText;
        wstnk_kadaluarsa.text = drivers[driverIndex].stnk_kadaluarsa.ToString();

        foreach (ModelMotor mot in motor)
        {
            mot.modelparent.SetActive(false);
        }

        motor[wmotor_modelIndex].modelparent.SetActive(true);
        wmotor_noplat[wmotor_modelIndex].text = drivers[driverIndex].motor_noplat;
        wmotor_mesin[wmotor_modelIndex].text = drivers[driverIndex].motor_mesin;
        wmotor_spion[wmotor_modelIndex].SetActive(drivers[driverIndex].motor_spion);
        wmotor_lampudepan[wmotor_modelIndex].SetActive(drivers[driverIndex].motor_lampudepan);
        wmotor_lampubelakang[wmotor_modelIndex].SetActive(drivers[driverIndex].motor_lampubelakang);
    }*/

    public void PlayerAnswer(bool answerBool)
    {
        /*if(answerBool) = */
    }
}
