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
    bool isShoot = false;


    void Start ()
    {
        shot = false;
        anim = transform.GetComponent<Animator>();
        StartCoroutine(Move(1,0));
    }

    private void Update()
    {
        //Debug.Log("isShoot" + isShoot);
        //Debug.Log("shot" + shot);
        if (Input.GetMouseButtonDown(0) && isShoot == false)
        {
            shot = true;
        }
        else
        {
            isShoot = false;
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
                Fuel = 1000;
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
                
                Debug.Log("1번 상태");
                while (isShoot == false)
                {
                    float angle = posin.transform.eulerAngles.z+Input.GetAxis("Vertical");                   
                    Vector3 Rot = new Vector3(0, 0, Mathf.Clamp(angle, 0, 80));
                    posin.transform.eulerAngles = Rot;
                   
                    if (shot)
                    {
                        GameObject Bullet = Instantiate(bullet, firePos.position, Quaternion.Euler(Rot));
                        isShoot = true;
                        shot = false;
                    }
                    yield return null;
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

            }
        }
        

    }

    //[Header(" - 포물선 운동 정보 - ")]
    //[SerializeField] private float fMaxHeight;
    //[SerializeField] private float fMaxTime;
    //public GameObject Bullet;
    //public Transform Boob;
    //public Vector3 m_tagetPos;
    //public Transform FirePos;
    //public Transform TargetPos;


    //Transform m_objTr;

    //Vector3 m_startPos; // 출발지
    //Vector3 m_targetPos; // 도착지
    //Vector3 m_currentPos; // 현재 위치

    //float m_fSpeedX; // X축 스피드
    //float m_fSpeedY; // Y축 스피드
    //float m_fSpeedZ; // Z축 스피드

    //float m_fg; // Y축으로의 중력가속도
    //float m_fEndTime; // 도착 지점 도달 시간
    //float m_fMaxHeight; // 최대 높이
    //float m_fHeight; // 최대 높이 - 시작 높이
    //float m_fTargetHeight; // 도착지점 높이 - 시작지점 높이 
    //float m_fTime = 0f; // 흐르는 시간
    //float m_fMaxTime; // 최대 높이까지 가는 시간

    //IEnumerator Shot(Transform _objTr, Vector3 _startPos, Vector3 _targetPos, float _fMaxHeight, float _fMaxTime)
    //{
        
    //       m_objTr = _objTr;
       
    //       m_startPos = _startPos;
    //       m_targetPos = _targetPos;
    //       m_fMaxHeight = _fMaxHeight;
    //       m_fMaxTime = _fMaxTime;
       
    //       m_fTargetHeight = m_targetPos.y - m_startPos.y;
    //       m_fHeight = m_fMaxHeight - m_startPos.y;
    //       m_fg = 2 * m_fHeight / (m_fMaxTime * m_fMaxTime);
    //       m_fSpeedY = Mathf.Sqrt(2 * m_fg * m_fHeight);
       
    //       float a = m_fg;
    //       float b = -2 * m_fSpeedY;
    //       float c = 2 * m_fTargetHeight;
       
    //       m_fEndTime = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
       
    //       m_fSpeedX = -(m_startPos.x - m_targetPos.x) / m_fEndTime;
    //       m_fSpeedZ = -(m_startPos.z - m_targetPos.z) / m_fEndTime;
       
    //       while (true)
    //       {
    //           m_fTime += Time.deltaTime;
       
    //           m_currentPos.x = m_startPos.x + m_fSpeedX * m_fTime;
    //           m_currentPos.y = m_startPos.y + (m_fSpeedY * m_fTime) - (0.5f * m_fg * m_fTime * m_fTime);
    //           m_currentPos.z = m_startPos.z + m_fSpeedZ * m_fTime;
       
    //           m_objTr.LookAt(m_currentPos);
       
    //           m_objTr.position = m_currentPos;
       
    //           if (m_fTime > m_fEndTime)
    //           {
    //               break;
    //           }
       
    //           yield return null;
    //       }
    //}
}

