using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinManager : MonoBehaviour {

    public static int coinAmount;

    Text text;

    // Use this for initialization
    void Awake()
    {

        text = GetComponent<Text>();
        coinAmount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (coinAmount < 10)
        {
            text.text = "x0" + coinAmount;
        }
        else
        {
            text.text = "x" + coinAmount;
        }


    }
}