using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // This is a singleton class
    public static LevelManager Instance;
    public int itemsToCollect;
    private int itemsCollected = 0;

    public bool isBossLevel = false;

    public void CollectItem()
    {
        itemsCollected++;

        if (itemsCollected >= itemsToCollect && !isBossLevel)
        {
          itemsCollected = 0;
          Debug.Log("Level completed!");
          GameManager.Instance.hasLevelEnded = true;
            // Trigger level completion logic here
        }
    }
}