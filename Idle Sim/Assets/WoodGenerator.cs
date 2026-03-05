using UnityEngine;

public class OakGenerator : MonoBehaviour
{
    public int oakPerTick = 10;
    public float tickInterval = 5f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            if (ResourceManager.Instance != null)
            {
                ResourceManager.Instance.AddOak(oakPerTick);
            }
            timer = 0f;
        }
    }
}