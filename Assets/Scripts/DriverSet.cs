using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DriverSet : MonoBehaviour
{
    public DayManager dman;
    public baseDataDriver bdd;

    public AudioSource benarsfx;
    public AudioSource salahsfx;

    public int driverIndex;

    public GameObject driverOb;
    public int statein;
    public Transform[] statepos = new Transform[3];
    public float moveSpeed;

    public bool isMatchStarted;
    public bool isInSession;
    public Vector2 waitDelay = new Vector2(1f, 1f);

    public int rightAnswer;
    public int wrongAnswer;

    public float wrongThresh;

    public int[] dateNow = new int[3] { 31, 7, 2021 };
    public Text dateNowText;

    public DriverProperties currentDriver;

    [System.Serializable]
    public class DriverAntriProperties
    {
        public bool isRandom;
        public DriverProperties dedriver;
    }

    public List<DriverAntriProperties> driverQueue = new List<DriverAntriProperties>();


    [System.Serializable]
    public class WarnaMotor
    {
        public string warnaText;
        public Material warnaMat;
        public Color warnaColor;
    }

    public Material motorMaterialOfficial;

    public WarnaMotor[] warna;



    [System.Serializable]
    public class ModelMotor
    {
        public string modelText;
        public GameObject modelparent;

        public Text noplat;
        public Text noplat2;
        public Text mesin;
        public GameObject spion;
        public GameObject lampudepan;
        public GameObject lampubelakang;

        public MeshRenderer meshren;
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

    
    /*public DriverProperties[] drivers;*/


    [Header("WORLD SIM C")]
    public GameObject wsimObject;
    public Text wsim_nama;
    public Image wsim_foto;
    public Image wsim_foto2;
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

    /*[Header("WORLD MOTOR PROPS")]
    public int wmotor_modelIndex;
    public Text[] wmotor_noplat;
    public Text[] wmotor_mesin;
    public GameObject[] wmotor_spion;
    public GameObject[] wmotor_lampudepan;
    public GameObject[] wmotor_lampubelakang;*/

    public float testDrive;

    // Start is called before the first frame update
    void Start()
    {
        /*isInSession = true;
        dateNowText.text = "TANGGAL " + dateNow[0] + " - " + dateNow[1]+" - " + dateNow[2];
        NextDriver();*/
    }

    // Update is called once per frame
    void Update()
    {


        driverOb.transform.position = Vector3.MoveTowards(driverOb.transform.position, statepos[statein].position, moveSpeed * Time.deltaTime);

        if (isMatchStarted)
        {
            if (!isInSession)
            {
                if (waitDelay.x > 0)
                {
                    waitDelay.x = waitDelay.x - 1f * Time.deltaTime;
                }
                else
                {
                    waitDelay.x = waitDelay.y;
                    isInSession = true;
                    NextDriver();
                }
            }
        }

    }

    
    public void MATCH_START()
    {
        isMatchStarted = true;
        isInSession = true;
        dateNowText.text = "TANGGAL " + dateNow[0] + " - " + dateNow[1] + " - " + dateNow[2];
        NextDriver();
    }


    public void PlayerAnswer(bool answerBool)
    {
        if (isInSession)
        {
            if (answerBool == currentDriver.isAman)
            {
                rightAnswer++;
                dman.benarAmount++;
                benarsfx.Play();
            }
            else
            {
                wrongAnswer++;
                dman.salahAmount++;
                salahsfx.Play();
            }
            isInSession = false;
            DoneDriver();

            driverIndex++;
        }
        
    }

    public void NextDriver()
    {

        Debug.Log("SAYA NEXT DRIVER DIPANGGIL");
        driverOb.transform.position = statepos[0].position;
        statein = 1;

        DriverConfigure();
        currentDriver = driverQueue[driverIndex].dedriver;
        ApplytoWorld();

    }

    public void DoneDriver()
    {

        statein = 2;
    }

    public void DriverConfigure()
    {
        if(driverIndex+1 > driverQueue.Count)
        {
            DriverAntriProperties thisDriver = new DriverAntriProperties();
            thisDriver.isRandom = true;
            thisDriver.dedriver = GenerateNewDriver();

            driverQueue.Add(thisDriver);
        }
        else
        {
            if (driverQueue[driverIndex].isRandom)
            {
                driverQueue[driverIndex].dedriver = GenerateNewDriver();
            }
        }

        /*if(driverQueue[driverIndex] == null)
        {
            DriverAntriProperties thisDriver = null;
            thisDriver.isRandom = true;
            thisDriver.dedriver = GenerateNewDriver();

            driverQueue.Add(thisDriver);
        }
        else
        {
            if (driverQueue[driverIndex].isRandom)
            {
                driverQueue[driverIndex].dedriver = GenerateNewDriver();
            }
        }*/


    }


    public DriverProperties GenerateNewDriver()
    {
        DriverProperties newDriver = new DriverProperties();

        newDriver.simExist = true;
        newDriver.sim_nama = bdd.nameList[Random.Range(0, bdd.nameList.Length)] + " " + bdd.nameList[Random.Range(0, bdd.nameList.Length)];
        newDriver.sim_foto = bdd.fotoList[Random.Range(0, bdd.fotoList.Length)];
        newDriver.sim_ttl = bdd.kotaList[Random.Range(0, bdd.kotaList.Length)] + ", " + Random.Range(1, 29) + "-" + Random.Range(1, 13) + "-" + Random.Range(1980, 2000);
        newDriver.sim_bgen = bdd.golDarahList[Random.Range(0, bdd.golDarahList.Length)] + " - " + bdd.genderList[Random.Range(0, bdd.genderList.Length)];
        newDriver.sim_pekerjaan = bdd.pekerjaanList[Random.Range(0, bdd.pekerjaanList.Length)];
        newDriver.sim_alamat = "JL. " + bdd.namaJalanList[Random.Range(0, bdd.namaJalanList.Length)] + " " + bdd.namaJalanList[Random.Range(0, bdd.namaJalanList.Length)] + " NO." + Random.Range(0, 10) + Random.Range(0, 10) +" "+bdd.kotaList[Random.Range(0,bdd.kotaList.Length)];
        newDriver.sim_kadaluarsa[0] = Random.Range(1, 28);
        newDriver.sim_kadaluarsa[1] = Random.Range(1, 13);
        newDriver.sim_kadaluarsa[2] = Random.Range(dateNow[2], 2023);
        int sim_kadaluarsagood = Random.Range(0, 3);
        switch (sim_kadaluarsagood)
        {
            case 0:
                newDriver.sim_kadaluarsa[0] = Random.Range(dateNow[0] + 1, 29);
                newDriver.sim_kadaluarsa[1] = dateNow[1];
                newDriver.sim_kadaluarsa[2] = dateNow[2];
                break;
            case 1:
                newDriver.sim_kadaluarsa[0] = Random.Range(1, 29);
                newDriver.sim_kadaluarsa[1] = Random.Range(dateNow[1] + 1, 13);
                newDriver.sim_kadaluarsa[2] = dateNow[2];
                break;
            case 2:
                newDriver.sim_kadaluarsa[0] = Random.Range(1, 29);
                newDriver.sim_kadaluarsa[1] = Random.Range(1, 13);
                newDriver.sim_kadaluarsa[2] = Random.Range(dateNow[2] + 1, 2026);
                break;
        }

        newDriver.stnkExist = true;
        newDriver.stnk_noplat = bdd.platKotaList[Random.Range(0, bdd.platKotaList.Length)] + " " + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + Random.Range(0, 10) + " " + bdd.platDaerahList[Random.Range(0, bdd.platDaerahList.Length)];
        newDriver.stnk_nama = newDriver.sim_nama;
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
                newDriver.sim_alamat = "JL. " + bdd.namaJalanList[Random.Range(0, bdd.namaJalanList.Length)] + " " + bdd.namaJalanList[Random.Range(0, bdd.namaJalanList.Length)] + " NO." + Random.Range(0, 10) + Random.Range(0, 10) + " " + bdd.kotaList[Random.Range(0, bdd.kotaList.Length)];
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
            newDriver.wrongIndex = Random.Range(0, 8);

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
                case 7:
                    switch (sim_kadaluarsagood)
                    {
                        case 0:
                            newDriver.sim_kadaluarsa[0] = Random.Range(1, dateNow[0] - 1);
                            newDriver.sim_kadaluarsa[1] = dateNow[1];
                            newDriver.sim_kadaluarsa[2] = dateNow[2];
                            break;
                        case 1:
                            newDriver.sim_kadaluarsa[0] = Random.Range(1, 29);
                            newDriver.sim_kadaluarsa[1] = Random.Range(1, dateNow[1] - 1);
                            newDriver.sim_kadaluarsa[2] = dateNow[2];
                            break;
                        case 2:
                            newDriver.sim_kadaluarsa[0] = Random.Range(1, 29);
                            newDriver.sim_kadaluarsa[1] = Random.Range(1, 13);
                            newDriver.sim_kadaluarsa[2] = Random.Range(2010, dateNow[2] - 1);
                            break;
                    }
                    break;

            }
        }


        return newDriver;
    }

    public void ApplytoWorld()
    {
        wsim_nama.text = "1. "+currentDriver.sim_nama;
        wsim_foto.sprite = currentDriver.sim_foto;
        wsim_foto2.sprite = currentDriver.sim_foto;
        wsim_ttl.text = "2. " + currentDriver.sim_ttl;
        wsim_bgen.text = "3. " + currentDriver.sim_bgen;
        wsim_alamat.text = "4. " + currentDriver.sim_alamat;
        wsim_pekerjaan.text = "5. " + currentDriver.sim_pekerjaan;
        wsim_kadaluarsa.text = currentDriver.sim_kadaluarsa[0] + "-" + currentDriver.sim_kadaluarsa[1] + "-" + currentDriver.sim_kadaluarsa[2];

        wstnk_noplat.text = currentDriver.stnk_noplat;
        wstnk_nama.text = currentDriver.stnk_nama;
        wstnk_alamat.text = currentDriver.stnk_alamat;
        wstnk_merk.text = motor[currentDriver.stnk_modelIndex].modelText;
        wstnk_jenis.text = currentDriver.stnk_jenis;
        wstnk_tahunpembuatan.text = currentDriver.stnk_tahunpembuatan.ToString();
        wstnk_cc.text = currentDriver.stnk_cc;
        wstnk_rangka.text = currentDriver.stnk_rangka;
        wstnk_mesin.text = currentDriver.stnk_mesin;
        wstnk_warna.text = warna[currentDriver.stnk_warnaIndex].warnaText;
        wstnk_kadaluarsa.text = currentDriver.stnk_kadaluarsa[0]+"-"+ currentDriver.stnk_kadaluarsa[1]+"-" + currentDriver.stnk_kadaluarsa[2];

        /*foreach (ModelMotor mot in motor)
        {
            mot.modelparent.SetActive(false);
        }*/



        /*motor[currentDriver.motor_model].modelparent.SetActive(true);*/
        /*wmotor_noplat[currentDriver.motor_model].text = currentDriver.motor_noplat;
        wmotor_mesin[currentDriver.motor_model].text = currentDriver.motor_mesin;
        wmotor_spion[currentDriver.motor_model].SetActive(currentDriver.motor_spion);
        wmotor_lampudepan[currentDriver.motor_model].SetActive(currentDriver.motor_lampudepan);
        wmotor_lampubelakang[currentDriver.motor_model].SetActive(currentDriver.motor_lampubelakang);*/

        for (int i = 0; i < motor.Length; i++)
        {
            motor[i].modelparent.SetActive(i == currentDriver.motor_model);
        }

        motor[currentDriver.motor_model].noplat.text = currentDriver.motor_noplat;
        motor[currentDriver.motor_model].noplat2.text = currentDriver.motor_noplat;
        motor[currentDriver.motor_model].mesin.text = currentDriver.motor_mesin;
        motor[currentDriver.motor_model].spion.SetActive(currentDriver.motor_spion);
        motor[currentDriver.motor_model].lampudepan.SetActive(currentDriver.motor_lampudepan);
        motor[currentDriver.motor_model].lampubelakang.SetActive(currentDriver.motor_lampubelakang);
        /*motor[currentDriver.motor_model].meshren.materials[5] = warna[currentDriver.motor_warnaIndex].warnaMat;*/
        motorMaterialOfficial.color = warna[currentDriver.motor_warnaIndex].warnaColor;


        wsimObject.SetActive(currentDriver.simExist);
        wstnkObject.SetActive(currentDriver.stnkExist);
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
