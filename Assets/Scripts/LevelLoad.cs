using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
  [Tooltip("This is a string that contains the level that will be loaded")]
  public string levelToLoad;

  //Method for in-game triggering
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      SceneManager.LoadScene(levelToLoad);
    }
  }

  //Method for UI to trigger
  public void _LoadLevel(string level)
  {
    SceneManager.LoadScene(level);
  }
}
