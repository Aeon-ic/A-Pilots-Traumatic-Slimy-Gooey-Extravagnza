using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MainMenu : MonoBehaviour
{
  public Canvas creditCanvas;
  public Canvas mainCanvas;

  private void Awake()
  {
    creditCanvas.enabled = false;
  }

  public void _LoadLevel(string level)
  {
    SceneManager.LoadScene(level);
  }

  public void _StartCredits()
  {
    creditCanvas.enabled = true;
    mainCanvas.enabled = false;
  }

  public void _StopCredits()
  {
    creditCanvas.enabled = false;
    mainCanvas.enabled = true;
  }

  public void _ExitGame()
  {
    Application.Quit();
  }
}
