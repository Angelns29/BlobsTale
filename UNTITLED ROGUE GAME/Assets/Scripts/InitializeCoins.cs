using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeCoins : MonoBehaviour
{
    public Text coinsUI;

    void Start()
    {
        coinsUI.text = GameManager.Instance.playerMoney.ToString();
    }
}
