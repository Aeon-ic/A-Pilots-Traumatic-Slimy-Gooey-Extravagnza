using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PickupableHealth : Pickupable
{
  [Tooltip("This is the amount of health that is restored to the player on pickup")]
  public int healthRestored = 25;
  AudioSource audioSource;
  [Tooltip("This is the audio that is played on pickup")]
  public AudioClip pickupSound;

  private void Start()
  {
    audioSource = this.gameObject.GetComponent<AudioSource>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Player.instance.player.GetComponent<AudioSource>().PlayOneShot(pickupSound);
      Player.instance.AddHealth(healthRestored);
      Destroy(gameObject);
    }
  }
}
