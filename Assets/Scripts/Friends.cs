using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friends : MonoBehaviour
{

  public GameObject itemPrefab; // The item to drop
  public float dropDelay = 2f; // The delay before dropping the item
  public float dropRadius = 2f; // The range in which the item will drop

  [SerializeField] Animator animator;

  private bool playerInRange = false; // Whether the player is currently in range
  private float inRangeTime = 0f; // The time the player has been in range

  private bool itemDropped = false; // Whether the item has been dropped

  SoundManager soundManager;
  AudioSource audioSource;


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


      // if (itemDropped)
      // {
      //   //GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
      //   // itemDropped = false; // Reset itemDropped variable
      //   //set itemDropped to false after a certain amount of time
      //   StartCoroutine(ResetItemDrop());
      // }
      // else
      // {
      //   //GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
      // }

    }
  }

  IEnumerator ResetItemDrop()
  {
    yield return new WaitForSeconds(20f);
    itemDropped = false;
    animator.SetBool("isEmpty", false);
    //GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);

  }

  private void Start()
  {
    soundManager = SoundManager.instance;
    audioSource = GetComponent<AudioSource>();
  }

  private void Update()
  {
    if (playerInRange && !itemDropped)
    {
      // change color of this object to orange
      //GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f);

      //Debug.Log("Player is in range");
      inRangeTime += Time.deltaTime;

      if (inRangeTime >= dropDelay)
      {

        //GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
        //Debug.Log("Player has been in range for " + dropDelay + " seconds");
        // Drop the item
        DropItem();

        // Reset variables
        playerInRange = false;
        inRangeTime = 0f;
        // Set itemDropped to true to prevent the item from being dropped again
        itemDropped = true;

      }
    }
  }


  private void DropItem()
  {
    animator.SetBool("isEmpty", true);

    // Instantiate the item prefab at the position of the entity and accelerate it in the player direction
    GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
    Rigidbody2D itemRigidbody = item.GetComponent<Rigidbody2D>();

    // Calculate the direction towards the player
    Vector2 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;

    // Add an impulse force to the rigidbody in the player direction
    itemRigidbody.AddForce(direction * 2f, ForceMode2D.Impulse);

    // Set the drag property of the rigidbody to gradually slow down the projectile
    itemRigidbody.drag = 1f;

    AudioClip clip = soundManager.GetAudioClip("fill");
    audioSource.clip = clip;
    audioSource.Play();
  }

}
