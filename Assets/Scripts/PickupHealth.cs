using UnityEngine;

public class PickupHealth : MonoBehaviour
{
    [Tooltip("This is the amount of health that is restored to the player on pickup")]
    public int healthRestored = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().AddHealth(healthRestored);
            Destroy(this.gameObject);
        }
    }
}
