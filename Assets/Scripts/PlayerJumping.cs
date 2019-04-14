using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
  [Tooltip("This is a float that modifies how high the player jumps")]
  public float jumpForce = 200f;
  public KeyCode jumpKey = KeyCode.Space;
  [Tooltip("This is a float that defines the distance to the ground for when a player is grounded")]
  public float groundedDistance = 1.1f;
  bool isGrounded = false;
  Rigidbody playerRigidbody;

  private void Awake()
  {
    playerRigidbody = this.gameObject.GetComponent<Rigidbody>();
  }

  private void Update()
  {
    //Check for ground
    //Debug.DrawRay(this.gameObject.transform.position, Vector3.down, Color.red, groundedDistance);
    if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, groundedDistance))
    {
      isGrounded = true;
    }
    else
    {
      isGrounded = false;
    }

    //Jump
    if (Input.GetKeyDown(jumpKey) && isGrounded)
    {
      playerRigidbody.AddForce(Vector3.up * jumpForce);
    }
  }
}
