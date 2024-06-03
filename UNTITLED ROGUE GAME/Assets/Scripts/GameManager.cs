using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TopDownCharacter2D.Stats;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> EasyLevels = new List<GameObject>();
    public List<GameObject> MidLevels = new List<GameObject>();
    public List<GameObject> DifLevels = new List<GameObject>();
    public List<GameObject> BossLevels = new List<GameObject>();

    public int currentLevel = 1;

    public bool isMutedBool = false;
    public bool isFPSTextActive;
    public bool isFullScreen = false;
    public bool isBossDefeated = false;

    public Text mutedText;
    public Text FPSText;
    public Text FullScreenText;

    public bool mutedToggleValue = false;
    public bool FPSToggleValue = false;
    public bool FullScreenToggleValue = false;

    public float musicSliderValue;
    public float SFXSliderValue;

    public float musicSliderValueBeforeMuting;
    public float SFXSliderValueBeforeMuting;

    public bool isThereSaveData = false;
    public bool isInMainMenu = true;
    public WinCanvasManager canvasManager;

    private InventoryController inventoryController;
    public int playerMoney = 0;


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadData();

        Screen.fullScreen = isFullScreen;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetSceneByBuildIndex(1) == scene)
        {
            LevelChange();
        }
        
    }

    void Start()
    {
        //LevelChange();
        inventoryController = GameObject.Find("InventoryController").GetComponent<InventoryController>();
        currentLevel = 1;
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        Debug.Log("GameManager Awake");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else Destroy(gameObject);
    }

    public void LoadData()
    {
        DataManager.instance.LoadData();
        if (DataManager.instance.isThereSaveFile)
        {
            isThereSaveData = true;

            Debug.Log("Loaded values: Music Slider Before Muting: " + musicSliderValueBeforeMuting + ", SFX Slider Before Muting: " + SFXSliderValueBeforeMuting);
        }


    }
    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            canvasManager = GameObject.Find("Canvas").GetComponent<WinCanvasManager>();

        }
    }
    public void LevelChange()
    {
        switch (currentLevel)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                Instantiate(EasyLevels[Random.Range(0, EasyLevels.Count)]);
                //EasyLevels[Random.Range(0, EasyLevels.Count)].SetActive(true);
                break;
            case 5:
                Instantiate(BossLevels[Random.Range(0, BossLevels.Count)]);
                //BossLevels[Random.Range(0, BossLevels.Count)].SetActive(true);
                break;
            case 6:
            case 7:
            case 8:
            case 9:
                Instantiate(MidLevels[Random.Range(0, MidLevels.Count)]);
                //MidLevels[Random.Range(0, MidLevels.Count)].SetActive(true);
                break;
            case 10:
                Instantiate(BossLevels[Random.Range(0, BossLevels.Count)]);
                //BossLevels[Random.Range(0, BossLevels.Count)].SetActive(true);
                break;
            case 11:
            case 12:
            case 13:
            case 14:
                Instantiate(DifLevels[Random.Range(0, DifLevels.Count)]);
                ///DifLevels[Random.Range(0, DifLevels.Count)].SetActive(true);
                break;
            case 15:
                Instantiate(BossLevels[Random.Range(0, BossLevels.Count)]);
                //BossLevels[Random.Range(0, BossLevels.Count)].SetActive(true);
                break;
            case 16:
                SceneManager.LoadScene("Win");
                break;
        }
    }
    public void NewLevel()
    {
        currentLevel++;
        LevelChange();
    }
    public void SetGameOver()
    {
        canvasManager.ShowGameOverMenu();
        inventoryController.SaveSecurebag();
    }
}
