using UnityEngine;
using System.Collections;

public class Weapon1 : MonoBehaviour
{
    public int magazineSize = 12; // The number of bullets that can be fired before reloading
    public float reloadSpeed = 2.0f; // The time it takes to reload the weapon
    public float fireRate = 0.34f; // The time between shots fired
    public float reducedspeed = 0.95f; //The movementspeed is therefore reduced to 0,95%
    public float reloadwalkspeed = 0.65f; //Walk speed when reloading. 
    private int currentAmmo; // The number of bullets currently in the magazine
    private bool isReloading; // Flag to indicate if the weapon is currently reloading
    private float nextFireTime; // The next time the weapon can be fired
    public GameObject Bullet1;
    void Start()
    {
        currentAmmo = magazineSize; // Set the initial ammo count to the magazine size
    }

    void Update()
    {
        // If the weapon is reloading, exit early
        if (isReloading)
        {
            return;
        }

        // If the fire button is pressed and the weapon can fire again, shoot a bullet
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
        }

        // If the reload button is pressed and the weapon isn't already reloading, start the reload process
        if (Input.GetButtonDown("Reload") && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        // Subtract one from the current ammo count
        currentAmmo--;

        // Set the next fire time based on the fire rate
        nextFireTime = Time.time + fireRate;

        // Fire a bullet
        GameObject temporary = Instantiate(Bullet1, transform.position, Quaternion.identity);
        temporary.GetComponent<Rigidbody>().AddForce(transform.forward*2f, ForceMode.Impulse);
    }

    public IEnumerator Reload()
    {
        // Set the reloading flag to true
        isReloading = true;

        // Wait for the reload speed before adding ammo back to the magazine
        yield return new WaitForSeconds(reloadSpeed);

        // Add ammo back to the magazine up to the magazine size
        currentAmmo = Mathf.Min(currentAmmo + (magazineSize - currentAmmo), magazineSize);

        // Reset the reloading flag
        isReloading = false;
    }
}