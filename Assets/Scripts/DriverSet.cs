using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverSet : MonoBehaviour
{
    public baseDataDriver bdd;

    public int driverIndex;

    public GameObject driverOb;
    public int statein;
    public Transform[] statepos = new Transform[3];

    public int rightAnswer;
    public int wrongAnswer;

    public float wrongThresh;

    public int[] dateNow = new int[3] { 31, 7, 2021 };


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

        public int wrongIndex;

        public bool simExist;
        public bool stnkExist;

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


    [Header("WORLD SIM C")]
    public GameObject wsimObject;
    public Text wsim_nama;
    public Image wsim_foto;
    public Text wsim_ttl;
    public Text wsim_bgen;
    public Text wsim_alamat;
    public Text wsim_pekerjaan;
    public Text wsim_kadaluarsa;

    [Header("WORLD STNK")]
    public GameObject wstnkObject;
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
        wsim_foto.sprite = currentDriver.sim_foto;
        wsim_ttl.text = currentDriver.sim_ttl;
        wsim_bgen.text = currentDriver.sim_bgen;
        wsim_alamat.text = currentDriver.sim_alamat;
        wsim_pekerjaan.text = currentDriver.sim_pekerjaan;
        wsim_kadaluarsa.text = currentDriver.sim_kadaluarsa[0] + "-" + currentDriver.sim_kadaluarsa[1] + "-" + currentDriver.sim_kadaluarsa[2];

        wstnk_noplat.text = currentDriver.stnk_noplat;
        wstnk_nama.text= currentDriver.stnk_nama ;
        wstnk_alamat.text= currentDriver.stnk_alamat;
        wstnk_merk.text= motor[currentDriver.stnk_modelIndex].modelText;
        wstnk_jenis.text= currentDriver.stnk_jenis;
        wstnk_tahunpembuatan.text= currentDriver.stnk_tahunpembuatan.ToString();
        wstnk_cc.text= currentDriver.stnk_cc;
        wstnk_rangka.text= currentDriver.stnk_rangka;
        wstnk_mesin.text= currentDriver.stnk_mesin;
        wstnk_warna.text = warna[currentDriver.stnk_warnaIndex].warnaText;
        wstnk_kadaluarsa.text= currentDriver.stnk_kadaluarsa.ToString();

        foreach(ModelMotor mot in motor)
        {
            mot.modelparent.SetActive(false);
        }

        motor[wmotor_modelIndex].modelparent.SetActive(true);
        wmotor_noplat[wmotor_modelIndex].text = currentDriver.motor_noplat;
        wmotor_mesin[wmotor_modelIndex].text = currentDriver.motor_mesin;
        wmotor_spion[wmotor_modelIndex].SetActive(currentDriver.motor_spion);
        wmotor_lampudepan[wmotor_modelIndex].SetActive(currentDriver.motor_lampudepan);
        wmotor_lampubelakang[wmotor_modelIndex].SetActive(currentDriver.motor_lampubelakang);

        wsimObject.SetActive(currentDriver.simExist);
        wstnkObject.SetActive(currentDriver.stnkExist);
    }

    /*public void ApplytoWorld()
    {
        wsim_nama.text = currentDriver.sim_nama;
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
        if(answerBool == currentDriver.isAman)
        {
            rightAnswer++;
        }
        else
        {
            wrongAnswer++;
        }


    }


    public DriverProperties GenerateNewDriver()
    {

        DriverProperties newDriver = null;


        newDriver.sim_nama = bdd.nameList[Random.Range(0, bdd.nameList.Length)] + " " + bdd.nameList[Random.Range(0, bdd.nameList.Length)];
        newDriver.sim_foto = bdd.fotoList[Random.Range(0, bdd.fotoList.Length)];
        newDriver.sim_ttl = bdd.kotaList[Random.Range(0, bdd.kotaList.Length)] + ", " + Random.Range(1, 29) + "-" + Random.Range(1, 13) + "-" + Random.Range(1980, 2000);
        newDriver.sim_bgen = bdd.golDarahList[Random.Range(0, bdd.golDarahList.Length)] + " - " + bdd.genderList[Random.Range(0, bdd.genderList.Length)];
        newDriver.sim_pekerjaan = bdd.pekerjaanList[Random.Range(0, bdd.pekerjaanList.Length)];
        newDriver.sim_kadaluarsa[0] = Random.Range(1, 28);
        newDriver.sim_kadaluarsa[1] = Random.Range(1, 13);
        newDriver.sim_kadaluarsa[2] = Random.Range(dateNow[2], 2023);

        newDriver.stnk_noplat = bdd.platKotaList[Random.Range(0, bdd.platKotaList.Length)] + " " + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + " " + bdd.platDaerahList[Random.Range(0, bdd.platDaerahList.Length)];
        newDriver.stnk_alamat = newDriver.sim_alamat;
        newDriver.stnk_modelIndex = Random.Range(0, motor.Length);
        newDriver.stnk_jenis = "SEPEDA MOTOR";
        newDriver.stnk_tahunpembuatan = Random.Range(2000, 2021);
        newDriver.stnk_cc = "00150CC";
        newDriver.stnk_rangka = bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10) + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10);
        newDriver.stnk_mesin = bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10) + Random.Range(0, 10) + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10);
        newDriver.stnk_warnaIndex = Random.Range(0, warna.Length);
        int stnk_kadaluarsagood = Random.Range(0, 3);
        switch (stnk_kadaluarsagood)
        {
            case 0:
                newDriver.stnk_kadaluarsa[0] = Random.Range(dateNow[0] + 1, 29);
                newDriver.stnk_kadaluarsa[1] = dateNow[1];
                newDriver.stnk_kadaluarsa[2] = dateNow[2];
                break;
            case 1:
                newDriver.stnk_kadaluarsa[0] = Random.Range(1, 29);
                newDriver.stnk_kadaluarsa[1] = Random.Range(dateNow[1] + 1, 13);
                newDriver.stnk_kadaluarsa[2] = dateNow[2];
                break;
            case 2:
                newDriver.stnk_kadaluarsa[0] = Random.Range(1, 29);
                newDriver.stnk_kadaluarsa[1] = Random.Range(1, 13);
                newDriver.stnk_kadaluarsa[2] = Random.Range(dateNow[2] + 1, 2026);
                break;
        }


        newDriver.motor_model = newDriver.stnk_modelIndex;
        newDriver.motor_noplat = newDriver.stnk_noplat;
        newDriver.motor_warnaIndex = newDriver.stnk_warnaIndex;
        newDriver.motor_mesin = newDriver.stnk_mesin;
        newDriver.motor_spion = true;
        newDriver.motor_lampudepan = true;
        newDriver.motor_lampubelakang = true;

        if(Random.Range(0f,1f) > wrongThresh)
        {
            newDriver.isAman = true;
            

            if(Random.Range(0f,1f) > 0.5f)
            {
                newDriver.sim_nama = bdd.nameList[Random.Range(0, bdd.nameList.Length)] + " " + bdd.nameList[Random.Range(0, bdd.nameList.Length)];
                newDriver.sim_foto = bdd.fotoList[Random.Range(0, bdd.fotoList.Length)];
                newDriver.sim_ttl = bdd.kotaList[Random.Range(0, bdd.kotaList.Length)] + ", " + Random.Range(1, 29) + "-" + Random.Range(1, 13) + "-" + Random.Range(1980, 2000);
                newDriver.sim_bgen = bdd.golDarahList[Random.Range(0, bdd.golDarahList.Length)] + " - " + bdd.genderList[Random.Range(0, bdd.genderList.Length)];
                newDriver.sim_pekerjaan = bdd.pekerjaanList[Random.Range(0, bdd.pekerjaanList.Length)];
                newDriver.sim_kadaluarsa[0] = Random.Range(1, 28);
                newDriver.sim_kadaluarsa[1] = Random.Range(1, 13);
                newDriver.sim_kadaluarsa[2] = Random.Range(dateNow[2], 2023);
            }
        }
        else
        {
            newDriver.isAman = false;
            newDriver.wrongIndex = Random.Range(0, 7);

            switch (newDriver.wrongIndex)
            {
                case 0:
                    newDriver.simExist = false;
                    break;
                case 1:
                    newDriver.stnkExist = false;
                    break;
                case 2:
                    newDriver.motor_spion = false;
                    break;
                case 3:
                    newDriver.motor_lampudepan = false;
                    break;
                case 4:
                    newDriver.motor_lampubelakang = false;
                    break;
                case 5:
                    switch (stnk_kadaluarsagood)
                    {
                        case 0:
                            newDriver.stnk_kadaluarsa[0] = Random.Range(1, dateNow[0]-1);
                            newDriver.stnk_kadaluarsa[1] = dateNow[1];
                            newDriver.stnk_kadaluarsa[2] = dateNow[2];
                            break;
                        case 1:
                            newDriver.stnk_kadaluarsa[0] = Random.Range(1, 29);
                            newDriver.stnk_kadaluarsa[1] = Random.Range(1, dateNow[1] - 1);
                            newDriver.stnk_kadaluarsa[2] = dateNow[2];
                            break;
                        case 2:
                            newDriver.stnk_kadaluarsa[0] = Random.Range(1, 29);
                            newDriver.stnk_kadaluarsa[1] = Random.Range(1, 13);
                            newDriver.stnk_kadaluarsa[2] = Random.Range(2010, dateNow[2] - 1);
                            break;
                    }
                    break;
                case 6:
                    newDriver.motor_model = Random.Range(0, motor.Length);
                    newDriver.motor_noplat = bdd.platKotaList[Random.Range(0, bdd.platKotaList.Length)] + " " + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + " " + bdd.platDaerahList[Random.Range(0, bdd.platDaerahList.Length)];
                    newDriver.motor_warnaIndex = newDriver.stnk_warnaIndex;
                    newDriver.motor_mesin = newDriver.stnk_mesin;
                    break;
            }
        }


        return newDriver;
    }

    /*public void RandomDriver()
    {
        if(Random.Range(0f,1f) > wrongThresh)
        {
            currentDriver.sim_nama = bdd.nameList[Random.Range(0, bdd.nameList.Length)]+" "+ bdd.nameList[Random.Range(0, bdd.nameList.Length)];
            currentDriver.sim_foto = bdd.fotoList[Random.Range(0, bdd.fotoList.Length)];
            currentDriver.sim_ttl = bdd.kotaList[Random.Range(0, bdd.kotaList.Length)] + ", " + Random.Range(1, 29) + "-" + Random.Range(1, 13) + "-" + Random.Range(1980, 2000);
            currentDriver.sim_bgen = bdd.golDarahList[Random.Range(0, bdd.golDarahList.Length)] + " - " + bdd.genderList[Random.Range(0, bdd.genderList.Length)];
            currentDriver.sim_pekerjaan = bdd.pekerjaanList[Random.Range(0, bdd.pekerjaanList.Length)];
            currentDriver.sim_kadaluarsa[0] = Random.Range(1, 28);
            currentDriver.sim_kadaluarsa[1] = Random.Range(1, 13);
            currentDriver.sim_kadaluarsa[2] = Random.Range(dateNow[2], 2023);

            currentDriver.stnk_noplat = bdd.platKotaList[Random.Range(0, bdd.platKotaList.Length)] + " " + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + " " + bdd.platDaerahList[Random.Range(0, bdd.platDaerahList.Length)];
            currentDriver.stnk_alamat = currentDriver.sim_alamat;
            currentDriver.stnk_modelIndex = Random.Range(0, motor.Length);
            currentDriver.stnk_jenis = "SEPEDA MOTOR";
            currentDriver.stnk_tahunpembuatan = Random.Range(2000, 2021);
            currentDriver.stnk_cc = "00150CC";
            currentDriver.stnk_rangka = bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10)+bdd.alfabet[Random.Range(0,bdd.alfabet.Length)] + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10);
            currentDriver.stnk_mesin = bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10) + Random.Range(0, 10)  + bdd.alfabet[Random.Range(0, bdd.alfabet.Length)] + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10);
            currentDriver.stnk_warnaIndex = Random.Range(0, warna.Length);
            int stnk_kadaluarsagood = Random.Range(0, 3);
            switch (stnk_kadaluarsagood)
            {
                case 0:
                    currentDriver.stnk_kadaluarsa[0] = Random.Range(dateNow[0] + 1, 29);
                    currentDriver.stnk_kadaluarsa[1] = dateNow[1];
                    currentDriver.stnk_kadaluarsa[2] = dateNow[2];
                    break;
                case 1:
                    currentDriver.stnk_kadaluarsa[0] = Random.Range(1, 29);
                    currentDriver.stnk_kadaluarsa[1] = Random.Range(dateNow[1] + 1, 13);
                    currentDriver.stnk_kadaluarsa[2] = dateNow[2];
                    break;
                case 2:
                    currentDriver.stnk_kadaluarsa[0] = Random.Range(1, 29);
                    currentDriver.stnk_kadaluarsa[1] = Random.Range(1, 13);
                    currentDriver.stnk_kadaluarsa[2] = Random.Range(dateNow[2] + 1, 2026);
                    break;
            }


            currentDriver.motor_model = currentDriver.stnk_modelIndex;
            currentDriver.motor_noplat = currentDriver.stnk_noplat;
            currentDriver.motor_warnaIndex = currentDriver.stnk_warnaIndex;
            currentDriver.motor_mesin = currentDriver.stnk_mesin;
            currentDriver.motor_spion = true;
            currentDriver.motor_lampudepan = true;
            currentDriver.motor_lampubelakang = true;
        }
    }*/
}
