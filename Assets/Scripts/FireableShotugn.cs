using System.Collections;
using UnityEngine;

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
  [Tooltip("This is the material for the bullet tracer when a gun is shot")]
  public Material bulletTracer;
  RaycastHit hit;
  LineRenderer[] bulletRender;
  [Tooltip("This is a prefab for the bullet renders for each pellet")]
  public GameObject prefab;

  private void Awake()
  {
    //Grab the LineRenderer component for the bullet tracers
    bulletRender = new LineRenderer[pelletsPerBullet];
    for (int i = 0; i < pelletsPerBullet; i++)
    {
      GameObject obj = Instantiate(prefab, gameObject.transform, false) as GameObject;
      bulletRender[i] = obj.AddComponent<LineRenderer>();
      bulletRender[i].material = bulletTracer;
      bulletRender[i].startWidth = .05f;
      bulletRender[i].endWidth = .05f;
      bulletRender[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }
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
        pelletList[i] = -gameObject.transform.forward + (Random.insideUnitSphere * bulletSpread);
        Debug.Log(gameObject.transform.position);
        Debug.Log("Pellet: " + i + "\tPellet direction" + pelletList[i]);
        if (Physics.Raycast(gameObject.transform.position, pelletList[i], out hit, bulletDistance))
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
        StartCoroutine(DrawBullet(pelletList[i], bulletRender[i]));
      }

    }
  }

  IEnumerator DrawBullet(Vector3 pelletTraj, LineRenderer bulletRender)
  {
    //Check if the bullet collided with anything
    if (hit.collider == null)
    {
      //If it didn't, draw a line out into forward space (relative up from the gun) at BulletDistance
      bulletRender.SetPosition(0, gameObject.transform.position);
      Ray ray = new Ray(gameObject.transform.parent.transform.position, pelletTraj);
      bulletRender.SetPosition(1, transform.TransformPoint(ray.direction * bulletDistance));
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
