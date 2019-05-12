using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NavMeshAgentEnemyHealth : MonoBehaviour, IEnemyHealth
{
  [Tooltip("This is a float that starts at the maximum enemy health and then shows current health")]
  public float enemyHealth;
  [Tooltip("This is the audio clip that is played randomly while the enemy is alive")]
  public AudioClip soundEffect;
  [Tooltip("This is a float that defines the minimum amount of time between sound effects")]
  public float minSoundEffectTime = 1f;
  [Tooltip("This is a float that defines the maximum amount of time between sound effects")]
  public float maxSoundEffectTime = 10f;
  AudioSource audioSource;

  private void Start()
  {
    audioSource = this.gameObject.GetComponent<AudioSource>();
    StartCoroutine(RandomSounds());
  }

  public void TakeDamage(float damage)
  {
    enemyHealth -= damage;
    if (enemyHealth <= 0)
    {
      Die();
    }
  }

  public void Die()
  {
    StopAllCoroutines();
    Destroy(this.gameObject);
  }

  IEnumerator RandomSounds()
  {
    while (true)
    {
      audioSource.PlayOneShot(soundEffect);
      yield return new WaitForSecondsRealtime(Random.Range(minSoundEffectTime, maxSoundEffectTime));
    }
  }
}
