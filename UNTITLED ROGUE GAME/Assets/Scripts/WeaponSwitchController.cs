using System;
using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D;
using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Attacks.Melee;
using TopDownCharacter2D.Stats;
using UnityEngine;

public class WeaponSwitchController : MonoBehaviour
{
    [SerializeField] private GameObject _weaponSprite;
    [SerializeField] private GameObject _bulletSpawnPoint;

    [SerializeField] private AttackConfig _weaponRange;
    [SerializeField] private AttackConfig _weaponMelee;

    private CharacterStatsHandler _statsHandler;

    private void Start()
    {
        _statsHandler = gameObject.GetComponent<CharacterStatsHandler>();
        OnRangeWeapon();
    }


    public void OnRangeWeapon()
    {
        gameObject.GetComponent<TopDownShooting>().enabled = true;
        gameObject.GetComponent<TopDownAimRotation>().enabled = true;

        gameObject.GetComponent<MeleeAttackController>().enabled = false;
        gameObject.GetComponent<TopDownMelee>().enabled = false;

        _weaponSprite.SetActive(true);
        _bulletSpawnPoint.SetActive(true);

        _statsHandler.baseStats.attackConfig = _weaponRange;
    }

    public void OnMeleeWeapon()
    {
        gameObject.GetComponent<TopDownShooting>().enabled = false;
        gameObject.GetComponent<TopDownAimRotation>().enabled = false;

        gameObject.GetComponent<MeleeAttackController>().enabled = true;
        gameObject.GetComponent<TopDownMelee>().enabled = true;

        _weaponSprite.SetActive(false);
        _bulletSpawnPoint.SetActive(false);

        _statsHandler.baseStats.attackConfig = _weaponMelee;
    }

    public void SwapRangeWeapon(AttackConfig newWeapon)
    {
        _weaponRange = newWeapon;
        OnRangeWeapon();
    }

    public void SwapMeleeWeapon(AttackConfig newWeapon)
    {
        _weaponMelee = newWeapon;
        OnMeleeWeapon();
    }
}
