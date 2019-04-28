using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
  [Tooltip("This is an array of string for all the different types of ammo (Must be the same length as startingAmmo)")]
  public string[] ammoTypes;
  [Tooltip("This is an array of ints for all the different values for the starting ammo (Must be the same length as ammoTypes)")]
  public int[] startingAmmo;
  [Tooltip("This is an array that limits how many bullets of each type a player can hold (Must be the same length as ammoTypes)")]
  public int[] maxAmmo;
  [Tooltip("This is an array containing how many bullets are left of each type")]
  public int[] currentAmmo;
  public static AmmoManager instance;

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

  // Start is called before the first frame update
  void Start()
  {
    currentAmmo = new int[ammoTypes.Length];
    for (int i = 0; i < startingAmmo.Length; i++)
    {
      currentAmmo[i] = startingAmmo[i];
    }
  }

  public int FillClip(string gunType, int ammoRequested)
  {
    //Check if the current ammo list has enough ammo to fill request
    int ammoIndex = Array.IndexOf(ammoTypes, gunType);
    if (currentAmmo[ammoIndex] >= ammoRequested)
    {
      //Return the ammo requested
      currentAmmo[ammoIndex] -= ammoRequested;
      return ammoRequested;
    }
    else
    {
      int ammoToReturn = currentAmmo[ammoIndex];
      currentAmmo[ammoIndex] -= currentAmmo[ammoIndex];
      return ammoToReturn;
    }
  }

  public void AddAmmo(string ammoType, int ammoAdded)
  {
    int ammoIndex = Array.IndexOf(ammoTypes, ammoType);
    if (currentAmmo[ammoIndex] + ammoAdded > maxAmmo[ammoIndex])
    {
      currentAmmo[ammoIndex] = maxAmmo[ammoIndex];
    }
    else
    {
      currentAmmo[ammoIndex] += ammoAdded;
    }
  }
}
