using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float force;
    public float ex_radius;

    bool isBomb= false;

	void Start ()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.right * force);

        CameraControl.Cam_Con.Set_Target(transform);
        CameraControl.Cam_Con.Set_State("Targeting");
        Destroy(gameObject, 3.0f);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (!isBomb)
        {
            isBomb = true;
            Debug.Log("충돌");

            Collider2D[] Dirts = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), ex_radius);

            foreach(Collider2D dirt in Dirts)
            {
                if(dirt.gameObject.tag == "Dirt")
                {
                    Debug.Log("꽈광");

                    Destroy(gameObject);
                    Destroy(dirt.gameObject);
                }
            }
        }
    }
}
