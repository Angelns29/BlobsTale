using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InventoryController: MonoBehaviour
{
    //public static InventoryController instance;
    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private InventarySO basementSO;

    [SerializeField]
    private InventarySO inventorySO;

    public int invCharSize;

    public int invBagSize;

    public int invBaseSize;

    [Header("SecureBag")]
    public GameObject secureBag;
    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else Destroy(gameObject);
    }*/
    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
        foreach (var item in basementSO.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.itemImage);
        }
    }
    private void Update()
    {
       // if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) ) inventoryUI = GameObject.FindObjectOfType<UIInventoryPage>().GetComponent<UIInventoryPage>();
        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inventoryUI =  (UIInventoryPage) FindObjectsOfType(typeof(UIInventoryPage))[0];

    }

    private void PrepareInventoryData()
    {
        //basementSO.Initialize();
        basementSO.OnInventoryUpdated += UpdateInventoryUI;
        foreach (var item in basementSO.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.itemImage);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in basementSO.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.itemImage);
        }
        for (int i = 0; i < 14; i++)
        {
            inventorySO.inventoryItems[i] = basementSO.inventoryItems[i];
           //inventorySO.AddItem(basementSO.GetItemAt(i));
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInvUI(invCharSize, invBagSize, invBaseSize);
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2)
    {
        basementSO.SwapItems(itemIndex1, itemIndex2);
        
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = basementSO.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.itemImage);
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) return;

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null) inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventorySO.RemoveItem(itemIndex, quantity);
        //Audio Drop
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null) inventorySO.RemoveItem(itemIndex, 1);

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null) itemAction.PerformAction(gameObject, inventoryItem.itemState);
        //if (inventorySO.GetItemAt(itemIndex).IsEmpty) inventoryUI.ResetSelection(); ;
    }
    public void AddMoreSlots()
    {
        if (secureBag.activeInHierarchy == false)
        {
            secureBag.SetActive(true);
            return;
        }
        if ((invBagSize + 2) < 8) inventoryUI.AddSlots(2);

    }
    public void CopySecurebagAndEquipment()
    {
        for (int i = 0; i < inventorySO.inventoryItems.Count; i++)
        {
            if (i < 14)
            {
                inventorySO.inventoryItems[i] = basementSO.inventoryItems[i];
            }

        }
       
    }
    public void SaveSecurebag()
    {
        for (int i = 0; i < inventorySO.inventoryItems.Count; i++)
        {
            if (i > 5 && i < 14)
            {
                basementSO.inventoryItems[i] = inventorySO.inventoryItems[i];
            }

        }
        inventorySO.Initialize();

    }
    public int CountInventoryItems()
    {
        int count = 79;
        for(int i = 0;i < inventorySO.inventoryItems.Count; i++)
        {
            if (inventorySO.inventoryItems[i].IsEmpty) count--; 
        }
        return count;
    }
    public void SaveInventory()
    {
        int count = CountInventoryItems();
        Debug.Log(count);

            for(int j = 14; j < inventorySO.inventoryItems.Count; j++)
            {
                if (count == 0) return;
            if (basementSO.inventoryItems[j].item != null)
            {
                inventorySO.AddItemToFirstFreeSlot(basementSO.inventoryItems[j].item, basementSO.inventoryItems[j].quantity);
                count--;
            }
                   /* if (inventorySO.inventoryItems[j].item == null)
                    {
                        inventorySO.inventoryItems[i] = basementSO.inventoryItems[j];
                        count--;
                        i++;
                        break;
                    }
                }*/
                
            }
        
    }
    /*
private void Update()
{
if(inventoryUI.isActiveAndEnabled == false)
{
  inventoryUI.Show();

}
else
{
  inventoryUI.Hide();
}
}*/
}
