using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
  public KeyCode shootKey = KeyCode.Mouse0;
  public KeyCode reloadKey = KeyCode.R;
  public IFireable currentWeapon;

  private void Update()
  {
    //Check if weapon has changed
    currentWeapon = gameObject.GetComponentInChildren<IFireable>();

    if (Input.GetKeyDown(shootKey))
    {
      if (currentWeapon != null)
      {
        currentWeapon.Shoot();
      }
    }

    if (Input.GetKeyDown(reloadKey))
    {
      if (currentWeapon != null)
      {
        currentWeapon.Reload();
      }
    }
  }
}
