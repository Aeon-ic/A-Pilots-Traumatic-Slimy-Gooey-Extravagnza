using UnityEngine;

public class KeyDoor : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      if (Player.instance.hasKeycard)
      {
        Destroy(this.gameObject);
      }
    }
  }
}
