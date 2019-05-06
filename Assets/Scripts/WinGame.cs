using UnityEngine;

public class WinGame : MonoBehaviour
{
  public Canvas victoryCanvas;

  private void Awake()
  {
    victoryCanvas.enabled = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      if (Player.instance.hasKeycard == true)
      {
        victoryCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
    }
  }
}
