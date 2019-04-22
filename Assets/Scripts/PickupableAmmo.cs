using UnityEngine;

public class PickupableAmmo : Pickupable
{
  [Tooltip("This is the amount of health that is restored to the player on pickup")]
  public int ammoRestored = 2;
  public string ammoType = "9mm";

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      AmmoManager.instance.AddAmmo(ammoType, ammoRestored);
      Destroy(gameObject);
    }
  }
}