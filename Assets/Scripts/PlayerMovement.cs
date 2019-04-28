using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [Tooltip("This is a float that modifies the player's walk speed")]
  public float walkSpeed = 6f;
  [Tooltip("This is a float that modifies the player's sprint speed")]
  public float sprintSpeed = 10f;
  public float rotationSpeed = 10f;
  Vector3 totalForce = Vector3.zero;
  public KeyCode forwardsKey = KeyCode.W;
  public KeyCode backwardsKey = KeyCode.S;
  public KeyCode strafeLeftKey = KeyCode.A;
  public KeyCode strafeRightKey = KeyCode.D;
  public KeyCode sprintKey = KeyCode.LeftShift;


  // Start is called before the first frame update
  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  // Update is called once per frame
  void Update()
  {
    totalForce = Vector3.zero;

    //Compile movement vector
    if (Input.GetKey(strafeLeftKey))
    {
      totalForce += transform.right * -1;
    }
    if (Input.GetKey(strafeRightKey))
    {
      totalForce += transform.right;
    }
    if (Input.GetKey(forwardsKey))
    {
      totalForce += transform.forward;
    }
    if (Input.GetKey(backwardsKey))
    {
      totalForce += transform.forward * -1;
    }

    //Move based on sprint or not
    if (Input.GetKey(sprintKey))
    {
      this.gameObject.transform.position += totalForce * sprintSpeed * Time.deltaTime;
    }
    else
    {
      this.gameObject.transform.position += totalForce * walkSpeed * Time.deltaTime;
    }

    //Mouse aiming
    //Get current euler angles
    Vector3 euler = this.gameObject.transform.localEulerAngles;

    //Modify saved euler angles by mouse input
    euler.y += Input.GetAxisRaw("Mouse X");

    //Set rotation to euler + mouse input
    this.gameObject.transform.localEulerAngles = euler;

    //Set Y rotation based on camera
    Vector3 cameraEuler = Camera.main.transform.eulerAngles;
    cameraEuler.x += -Input.GetAxisRaw("Mouse Y");
    if (cameraEuler.x > 180)
    {
      cameraEuler.x -= 360;
    }
    if (cameraEuler.x < -180)
    {
      cameraEuler.x += 360;
    }
    Camera.main.transform.eulerAngles = cameraEuler;
  }
}
