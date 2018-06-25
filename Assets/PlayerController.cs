using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerController : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpwan;

    private void Update()
    {
        if (!isLocalPlayer)//Check if its the local player if not stop with the update function
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }

    }

    [Command]
    void CmdFire()
    {
        //Create a bullet from the bullet prefab
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpwan.position, bulletSpwan.rotation);

        //Add velocity
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        NetworkServer.Spawn(bullet);

        //Destroy the bullet after 2 seconds
        Destroy(bullet, 2);
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
