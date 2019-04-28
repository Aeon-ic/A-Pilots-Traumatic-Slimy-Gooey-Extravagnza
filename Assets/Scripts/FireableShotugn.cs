using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class FireableShotugn : MonoBehaviour, IFireable
{
  [Tooltip("This is a float that defines the amount of damage the pellet does on hit")]
  public float damage = 10f;
  [Tooltip("This is an int that defines how many bullets are currently in the magazine")]
  public int ammoLoaded = 2;
  [Tooltip("This is an int that defines how many bullets the gun can hold in the magazine")]
  public int clipSize = 2;
  [Tooltip("This is an int that defines how many pellets are shot per click")]
  public int pelletsPerBullet = 8;
  [Tooltip("This is a float that defines how far the bullet can travel")]
  public float bulletDistance = 75f;
  [Tooltip("This is a float that defines how long the bullet tracer shows")]
  public float drawBulletTime = .05f;
  [Tooltip("This is a float that defines how far the bullets spread")]
  public float bulletSpread = .05f;
  [Tooltip("This is a string for ammo type that must match a type in the AmmoManager")]
  public string ammoType = "12Gauge";
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
      Vector3[] pelletList = new Vector3[pelletsPerBullet];
      //Check if it hit
      for (int i = 0; i < pelletsPerBullet; i++)
      {
        pelletList[i] = -gameObject.transform.forward;
        Debug.Log(gameObject.transform.position);
        Debug.Log("Pellet: " + i +"\tPellet direction" + pelletList[i]);
        if (Physics.Raycast(gameObject.transform.position, -gameObject.transform.forward, out hit, bulletDistance))
        {
          Debug.Log("Pellet hit: " + hit.collider);

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
        //Display the bullet tracer
        StartCoroutine(DrawBullet(pelletList[i]));
      }

    }
  }

  IEnumerator DrawBullet(Vector3 pelletTraj)
  {
    //Check if the bullet collided with anything
    if (hit.collider == null)
    {
      //If it didn't, draw a line out into forward space (relative up from the gun) at BulletDistance
      bulletRender.SetPosition(0, gameObject.transform.position);
      bulletRender.SetPosition(1, transform.TransformPoint(pelletTraj));
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
