using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class UIManagement : MonoBehaviour
{
    public static UIManagement instance;

    [Header("UI References")]
    public GameObject startPanel;
    public GameObject gameInPanel;
    public GameObject settingsPanel;
    public GameObject settingsPanel2;
    public GameObject winPanel;
    public GameObject failPanel;
    public GameObject pausePanel;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject settingsButtonOn;
    [SerializeField] GameObject settingsButtonOff;
    [SerializeField] GameObject soundButtonOn;
    [SerializeField] GameObject soundButtonOff;
    [SerializeField] GameObject vibrateButtonOn;
    [SerializeField] GameObject vibrateButtonOff;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText;

    [Header("Variables")]
    public bool isVibrateActive = true;
    public bool isStarted = false;

    public int currentLevel;
    public int nextLevel;

    [Header("Looper")]
    public List<Scene> scenes = new List<Scene>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }

    public void StartGame()
    {
        //isStarted = true;
        //PathFollower.Instance.speed = 20f;
        //startPanel.SetActive(false);
        //gameInPanel.SetActive(true);
    }

    public void SettingsPanelOn()
    {
        if (!settingsPanel)
            settingsPanel.SetActive(true);
        settingsButtonOn.SetActive(false);
        settingsButtonOff.SetActive(true);
        settingsPanel.SetActive(false);
        settingsPanel2.SetActive(true);
    }
    public void SettingsPanelOff()
    {
        settingsButtonOn.SetActive(true);
        settingsButtonOff.SetActive(false);
        settingsPanel.SetActive(true);
        settingsPanel2.SetActive(false);
    }

    public void PausePanel()
    {
        settingsButtonOff.SetActive(true);
        //pausePanel.GetComponent<Image>().DOFade(1f, .1f)
        //    .OnComplete(() => pausePanel.SetActive(true));
    }
    public void ClosePausePanel()
    {
        settingsButtonOff.SetActive(false);
        //pausePanel.GetComponent<Image>().DOFade(0f, .15f)
        //    .OnComplete(() => pausePanel.SetActive(false));
    }

    public void KeepPlaying()
    {
        //pausePanel.GetComponent<Image>().DOFade(0f, .1f)
        //    .OnComplete(() => pausePanel.SetActive(false));
    }

    public void NextLevel()
    {
        Debug.Log("1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        textMesh.text = SceneManager.GetActiveScene().buildIndex.ToString();
        winPanel.SetActive(false);
        currentLevel++;
        nextLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("NextLevel", nextLevel);
    }
    public void RetryLevel()
    {
        failPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SoundOn()
    {
        Debug.LogWarning("Sound On");
        soundButtonOff.SetActive(true);
        soundButtonOn.SetActive(false);
    }
    public void SoundOff()
    {
        Debug.LogWarning("Sound Off");
        soundButtonOn.SetActive(true);
        soundButtonOff.SetActive(false);
    }

    public void VibrateOn()
    {
        if (isVibrateActive)
            isVibrateActive = false;
        Debug.LogWarning("Vibrate On");
        vibrateButtonOff.SetActive(true);
        vibrateButtonOn.SetActive(false);
    }

    public void VibrateOff()
    {
        if (!isVibrateActive)
            isVibrateActive = true;
        Debug.LogWarning("Vibrate Off");
        vibrateButtonOn.SetActive(true);
        vibrateButtonOff.SetActive(false);
    }

    public void LoopLevels()
    {
        SceneManager.LoadScene(2);
        textMesh.text = SceneManager.GetActiveScene().buildIndex.ToString();
        winPanel.SetActive(false);
        currentLevel++;
        nextLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("NextLevel", nextLevel);
    }

    public void LoopScenes()
    {
        int index = Random.Range(1, 6);
        SceneManager.LoadScene(index);
        textMesh.text = SceneManager.GetActiveScene().buildIndex.ToString();
        winPanel.SetActive(false);
        currentLevel++;
        nextLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("NextLevel", nextLevel);
    }
}
