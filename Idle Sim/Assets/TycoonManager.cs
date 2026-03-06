using UnityEngine;
using TMPro;

public class TycoonManager : MonoBehaviour
{
    public static TycoonManager Instance;

    [Header("Purchase Math")]
    [Tooltip("The cost increases by this factor after every purchase.")]
    public float priceMultiplier = 1.5f; 

    [Header("Oak Generator Settings")]
    public int oakGenBaseCost = 10;
    public int oakGenCount = 0;
    public float oakProductionMultiplier = 1f;
    [Space]
    public TextMeshProUGUI oakCostText;
    public TextMeshProUGUI oakMultText;
    public GameObject[] oakGenModels; // Assign your 4 Oak Generator objects here

    [Header("Maple Generator Settings")]
    public int mapleGenBaseCost = 50;
    public int mapleGenCount = 0;
    public float mapleProductionMultiplier = 1f;
    [Space]
    public TextMeshProUGUI mapleCostText;
    public TextMeshProUGUI mapleMultText;
    public GameObject[] mapleGenModels; // Assign your 4 Maple Generator objects here

    [Header("End Game Goals")]
    public int oakHouseCost = 5000;
    public int mapleHouseCost = 5000;
    public GameObject oakHouseModel;
    public GameObject mapleHouseModel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateTycoonUI();
    }

    // --- OAK LOGIC ---

    public void BuyOakGenerator()
    {
        int currentCost = GetCurrentCost(oakGenBaseCost, oakGenCount);
        
        if (ResourceManager.Instance.oakCount >= currentCost && oakGenCount < 4)
        {
            ResourceManager.Instance.AddOak(-currentCost);
            oakGenModels[oakGenCount].SetActive(true);
            oakGenCount++;
            UpdateTycoonUI();
        }
    }

    public void UpgradeOakMultiplier()
    {
        // Simple logic: upgrade costs 100 Oak, gives +0.5x, max 10x
        if (ResourceManager.Instance.oakCount >= 100 && oakProductionMultiplier < 10f)
        {
            ResourceManager.Instance.AddOak(-100);
            oakProductionMultiplier += 0.5f;
            UpdateTycoonUI();
        }
    }

    // --- MAPLE LOGIC ---

    public void BuyMapleGenerator()
    {
        int currentCost = GetCurrentCost(mapleGenBaseCost, mapleGenCount);

        if (ResourceManager.Instance.mapleCount >= currentCost && mapleGenCount < 4)
        {
            ResourceManager.Instance.AddMaple(-currentCost);
            mapleGenModels[mapleGenCount].SetActive(true);
            mapleGenCount++;
            UpdateTycoonUI();
        }
    }

    public void UpgradeMapleMultiplier()
    {
        if (ResourceManager.Instance.mapleCount >= 100 && mapleProductionMultiplier < 10f)
        {
            ResourceManager.Instance.AddMaple(-100);
            mapleProductionMultiplier += 0.5f;
            UpdateTycoonUI();
        }
    }

    // --- END GAME PURCHASES ---

    public void BuyOakHouse()
    {
        if (ResourceManager.Instance.oakCount >= oakHouseCost)
        {
            ResourceManager.Instance.AddOak(-oakHouseCost);
            oakHouseModel.SetActive(true);
        }
    }

    public void BuyMapleHouse()
    {
        if (ResourceManager.Instance.mapleCount >= mapleHouseCost)
        {
            ResourceManager.Instance.AddMaple(-mapleHouseCost);
            mapleHouseModel.SetActive(true);
        }
    }

    // --- UTILS ---

    private int GetCurrentCost(int baseCost, int count)
    {
        // Exponential Formula: BaseCost * (Multiplier ^ NumberOwned)
        return Mathf.RoundToInt(baseCost * Mathf.Pow(priceMultiplier, count));
    }

    void UpdateTycoonUI()
    {
        if (oakCostText != null) 
            oakCostText.text = $"Cost: {GetCurrentCost(oakGenBaseCost, oakGenCount)} Oak";
        if (oakMultText != null) 
            oakMultText.text = $"Mult: {oakProductionMultiplier}x";

        if (mapleCostText != null) 
            mapleCostText.text = $"Cost: {GetCurrentCost(mapleGenBaseCost, mapleGenCount)} Maple";
        if (mapleMultText != null) 
            mapleMultText.text = $"Mult: {mapleProductionMultiplier}x";
    }
}