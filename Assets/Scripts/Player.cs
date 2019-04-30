using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [Tooltip("This is an int for the max player HP")]
  public int maxHP = 100;
  [Tooltip("This is an int for the max amount of armor")]
  public int maxARM = 50;
  [Tooltip("This is an int for the starting hp")]
  public int startingHP = 100;
  [Tooltip("This is an int for the starting armor")]
  public int startingARM = 50;
  [Tooltip("This is an int containing the current hp")]
  public int currHP;
  [Tooltip("This is an int containing the current armor")]
  public int currARM;
  public static Player instance;
  public GameObject player;

  private void Awake()
  {
    //Singleton code
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(this);
    }

    DontDestroyOnLoad(this.gameObject);

    //Setup player reference
    player = GameObject.FindGameObjectWithTag("Player");

    //Setup current hp and armor
    currHP = startingHP;
    currARM = startingARM;
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
    //Take into account armor on the incoming damage
    if (currARM > 0)
    {
      if (currARM - damage > 0)
      {
        currARM -= damage;
        damage = 0;
      }
      else
      {
        damage -= currARM;
        currARM = 0;
      }
    }

    //Check for any damage left after armor has done it's job
    if (damage > 0)
    {
      currHP -= damage;
    }

    //Check if player has died
    if (currHP <= 0)
    {
      Die();
    }
  }

  void Die()
  {
    Debug.Log("Dead");
    Time.timeScale = 0;
  }
}
