using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Player playerScript;
    float speed = 300;

    Vector3 velocity = new Vector3(300.0f, 300.0f, 0.0f);

	void Start ()
    {
        playerScript = FindObjectOfType<Player>();
        GetComponent<Rigidbody2D>().AddRelativeForce(transform.right * speed);
        //GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().velocity = new Vector3(5, 5, 0);
        //GetComponent<Rigidbody2D>().AddForce(velocity);
        //GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    void Update ()
    {
		
	}
}
