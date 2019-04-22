using UnityEngine;

public class PickupableHealth : Pickupable
{
  [Tooltip("This is the amount of health that is restored to the player on pickup")]
  public int healthRestored = 25;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Player.instance.AddHealth(healthRestored);
      Destroy(gameObject);
    }
  }
}
