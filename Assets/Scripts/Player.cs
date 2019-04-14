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
  Player instance;

  private void Start()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(this);
    }
  }
}
