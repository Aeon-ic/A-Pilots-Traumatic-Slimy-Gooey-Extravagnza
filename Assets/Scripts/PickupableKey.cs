using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableKey : Pickupable
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Player.instance.hasKeycard = true;
      Destroy(this.gameObject);
    }
  }
}
