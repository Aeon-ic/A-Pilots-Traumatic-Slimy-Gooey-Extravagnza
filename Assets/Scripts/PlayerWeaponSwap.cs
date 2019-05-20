using UnityEngine;

public class PlayerWeaponSwap : MonoBehaviour
{
  public Transform pistol;
  public Transform shotgun;

  private void Start()
  {
    shotgun.gameObject.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.mouseScrollDelta.y != 0)
    {
      if (pistol.gameObject.activeInHierarchy == true)
      {
        pistol.gameObject.SetActive(false);
        shotgun.gameObject.SetActive(true);
      }
      else if (pistol.gameObject.activeInHierarchy == false)
      {
        pistol.gameObject.SetActive(true);
        shotgun.gameObject.SetActive(false);
      }
    }
  }
}