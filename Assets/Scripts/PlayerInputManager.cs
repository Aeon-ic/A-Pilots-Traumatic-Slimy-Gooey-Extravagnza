using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
  public KeyCode shootKey = KeyCode.Mouse0;
  public KeyCode reloadKey = KeyCode.R;
  public IFireable currentWeapon;

  private void Update()
  {
    //Check if weapon has changed
    currentWeapon = this.gameObject.GetComponentInChildren<IFireable>();

    if (Input.GetKeyDown(shootKey))
    {
      currentWeapon.Shoot();
    }

    if (Input.GetKeyDown(reloadKey))
    {
      currentWeapon.Reload();
    }
  }
}
