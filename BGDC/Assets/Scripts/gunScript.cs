using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    Camera cam;

    [Header ("Weapon Status")]
    public float damage;
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    int magazineSize;
    int curAmmo;
    [Range (0f, 1f)]
    public float spread;
    [Range(0f, 2f)]
    public float recoil;
    public float bulletPerSecond;
    public float moveSpeed;
    [SerializeField]
    bool isRaycast;
    public float bulletSpeed;

    float nextShoot;
    public GameObject bulletPrefab;
    public Transform bulletShooter;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        curAmmo = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButton("Fire1") && curAmmo > 0)
            {
                if (isRaycast)
                {
                    ShootRaycast();
                }
                else
                {
                    ShootProjectile();
                }
                nextAttackTime = Time.time + 1f / bulletPerSecond;
                curAmmo--;
            }
        }
        
        if (curAmmo == 0)
        {
            //reload animation
            if (maxAmmo >= 30)
            {
                maxAmmo -= magazineSize;
                curAmmo = magazineSize;
            }
            else
            {
                curAmmo += maxAmmo;
                maxAmmo -= maxAmmo;     
            }
           
        }
    }

    void ShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log("you hit " + hit.transform.name);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                //deal damage to enemy       
            }
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000);
        }

        float spreadX = Random.Range(0f, spread);
        float spreadY = Random.Range(0f, spread);
        Vector3 targetPointafterSpread = new Vector3(targetPoint.x + spreadX, targetPoint.y + spreadY, targetPoint.z);
        GameObject bullet = Instantiate(bulletPrefab, bulletShooter.position, bulletShooter.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (targetPointafterSpread - bulletShooter.transform.position).normalized * bulletSpeed;
    }
}
