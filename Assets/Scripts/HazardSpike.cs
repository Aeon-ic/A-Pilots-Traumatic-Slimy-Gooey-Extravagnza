using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpike : MonoBehaviour
{
  [Tooltip("This is an int that defines how much damage the player takes on contact")]
  public int damage = 10;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      Player.instance.TakeDamage(damage);
      Destroy(this.gameObject);
    }
  }
}
