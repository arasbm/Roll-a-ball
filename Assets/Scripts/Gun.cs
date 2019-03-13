using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
        // Gun settings
    public Rigidbody shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    private Rigidbody shotBody;
    public float shotSpeed;
    public float gunRotateSpeed;

    void Start ()
    {
        offset = transform.position - player.transform.position;
    }

    void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shotBody = Instantiate(shot, transform.position, transform.rotation) as Rigidbody;
            shotBody.velocity = transform.TransformDirection(0, 0, shotSpeed);
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * gunRotateSpeed);
    }
    
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset * player.transform.localScale.x;
    }
}