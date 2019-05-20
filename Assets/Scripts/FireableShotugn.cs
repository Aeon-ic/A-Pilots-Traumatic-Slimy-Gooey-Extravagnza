using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class FireableShotugn : MonoBehaviour, IFireable
{
  [Tooltip("This is a float that defines the amount of damage the pellet does on hit")]
  public float damage = 10;
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
  [Tooltip("This is an audio clip for the shotgun firing sound effect")]
  public AudioClip shotSound;
  AudioSource shotgunSource;
  [Tooltip("This is the text that is updated when the gun is shot")]
  public Text shotgunUIText;

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

    //Get the audio source on the current object
    shotgunSource = this.gameObject.GetComponent<AudioSource>();

    //Subscribe UpdateShotgunText to HealthCanvas OnUIUpdate
    GameObject.Find("HealthCanvas").GetComponent<HealthCanvas>().OnUIUpdate += UpdateShotgunText;
  }

  public void Shoot()
  {
    //Check if gun has ammo to shoot
    if (ammoLoaded > 0)
    {
      //If it does, subtract the bullet shot, and Raycast for the bullet travel
      ammoLoaded -= 1;
      Vector3[] pelletList = new Vector3[pelletsPerBullet];

      //Play Sound effect
      if (shotgunSource.isPlaying)
      {
        shotgunSource.Stop();
      }
      shotgunSource.PlayOneShot(shotSound);

      //Loop for each pellet in each shot
      for (int i = 0; i < pelletsPerBullet; i++)
      {
        //Calculate single pellet spread
        pelletList[i] = -gameObject.transform.forward + (Random.insideUnitSphere * bulletSpread);

        //"Shoot" the bullet
        if (Physics.Raycast(gameObject.transform.position, pelletList[i], out hit, bulletDistance))
        {
          Debug.Log("Pellet hit: " + hit.collider);

          //Check what the bullet hit
          if (hit.collider.CompareTag("Enemy"))
          {
            //Call TakeDamage on the enemy
            hit.collider.gameObject.GetComponent<IEnemyHealth>().TakeDamage(damage);
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
      //If it didn't, draw a line out into forward space at BulletDistance
      bulletRender.SetPosition(0, gameObject.transform.position);
      bulletRender.SetPosition(1, this.gameObject.transform.position + pelletTraj * bulletDistance);
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

  public void UpdateShotgunText()
  {
    //Change UI
    shotgunUIText.text = $"{ammoLoaded}/{AmmoManager.instance.AmmoLeft(ammoType)}";
  }
}
