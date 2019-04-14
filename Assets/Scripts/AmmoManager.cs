using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
  public Dictionary<string, int> ammoManager = new Dictionary<string, int>();
  [Tooltip("This is an array of string for all the different types of ammo (Must be the same length as startingAmmo)")]
  public string[] ammoTypes;
  [Tooltip("This is an array of ints for all the different values for the starting ammo (Must be the same length as ammoTypes)")]
  public int[] startingAmmo;
  AmmoManager instance;

  
  // Start is called before the first frame update
  void Start()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(this);
    }

    for(int i = 0; i < ammoTypes.Length; i++)
    {
      Debug.Log(ammoTypes[i]);
      Debug.Log(startingAmmo[i]);
      ammoManager.Add(ammoTypes[i], startingAmmo[i]);
    }
  }

  int FillClip(string gunType, int clipSize)
  {
    if (ammoManager[gunType] >= clipSize)
    {
      ammoManager[gunType] -= clipSize;
      return clipSize;
    }
    else
    {
      ammoManager[gunType] -= ammoManager[gunType];
      return ammoManager[gunType];
    }
  }
}
