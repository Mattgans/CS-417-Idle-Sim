using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    public int woodCount = 0;
    public TextMeshProUGUI woodText;

    void Awake() => Instance = this;

    public void AddWood(int amount)
    {
        woodCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (woodText != null) woodText.text = "Wood: " + woodCount;
    }
}
