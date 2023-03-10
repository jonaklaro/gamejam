using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : MonoBehaviour
{

  public GameObject itemPrefab; // The item to drop
  public float dropDelay = 2f; // The delay before dropping the item
  public float dropRadius = 2f; // The range in which the item will drop

  private bool playerInRange = false; // Whether the player is currently in range
  private float inRangeTime = 0f; // The time the player has been in range
                                  // Start is called before the first frame update
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      playerInRange = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      playerInRange = false;
      inRangeTime = 0f;
    }
  }

  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (playerInRange)
        {
            inRangeTime += Time.deltaTime;

            if (inRangeTime >= dropDelay)
            {
                // Drop the item
                GameObject item = Instantiate(itemPrefab, transform.position + transform.up, Quaternion.identity);
                item.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);

                // Reset variables
                playerInRange = false;
                inRangeTime = 0f;
            }
        }
  }

  void DropItem()
  {
    //drop item
  }
}
