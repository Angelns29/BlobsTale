using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [Header("Important")]
    [SerializeField]
    private UIInventoryItem itemPrefab;
    [SerializeField]
    private UIInventoryItem itemBasePrefab;
    [SerializeField]
    private UIInventoryImage invImage;
    [SerializeField]
    private MouseFollower mouseFollower;

    [Header("Paneles")]
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private RectTransform contentBagPanel;
    [SerializeField]
    private RectTransform contentBasePanel;

    [Header("Others")]
    List<UIInventoryItem> inventoryItems = new List<UIInventoryItem>();
    public event Action<int> OnImageRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;
    private int currentDragItem = -1;
    public GameObject secureBag;

    [SerializeField] private ItemActionPanel actionPanel;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        invImage.ResetImage();
        if (GameManager.Instance.isBossDefeated == false) secureBag.SetActive(false);
        else secureBag.SetActive(true);
    }

    public void InitializeInvUI(int invCharSize, int invBagSize, int invBaseSize)
    {
        for (int i = 0; i < invCharSize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            inventoryItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }

        for (int i = 0; i < invBagSize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentBagPanel);
            inventoryItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }

        for (int i = 0; i < invBaseSize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemBasePrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentBasePanel);
            inventoryItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    public void AddSlots(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentBagPanel);
            inventoryItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    public void UpdateData(int index, Sprite image)
    {
        if (inventoryItems.Count > index) 
        {
            inventoryItems[index].SetData(image);
        }
    }

    private void HandleShowItemActions(UIInventoryItem item)
    {
        int index = inventoryItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(UIInventoryItem item)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UIInventoryItem item)
    {
        int index = inventoryItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentDragItem, index);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentDragItem = -1;
    }

    private void HandleBeginDrag(UIInventoryItem item)
    {
        int index = inventoryItems.IndexOf(item);
        if (index == -1) return;
        currentDragItem = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);
        
    }

    public void CreateDraggedItem(Sprite image)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image);
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        int index = inventoryItems.IndexOf(item);
        if (index == -1) return;
        OnImageRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        invImage.ResetImage();
        ResetSelection();
    }

    private void ResetSelection()
    {
        invImage.ResetImage();
        DeselectAllItems();
    }
    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButton(actionName, performAction);
    }
    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = inventoryItems[itemIndex].transform.position;
    }
    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in inventoryItems)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);  
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    internal void ResetAllItems()
    {
        foreach (var item in inventoryItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
