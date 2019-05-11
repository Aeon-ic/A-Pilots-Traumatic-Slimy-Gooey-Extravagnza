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
      //Turn on the victory canvas
      victoryCanvas.enabled = true;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;

      //Turn off player movement
      Player.instance.player.gameObject.GetComponent<PlayerMovement>().enabled = false;
    }
  }
}
