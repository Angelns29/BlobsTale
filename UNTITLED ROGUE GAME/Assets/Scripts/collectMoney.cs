using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Stats;
using UnityEngine;

public class collectMoney : MonoBehaviour
{
    public GameObject coin;
    public CharacterStatsHandler statsHandler;

    private void Start()
    {
        statsHandler = GameObject.FindWithTag("Player").GetComponent<CharacterStatsHandler>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
       {
            statsHandler.money++;
            Debug.Log("dinero " + statsHandler.money);
            AudioManagerScript.instance.sfxSource.clip = AudioManagerScript.instance.collectCoin;
            AudioManagerScript.instance.sfxSource.Play();
            coin.SetActive(false);
       }
    }
}
