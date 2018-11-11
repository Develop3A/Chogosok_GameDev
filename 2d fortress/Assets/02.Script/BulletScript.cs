using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float force = 300;

	void Start ()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.right * force);
    }
}
