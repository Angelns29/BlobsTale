using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryImage : MonoBehaviour
{
    [SerializeField] 
    private Image itemImage;

    public void Awake()
    {
        ResetImage();
    }

    public void ResetImage()
    {
        this.itemImage.gameObject.SetActive(false);
    }

    public void SetImage(Sprite sprite)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;   
    }
}
