using System.Collections.Generic;
using UnityEngine;

public interface IItemAction
{
    public string ActionName { get; }
    public AudioClip actionSFX { get; }
    bool PerformAction(GameObject character, List<ItemParameter> itemState);
}
