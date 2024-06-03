using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Attacks;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponColectable : MonoBehaviour
{
    private Sprite colectableSprite;

    [SerializeField] private List<AttackConfig> weaponRangedList;
    [SerializeField] private List<AttackConfig> weaponMeleeList;
    [SerializeField] private List<ComplementsSO> complementsList;
    [SerializeField] private List<EquipmentSO> equipmentList;

    private AttackConfig colectableSelected;

    private ComplementsSO complementSelected;
    private EquipmentSO equipmentSelected;

    private int colectableType;

    private void Start()
    {
        colectableType = Random.Range(0, 3);
        switch (colectableType)
        {
            case 0:
                colectableSelected = weaponRangedList[Random.Range(0, weaponRangedList.Count)];
                colectableSprite = colectableSelected.itemImage;
                break;

            case 1:
                colectableSelected = weaponMeleeList[Random.Range(0, weaponMeleeList.Count)];
                colectableSprite = colectableSelected.itemImage;
                break;

            case 2:
                complementSelected = complementsList[Random.Range(0, complementsList.Count)];
                colectableSprite = complementSelected.itemImage;
                break;

            case 3:
                equipmentSelected = equipmentList[Random.Range(0, equipmentList.Count)];
                colectableSprite = equipmentSelected.itemImage;
                break;
        }

        

        gameObject.GetComponent<SpriteRenderer>().sprite = colectableSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (colectableType)
            {
                case 0:
                    collision.GetComponent<WeaponSwitchController>().SwapRangeWeapon(colectableSelected);
                    break;

                case 1:
                    collision.GetComponent<WeaponSwitchController>().SwapMeleeWeapon(colectableSelected);
                    break;
                case 2:
                    //add complement on inventory
                    break;

                case 3:
                    //add Equipment on inventory
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}
