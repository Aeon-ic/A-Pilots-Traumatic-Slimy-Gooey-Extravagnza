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
    for (int i = 0; i < ammoTypes.Length; i++)
    {
      ammoManager.Add(ammoTypes[i], startingAmmo[i]);
    }
  }

  public int FillClip(string gunType, int ammoRequested)
  {
    if (ammoManager[gunType] >= ammoRequested)
    {
      ammoManager[gunType] -= ammoRequested;
      Debug.Log(gunType + " bullets left: " + ammoManager[gunType]);
      return ammoRequested;
    }
    else
    {
      ammoManager[gunType] -= ammoManager[gunType];
      Debug.Log(gunType + " bullets left: " + ammoManager[gunType]);
      return ammoManager[gunType];
    }
  }
}
