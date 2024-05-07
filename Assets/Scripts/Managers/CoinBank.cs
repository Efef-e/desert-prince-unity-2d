using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBank : MonoBehaviour
{

    public int bank;
    public Text bankText;
    public static CoinBank instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bankText.text = "X" + bank.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Money(int coinCollected)
    {
        bank += coinCollected;
        bankText.text = "X" + bank.ToString();
    }
}
