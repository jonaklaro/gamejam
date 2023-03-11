using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D other)
  {

    if (other.gameObject.CompareTag("Resource"))
    {
      Debug.Log("Resource collected");
      Destroy(other.gameObject);
    }
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }
}
