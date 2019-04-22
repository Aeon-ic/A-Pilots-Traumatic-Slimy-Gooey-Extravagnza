using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  int currHP;
  [Tooltip("This is an int for the max player HP")]
  public int maxHP = 100;
  int currARM;
  [Tooltip("This is an int for the max amount of armor")]
  public int maxARM = 50;
  public static Player instance;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(this);
    }

    DontDestroyOnLoad(this.gameObject);
  }

  public void AddHealth(int healthAdded)
  {
    if (currHP + healthAdded > maxHP)
    {
      currHP = maxHP;
    }
    else
    {
      currHP += healthAdded;
    }
  }

  public void AddArmor(int armorAdded)
  {
    if (currARM + armorAdded > maxARM)
    {
      currARM = maxARM;
    }
    else
    {
      currARM += armorAdded;
    }
  }

  public void TakeDamage(int damage)
  {
    if (currHP - damage <= 0)
    {
      Die();
    }
    else
    {
      currHP -= damage;
    }
  }

  void Die()
  {

  }
}
