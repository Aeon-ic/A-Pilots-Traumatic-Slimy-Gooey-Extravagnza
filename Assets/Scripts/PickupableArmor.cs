using UnityEngine;

public class PickupableArmor : Pickupable
{
  [Tooltip("This is the amount of health that is restored to the player on pickup")]
  public int armorRestored = 10;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Player.instance.AddArmor(armorRestored);
      Destroy(gameObject);
    }
  }
}