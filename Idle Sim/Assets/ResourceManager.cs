using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [Header("Oak Settings")]
    public int oakCount = 0;
    public TextMeshProUGUI oakText;

    [Header("Maple Settings")]
    public int mapleCount = 0;
    public TextMeshProUGUI mapleText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddOak(int amount)
    {
        oakCount += amount;
        UpdateUI();
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