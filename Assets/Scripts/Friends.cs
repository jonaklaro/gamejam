using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : MonoBehaviour
{

  // public GameObject itemPrefab; // The item to drop
  public float dropDelay = 2f; // The delay before dropping the item
  public float dropRadius = 2f; // The range in which the item will drop

  private bool playerInRange = false; // Whether the player is currently in range
  private float inRangeTime = 0f; // The time the player has been in range

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      playerInRange = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      playerInRange = false;
      inRangeTime = 0f;
    }
  }

  private void Update()
  {
    if (playerInRange)
    {
      // change color of this object to orange
      GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f);

      Debug.Log("Player is in range");
      inRangeTime += Time.deltaTime;

      if (inRangeTime >= dropDelay)
      {

        GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
        Debug.Log("Player has been in range for " + dropDelay + " seconds");
        // Drop the item
        // DropItem();

        // Reset variables
        playerInRange = false;
        inRangeTime = 0f;
      }
    }
  }

  // void DropItem()
  // {
  //   GameObject item = Instantiate(itemPrefab, transform.position + transform.up, Quaternion.identity);
  //   item.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
  // }
}
