using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public bool isMutedBool;
    public Text muteText;
    public bool muteToggleValue;

    public bool isFPSTextActive;
    public Text FPSText;
    public bool FPSToggleValue;

    public bool isFullScreen;
    public Text FullScreenText;
    public bool FullScreenToggleValue;

    public float musicSliderValue;
    public float SFXSliderValue;

    public float musicSliderValueBeforeMuting;
    public float SFXSliderValueBeforeMuting;

    public bool isThereSaveData;

    public int playerMoney;
    //public PowerUp[] powerUps;
    // public List<Slot> slots;
    //public List<Sprite> images;
    //public float sliderMusicValue;
    //public float sliderSFXValue;
    //public bool logroPuntos;
    //public bool logroMatar;
}
