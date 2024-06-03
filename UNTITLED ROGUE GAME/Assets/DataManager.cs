using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public string SaveFiles;
    public GameData gameData = new GameData();

    public bool isThereSaveFile;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SaveFiles = Application.persistentDataPath + "/GameData.json"; //La localizaci�n de la carpeta donde est�n las SaveFiles
    }

    [RuntimeInitializeOnLoadMethod]
    public void LoadData()
    {
        if (File.Exists(SaveFiles))
        {
            isThereSaveFile = true;

            string content = File.ReadAllText(SaveFiles);
            Debug.Log("JSON Content: " + content);
            GameData loadedData = JsonUtility.FromJson<GameData>(content);

            //_____________________________________________________________
            gameData.isMutedBool = loadedData.isMutedBool;
            gameData.muteText = loadedData.muteText;
            gameData.muteToggleValue = loadedData.muteToggleValue;

            gameData.isFPSTextActive = loadedData.isFPSTextActive;
            gameData.FPSText = loadedData.FPSText;
            gameData.FPSToggleValue = loadedData.FPSToggleValue;

            gameData.isFullScreen = loadedData.isFullScreen;
            gameData.FullScreenText = loadedData.FullScreenText;
            gameData.FullScreenToggleValue = loadedData.FullScreenToggleValue;

            gameData.musicSliderValue = loadedData.musicSliderValue;
            gameData.SFXSliderValue = loadedData.SFXSliderValue;

            gameData.musicSliderValueBeforeMuting = loadedData.musicSliderValueBeforeMuting;
            gameData.SFXSliderValueBeforeMuting = loadedData.SFXSliderValueBeforeMuting;

            gameData.isThereSaveData = loadedData.isThereSaveData;

            gameData.playerMoney = loadedData.playerMoney;



            GameManager.Instance.isMutedBool = gameData.isMutedBool;
            GameManager.Instance.mutedText = gameData.muteText;
            GameManager.Instance.mutedToggleValue = gameData.muteToggleValue;

            GameManager.Instance.isFPSTextActive = gameData.isFPSTextActive;
            GameManager.Instance.FPSText = gameData.FPSText;
            GameManager.Instance.FPSToggleValue = gameData.FPSToggleValue;

            GameManager.Instance.isFullScreen = gameData.isFullScreen;
            GameManager.Instance.FullScreenText = gameData.FullScreenText;
            GameManager.Instance.FullScreenToggleValue = gameData.FullScreenToggleValue;

            GameManager.Instance.musicSliderValue = gameData.musicSliderValue;
            GameManager.Instance.SFXSliderValue = gameData.SFXSliderValue;

            GameManager.Instance.musicSliderValueBeforeMuting = gameData.musicSliderValueBeforeMuting;
            GameManager.Instance.SFXSliderValueBeforeMuting = gameData.SFXSliderValueBeforeMuting;

            GameManager.Instance.isThereSaveData = gameData.isThereSaveData;

            GameManager.Instance.playerMoney = gameData.playerMoney;
        }

        else
        {
            Debug.Log("El archivo de guardado no existe");
        }
    }

    public void SaveData()
    {
        isThereSaveFile = true;

        GameData newData = new GameData();
        {
            newData.isMutedBool = GameManager.Instance.isMutedBool;
            newData.muteText = GameManager.Instance.mutedText;
            newData.muteToggleValue = GameManager.Instance.mutedToggleValue;

            newData.isFPSTextActive = GameManager.Instance.isFPSTextActive;
            newData.FPSText = GameManager.Instance.FPSText;
            newData.FPSToggleValue = GameManager.Instance.FPSToggleValue;

            newData.isFullScreen = GameManager.Instance.isFullScreen;
            newData.FullScreenText = GameManager.Instance.FullScreenText;
            newData.FullScreenToggleValue = GameManager.Instance.FullScreenToggleValue;

            newData.musicSliderValue = GameManager.Instance.musicSliderValue;
            newData.SFXSliderValue = GameManager.Instance.SFXSliderValue;

            newData.musicSliderValueBeforeMuting = GameManager.Instance.musicSliderValueBeforeMuting;
            newData.SFXSliderValueBeforeMuting = GameManager.Instance.SFXSliderValueBeforeMuting;

            GameManager.Instance.isThereSaveData = true;

            newData.isThereSaveData = GameManager.Instance.isThereSaveData;

            newData.playerMoney = GameManager.Instance.playerMoney;
        };

        string JsonString = JsonUtility.ToJson(newData, true);

        File.WriteAllText(SaveFiles, JsonString);

        Debug.Log("Saved File");
    }
}
