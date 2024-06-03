using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

    [CreateAssetMenu]
    public class InventarySO : ScriptableObject
    {
        [SerializeField] public List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 16;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }
        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {

                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (IsInventoryFull()) return quantity;
                    while(quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item,1,itemState);
                        
                    }
                    InformAboutChange();
                    return quantity;
                    
                }
            //quantity = AddStackableItem(item,quantity);
            InformAboutChange();
            return quantity;
            
        }

    public void AddItemAtPosition(ItemSO item, int quantity, int position, List<ItemParameter> itemState = null)
    {
        if (position < 0 || position >= inventoryItems.Count) return;
        if (inventoryItems[position].IsEmpty || inventoryItems[position].item == null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState ?? item.DefaultParametersList)
            };
            inventoryItems[position] = newItem;
            InformAboutChange();
        }
    }

    public int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = quantity,
            itemState = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
        };
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty || inventoryItems[i].item == null)
            {
                inventoryItems[i] = newItem;
                return quantity;
            }
        }
        return 0;
    }

    private bool IsInventoryFull() => inventoryItems.All(x => !x.IsEmpty);

    /*private int AddStackableItem(ItemSO item, int quantity)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty || inventoryItems[i].item == null) continue;
            if (inventoryItems[i].item.itemID == item.itemID)
            {
                int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                if (quantity > amountPossibleToTake)
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                    inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity+quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }
        while(quantity > 0 && IsInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item,newQuantity);
        }
        return quantity;
    }*/

    public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                if (inventoryItems[i].IsEmpty || inventoryItems[i].item == null) continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItem item1 = inventoryItems[itemIndex1];
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = item1;
            InformAboutChange();
        }

        public void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty || inventoryItems[itemIndex].item == null) return;
                int reminder = inventoryItems[itemIndex].quantity-amount;
                if (reminder <= 0) inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);
                InformAboutChange();
            }
        }
    }
    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;

    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }
        public static InventoryItem GetEmptyItem() => new InventoryItem
        {
            item = null,
            quantity = 0,
            itemState = new List<ItemParameter>()
        };
    }

