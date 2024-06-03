using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{

    [SerializeField] private int _enemies;
    public EventHandler OnEnemiesKilled;
    public WinCanvasManager canvasManager;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<EnemyController>().OnKilled += SubstractEnemyFromList;
        canvasManager = GameObject.Find("Canvas").GetComponent<WinCanvasManager>();
    }

    private void Update()
    {
        if (_enemies <= 0)
        {
            OnEnemiesKilled?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            if (GameManager.Instance.currentLevel == 4 || GameManager.Instance.currentLevel == 9 | GameManager.Instance.currentLevel==14) canvasManager.ShowCheckpointMenu();
            else
            {
                GameManager.Instance.NewLevel();
                collision.gameObject.transform.position = new Vector2(0, 0);
            }
        }
    }

    private void SubstractEnemyFromList(object sender, EventArgs e)
    {
        _enemies--;
    }
}
