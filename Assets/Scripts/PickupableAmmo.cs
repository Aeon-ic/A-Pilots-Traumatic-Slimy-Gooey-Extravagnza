using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PickupableAmmo : Pickupable
{
  [Tooltip("This is the amount of health that is restored to the player on pickup")]
  public int ammoRestored = 2;
  public string ammoType = "9mm";
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
      audioSource.PlayOneShot(pickupSound);
      AmmoManager.instance.AddAmmo(ammoType, ammoRestored);
      Destroy(gameObject);
    }
  }
}