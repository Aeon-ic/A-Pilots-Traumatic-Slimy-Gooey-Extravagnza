using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
  [ReadOnly]
  public int currHP;
  [Tooltip("This is an int containing the current armor")]
  [ReadOnly]
  public int currARM;
  public static Player instance;
  [Tooltip("This is the reference to the current player gameObject in the scene")]
  [ReadOnly]
  public GameObject player;
  [Tooltip("This is the reference to the death canvas in the scene")]
  [ReadOnly]
  public Canvas deathCanvas;
  [Tooltip("This is the refernce to the health, armor, and ammo canvas on the player")]
  [ReadOnly]
  public Canvas healthCanvas;
  [Tooltip("This is a bool that shows if the player has the keycard")]
  [ReadOnly]
  public bool hasKeycard;

  private void Awake()
  {
    //Singleton code
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(this.gameObject);
    }

    DontDestroyOnLoad(this.gameObject);

    //Setup references on start
    SetupReferences();

    //Setup current hp and armor
    currHP = startingHP;
    currARM = startingARM;
  }

  private void Update()
  {
    //Set canvas references if they turn null
    if (deathCanvas == null || healthCanvas == null || player == null)
    {
      SetupReferences();
    }
  }

  public void SetupReferences()
  {
    //Setup player reference
    player = GameObject.FindGameObjectWithTag("Player");

    //Setup death and health canvas reference
    var canvases = FindObjectsOfType(typeof(Canvas)) as Canvas[];
    foreach (Canvas currCanvas in canvases)
    {
      if (currCanvas.tag == "DeathCanvas")
      {
        deathCanvas = currCanvas;
      }
      if (currCanvas.tag == "HealthCanvas")
      {
        healthCanvas = currCanvas;
      }
    }

    //Turn off the deathCanvas until dead
    deathCanvas.enabled = false;
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

  //This would be much neater with a delegate, but I need to be fast with all these additions
  void Die()
  {
    Debug.Log("Player Dead");

    //Set the canvases to the proper state for death
    deathCanvas.enabled = true;
    healthCanvas.enabled = false;

    //Change back the cursor lock state for menuing
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    //Disable player movement
    player.GetComponent<PlayerMovement>().enabled = false;

    //Reset health on player in case they restart
    currHP = startingHP;
    currARM = startingARM;
  }
}
