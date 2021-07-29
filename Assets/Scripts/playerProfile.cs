using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProfile : MonoBehaviour
{

    public int daySurvived;
    public float foodAmount;

    public int moneyAmount;

    public int[] dateNow = new int[3] {1 , 1, 2021 };

    public int foodRation;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);


    }

    // Update is called once per frame
    void Update()
    {
        if(dateNow[0] > 28)
        {
            dateNow[0] = 1;
            dateNow[1]++;
        }

        if (dateNow[1] > 12)
        {
            dateNow[1] = 1;
            dateNow[2]++;
        }
    }

    public void PLAYER_RESET()
    {
        daySurvived = 1;
        foodAmount = 100f;

        moneyAmount = 10000;
    }
}
