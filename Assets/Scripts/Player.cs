using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public double money;
    public TextMeshProUGUI txtMoney;
    // Start is called before the first frame update
    void Start()
    {
        money = 500.00;
        UpdateMoney(money);
    }

    // Update is called once per frame
    public void UpdateMoney(double money)
    {
        txtMoney.text = "$" + money.ToString("F");
    }
}
