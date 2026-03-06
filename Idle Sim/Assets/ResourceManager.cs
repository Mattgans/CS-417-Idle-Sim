using UnityEngine;
using TMPro;
using System.Collections;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [Header("Oak Settings")]
    public int oakCount = 0;
    public TextMeshProUGUI oakText;
    public int oakGoal = 1000;

    [Header("Maple Settings")]
    public int mapleCount = 0;
    public TextMeshProUGUI mapleText;

    [Header("Unlockables")]
    public GameObject mapleBlockerPlane; // The wall/floor blocking the path
    public GameObject achievementPopup;  // The UI Panel for the achievement
    public float popupDuration = 3f;     // How long the popup stays visible

    private bool mapleUnlocked = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
        if (achievementPopup != null) achievementPopup.SetActive(false);
    }

    public void AddOak(int amount)
    {
        oakCount += amount;
        UpdateUI();

        // Check for the unlock goal
        if (!mapleUnlocked && oakCount >= oakGoal)
        {
            UnlockMapleArea();
        }
    }

    void UnlockMapleArea()
    {
        mapleUnlocked = true;

        // Disable the physical blocker
        if (mapleBlockerPlane != null) 
            mapleBlockerPlane.SetActive(false);

        // Show the achievement popup
        if (achievementPopup != null)
            StartCoroutine(ShowAchievement());
    }

    IEnumerator ShowAchievement()
    {
        achievementPopup.SetActive(true);
        yield return new WaitForSeconds(popupDuration);
        achievementPopup.SetActive(false);
    }

    public void AddMaple(int amount)
    {
        mapleCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (oakText != null) oakText.text = $"Oak: {oakCount}";
        if (mapleText != null) mapleText.text = $"Maple: {mapleCount}";
    }
}