using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
  [Tooltip("This is the image that is changed when health is decreased or increased")]
  public Image healthBar;
  [Tooltip("This is the image that is changed when armor is decreased or increased")]
  public Image armorBar;
  [Tooltip("This is the image that is enabled when the key has been picked up")]
  public Image keyAcquired;
  public event Action OnUIUpdate = delegate { };

  // Update is called once per frame
  void Update()
  {
    healthBar.fillAmount = Player.instance.currHP / (float)Player.instance.maxHP;
    armorBar.fillAmount = Player.instance.currARM / (float)Player.instance.maxARM;

    OnUIUpdate();

    if (Player.instance.hasKeycard)
    {
      keyAcquired.enabled = true;
    }
    else
    {
      keyAcquired.enabled = false;
    }
  }
}
