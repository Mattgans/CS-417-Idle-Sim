using UnityEngine;
using System.Collections;

public class ResourceTree : MonoBehaviour
{
    [Header("Resource Settings")]
    public int woodYield = 10;
    public float respawnTime = 10f;
    public float hitCooldown = 0.5f;

    [Header("Visuals")]
    public GameObject fullTreeModel;
    public GameObject cutWoodModel;
    public Animator treeAnimator; // Drag the Animator component here

    private int hitsRemaining;
    private bool isAvailable = true;
    private bool canBeHit = true;
    private Collider treeCollider;

    void Start()
    {
        treeCollider = GetComponent<Collider>();
        
        // If you didn't assign the animator in the inspector, try to find it
        if (treeAnimator == null) 
            treeAnimator = GetComponentInChildren<Animator>();

        ResetTree();
    }

    public void GetHit()
    {
        if (!isAvailable || !canBeHit) return;

        hitsRemaining--;
        Debug.Log("Tree Hit! Hits left: " + hitsRemaining);

        // --- NEW: Trigger the animation ---
        if (treeAnimator != null)
        {
            treeAnimator.Play("tree_wig", 0, 0f); 
            // The 0, 0f restart the animation from the beginning if hit rapidly
        }

        if (hitsRemaining <= 0)
        {
            StartCoroutine(HandleHarvest());
        }
        else
        {
            StartCoroutine(CooldownTimer());
        }
    }

    IEnumerator CooldownTimer()
    {
        canBeHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canBeHit = true;
    }

    IEnumerator HandleHarvest()
    {
        isAvailable = false;
        if(ResourceManager.Instance != null)
            ResourceManager.Instance.AddWood(woodYield);

        fullTreeModel.SetActive(false);
        cutWoodModel.SetActive(true);
        if(treeCollider != null) treeCollider.enabled = false;

        yield return new WaitForSeconds(respawnTime);
        ResetTree();
    }

    void ResetTree()
    {
        hitsRemaining = Random.Range(3, 6);
        isAvailable = true;
        canBeHit = true;
        
        fullTreeModel.SetActive(true);
        cutWoodModel.SetActive(false);
        
        if(treeCollider != null) treeCollider.enabled = true;
    }
}