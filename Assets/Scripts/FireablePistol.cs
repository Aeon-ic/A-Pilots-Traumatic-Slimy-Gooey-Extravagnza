using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FireablePistol : MonoBehaviour, IFireable
{
  [Tooltip("This is a float that defines the amount of damage the gun does on hit")]
  public float damage = 12.5f;
  [Tooltip("This is an int that defines how many bullets are currently in the magazine")]
  public int ammoLoaded = 10;
  [Tooltip("This is an int that defines how many bullets the gun can hold in the magazine")]
  public int clipSize = 10;
  [Tooltip("This is a float that defines how far the bullet can travel")]
  public float bulletDistance = 75f;
  [Tooltip("This is a float that defines how long the bullet tracer shows")]
  public float drawBulletTime = .05f;
  [Tooltip("This is a string for ammo type that must match a type in the AmmoManager")]
  public string ammoType = "9mm";
  RaycastHit hit;
  LineRenderer bulletRender;

  private void Awake()
  {
    //Grab the LineRenderer component for the bullet tracers
    bulletRender = gameObject.GetComponent<LineRenderer>();
  }

  public void Shoot()
  {
    //Check if gun has ammo to shoot
    if (ammoLoaded > 0)
    {
      //If it does, subtract the bullet shot, and Raycast for the bullet travel
      ammoLoaded -= 1;
      //Check if it hit
      Debug.Log(gameObject.transform.up);
      if (Physics.Raycast(gameObject.transform.position, gameObject.transform.up, out hit, bulletDistance))
      {
        //Display the bullet tracer
        StartCoroutine(DrawBullet());
        Debug.Log("Bullet hit: " + hit.collider);

        //Check what the bullet hit
        if (hit.collider.CompareTag("Enemy"))
        {
          //Add in damage later
        }
        else
        {
          //Add in particle effects on hit point later
        }
      }
      else
      {
        //Draw bullet on miss
        StartCoroutine(DrawBullet());
      }
    }
  }

  IEnumerator DrawBullet()
  {
    //Check if the bullet collided with anything
    if (hit.collider == null)
    {
      //If it didn't, draw a line out into forward space (relative up from the gun) at BulletDistance
      bulletRender.SetPosition(0, gameObject.transform.position);
      bulletRender.SetPosition(1, transform.TransformPoint(Vector3.up * bulletDistance));
    }
    else
    {
      //If it did, draw a line towards the hit point
      bulletRender.SetPosition(0, gameObject.transform.position);
      bulletRender.SetPosition(1, hit.point);
    }
    //Display the bullet tracer for DrawBulletTime
    bulletRender.enabled = true;
    yield return new WaitForSeconds(drawBulletTime);
    bulletRender.enabled = false;
  }

  public void Reload()
  {
    ammoLoaded += AmmoManager.instance.FillClip(ammoType, clipSize - ammoLoaded);
  }
}
