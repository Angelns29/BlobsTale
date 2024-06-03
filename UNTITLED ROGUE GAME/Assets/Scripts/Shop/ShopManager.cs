using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Text coinsUI;

    public ItemSO[] shopItemsSO;
    public Button[] purchaseButtons;
    public ShopTemplate[] shopPanels;

    public InventarySO inventoryData;
    void Start()
    {
        inventoryData.Initialize();
        coinsUI.text = GameManager.Instance.playerMoney.ToString();
        LoadPanels();
        CheckPurchaseable();

        //for(int i = 0; i < inventoryData.inventoryItems.Count; i++)
        //{
        //    inventoryData.RemoveItem(i, 1);
        //}
    }

    private void Update()
    {
        AddCoins();
    }

    public void AddCoins()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            GameManager.Instance.playerMoney+= 10;
            coinsUI.text = GameManager.Instance.playerMoney.ToString();
            CheckPurchaseable();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].itemName.text = shopItemsSO[i].itemName;
            shopPanels[i].itemDescription.text = shopItemsSO[i].description;
            shopPanels[i].itemCost.text = shopItemsSO[i].cost.ToString();
            shopPanels[i].itemSprite.sprite = shopItemsSO[i].itemImage;
        }
    }

    public void CheckPurchaseable()
    {
        for(int i = 0;i < shopItemsSO.Length;i++)
        {
            if(GameManager.Instance.playerMoney >= shopItemsSO[i].cost)
            {
                purchaseButtons[i].interactable = true;
            }

            else
            {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNumber)
    {
        if(GameManager.Instance.playerMoney >= shopItemsSO[btnNumber].cost)
        {
            GameManager.Instance.playerMoney = GameManager.Instance.playerMoney - shopItemsSO[btnNumber].cost;
            coinsUI.text = GameManager.Instance.playerMoney.ToString();

            DataManager.instance.SaveData();

            for (int i = 14; i < inventoryData.Size; i++)
            {
                if (inventoryData.inventoryItems[i].IsEmpty)
                {
                    inventoryData.AddItemAtPosition(shopItemsSO[btnNumber], 1, i);
                    CheckPurchaseable();
                    return;
                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (inventoryData.inventoryItems[i].IsEmpty)
                {
                    inventoryData.AddItemAtPosition(shopItemsSO[btnNumber], 1, i);
                    CheckPurchaseable();
                    return;
                }
            }
        }
    }

}
