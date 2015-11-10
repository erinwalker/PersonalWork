using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    //Set variables
    public Rigidbody bullet;
    public GameObject spawnPoint;
    protected Vector3 position;
    public int health;

    public void Shoot()
    {
        //Makes a bullet and gives it a force to shoot
        Rigidbody bulletSpawned = Instantiate(bullet, position, Quaternion.identity) as Rigidbody;
        bulletSpawned.AddForce(transform.forward * 2000);
    }
}
