using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Animator animator;

    public AnimationClip flipClip;
    public AnimationClip reverseClip;

    public GameObject panelPunto1;
    public GameObject panelPunto2;
    public GameObject panelPunto3;

    public GameObject pabloShopPanel;
    public GameObject pabloProfilePanel;
    public GameObject maliciaShopPanel;
    public GameObject maliciaProfilePanel;

    public float initialAudio = -20f;

    [SerializeField]public GameObject currentPanel;

    public Text playerMoneyText;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        AudioManagerScript.instance.musicSource.clip = AudioManagerScript.instance.menuTheme;
        AudioManagerScript.instance.musicSource.Play();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerMoneyText.text = GameManager.Instance.playerMoney.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadStartAnimationTransition()
    {
        StartCoroutine(LoadStartAnimation());
    }

    public void LoadReverseAnimationTransition()
    {
        StartCoroutine(LoadReverseAnimation());
    }

    IEnumerator LoadStartAnimation()
    {
        animator.SetTrigger("StartGame");

        yield return new WaitForSeconds(flipClip.length + 0.1f);
    }

    IEnumerator LoadReverseAnimation()
    {
        animator.SetTrigger("BackToMenu");

        yield return new WaitForSeconds(reverseClip.length + 0.1f);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitBase()
    {
        GameManager.Instance.isInMainMenu = false;
    }

    public void LoadBuyPanel()
    {
        
    }

    public void ActiveInventoryPanel()
    {
        StartCoroutine(SetInventoryPanelActive());
    }

    public void CloseBook()
    {
        StartCoroutine(CloseBookCoroutine());
    }
    public void ReturnMenuTheme()
    {
        AudioManagerScript.instance.StartMenuTheme();
    }
    IEnumerator SetInventoryPanelActive()
    {
        yield return new WaitForSeconds(0.5f);

        panelPunto1.SetActive(true);
    }


    IEnumerator CloseBookCoroutine()
    {
        yield return new WaitForSeconds(0.3f);

        panelPunto1.SetActive(false);
        panelPunto2.SetActive(false);
        panelPunto3.SetActive(false);
    }




}
