using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class NavMeshAgentEnemyCombat : MonoBehaviour
{
  NavMeshAgent agent;
  [Tooltip("This is an int that defines how much damage the enemy does on a melee hit")]
  public int meleeDamage = 15;
  [Tooltip("This is a float that defines how close an enemy has to be to be melee attacked")]
  public float meleeRange = 1f;
  [Tooltip("This is a float that defines how long of a time between melee attacks")]
  public float meleeCooldown = 1f;
  [Tooltip("This is a float that defines how close the player has to be for the enemy to start chasing")]
  public float aggroRange = 15f;
  Vector3 playerPosition;
  bool attacking = false;

  private void Start()
  {
    agent = gameObject.GetComponent<NavMeshAgent>();
    this.gameObject.GetComponent<CapsuleCollider>().radius = meleeRange;
  }

  // Update is called once per frame
  void Update()
  {
    playerPosition = Player.instance.player.gameObject.transform.position;
    if (Vector3.Distance(playerPosition, this.gameObject.transform.position) <= aggroRange)
    {
      if (attacking)
      {
        agent.SetDestination(this.gameObject.transform.position);
      }
      else
      {
        agent.SetDestination(playerPosition);
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      attacking = true;
      StartCoroutine(Attack());
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      attacking = false;
      StopAllCoroutines();
    }
  }

  public IEnumerator Attack()
  {
    float nextAttack = Time.time + meleeCooldown;
    while (true)
    {
      if (Time.time >= nextAttack)
      {
        nextAttack = Time.time + meleeCooldown;
        Player.instance.TakeDamage(meleeDamage);
      }
      yield return null;
    }
  }
}
