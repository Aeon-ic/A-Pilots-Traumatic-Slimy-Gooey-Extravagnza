using UnityEngine;

public class NavMeshAgentEnemyHealth : MonoBehaviour, IEnemyHealth
{
  [Tooltip("This is a float that starts at the maximum enemy health and then shows current health")]
  public float enemyHealth;

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
    Destroy(this.gameObject);
  }
}
