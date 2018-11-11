using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject posin;
    public GameObject bullet;
    public Transform firePos;

    Animator anim;

    bool shot = false;
    bool shot_per = false; //shot키를 누를 수 있는 떄를 설정체크


    void Start ()
    {
        anim = transform.GetComponent<Animator>();
        StartCoroutine(Move(1,0));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !shot && shot_per)
        {
            shot = true;
            shot_per = false;
        }
    }

    IEnumerator Move(int turn, int index)
    {
        int Fuel = 1000;
        int speed = 1;
        

        while (turn == 1)
        {         
            if (index == 0)
            {
                anim.SetInteger("StateType", 0);
                Fuel = 10;
                Debug.Log("0번 상태");
                while (Fuel >= 0)
                {
                    float xM = Input.GetAxis("Horizontal") * speed * 0.01f;
                    this.transform.Translate(new Vector3(xM, 0, 0));

                    yield return null;

                    if (xM > 0 || xM < 0)
                    {
                        Fuel--;
                    }
                }
                if (Fuel < 0) { index = 1; }
            }

            if (index == 1)
            {
                anim.SetInteger("StateType", 1);
                posin.SetActive(true);
                shot_per = true;
                Vector3 Angle = new Vector3(0, 0, 0);
                
                Debug.Log("1번 상태");
                while (!shot)
                {
                    float angle = posin.transform.eulerAngles.z+Input.GetAxis("Vertical");                   
                    Vector3 Rot = new Vector3(0, 0, Mathf.Clamp(angle, 0, 80));
                    posin.transform.eulerAngles = Rot;
                    Angle = Rot;
                   
                    yield return null;
                }
                if (shot)
                {
                    GameObject Bullet = Instantiate(bullet, firePos.position, Quaternion.Euler(Angle));
                    Debug.Log(Angle);
                }
                index = 2;
                posin.SetActive(false);
            }

            if (index == 2)
            {
                anim.SetInteger("StateType", 0);
                posin.transform.localRotation = Quaternion.identity;
                Debug.Log("2번 상태");
                index = 0;
                turn = 1;

                Debug.Log("0번으로 돌아가기 위한 대기시간");
                yield return new WaitForSeconds(2f);

                shot = false;
                shot_per = false;
            }
        }
        

    }
    
}

