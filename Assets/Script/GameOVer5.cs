using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Advertisements;

public class GameOver : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    private string _adUnitId;

    public GameObject gameOverPanel;
    public GameObject continuePanel;
    public TextMeshProUGUI timerText;

    public GameObject playerPrefab;
    public Transform spawnPoint;
    public LoopingBackground loopingBackground; // <<-- เพิ่มเข้ามา
    public float defaultMoveSpeed = 5f;

    private float timer;
    private bool isContinuing = false;

    void Awake()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;
    }

    void Start()
    {
        timer = PlayerPrefs.GetFloat("LastPlayTime", 0f); // โหลดเวลาที่บันทึกไว้
        LoadAd();
        if (continuePanel != null)
            continuePanel.SetActive(false);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null && !isContinuing)
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }

        if ((gameOverPanel != null && !gameOverPanel.activeSelf) || isContinuing)
        {
            timer += Time.deltaTime;
        }

        if (timerText != null)
        {
            timerText.text = "Time: " + timer.ToString("F2") + "s";
        }
    }

    // ========================== Advertisement Section =============================

    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId) { }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }

    public void OnUnityAdsShowClick(string _adUnitId) { }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED || showCompletionState == UnityAdsShowCompletionState.SKIPPED)
        {
            Debug.Log("Ad finished or skipped. Showing continue panel.");
            if (continuePanel != null)
                continuePanel.SetActive(true);
        }
    }

    // ========================== Game Control Section =============================

    public void OnClickWatchAd()
    {
        // ดีบักก่อนที่จะปิด gameOverPanel
        Debug.Log("Watch Ad button clicked. Destroying gameOverPanel.");

        // ทำลาย gameOverPanel เมื่อกดปุ่มดูโฆษณา
        if (gameOverPanel != null)
        {
            Destroy(gameOverPanel); // ทำลาย gameOverPanel
            Debug.Log("gameOverPanel has been destroyed.");
        }

        // แสดงโฆษณา
        ShowAd();
    }

    public void OnClickContinueGame()
    {
        Debug.Log("Player chooses to continue the game.");

        if (gameOverPanel != null)
            Destroy(gameOverPanel); // ทำลาย gameOverPanel

        if (continuePanel != null)
            continuePanel.SetActive(false);

        // บันทึกเวลาเมื่อเลือก continue
        PlayerPrefs.SetFloat("LastPlayTime", timer);

        // โหลดซีนใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // ตั้งค่า isContinuing ให้เป็น true
        isContinuing = true;
    }

    public void Restart()
    {
        // รีเซ็ตเวลาใหม่เมื่อเลือก Restart
        timer = 0f; // ตั้งค่า timer เป็น 0

        // รีเซ็ตเวลาแล้วโหลดซีนใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // ให้ UI ของเวลาอัปเดตด้วย
        if (timerText != null)
        {
            timerText.text = "Time: " + timer.ToString("F2") + "s";
        }

        // รีเซ็ตการบันทึกเวลาใน PlayerPrefs
        PlayerPrefs.SetFloat("LastPlayTime", 0f);
    }

    // ========================== Save and Load Time =============================

    // เมื่อเกมโอเวอร์ให้บันทึกเวลา
    public void GameOverR()
    {
        PlayerPrefs.SetFloat("LastPlayTime", timer);
    }
}
