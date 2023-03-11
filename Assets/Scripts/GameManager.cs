using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Friends> friends;
    public bool itemDropped = false;
    [SerializeField] Animator animator;

    public bool hasLevelEnded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        bool allFriendsDroppedItem = true;

        foreach (Friends friend in friends)
        {
            if (!friend.itemDropped)
            {
                allFriendsDroppedItem = false;
                break;
            }
        }

        if (allFriendsDroppedItem)
        {
            itemDropped = true;
            hasLevelEnded = true;
            Debug.Log("All friends have dropped the item - game over!");
        }
    }
}