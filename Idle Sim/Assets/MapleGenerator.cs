using UnityEngine;

public class MapleGenerator : MonoBehaviour
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
                ResourceManager.Instance.AddMaple(oakPerTick);
            }
            timer = 0f;
        }
    }
}