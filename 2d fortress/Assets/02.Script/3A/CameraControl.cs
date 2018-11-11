using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    private static CameraControl cam_con;

    public static CameraControl Cam_Con
    {
        get
        {
            if (cam_con == null)
            {
                cam_con = FindObjectOfType(typeof(CameraControl)) as CameraControl;

                if (cam_con == null)
                {
                    Debug.LogError("카메라 컨트롤 스크립트가 없스무니당.");
                }
            }

            return cam_con;

        }
    }

    enum CamState {Normal, Targeting};
    CamState State = CamState.Normal;

    public Transform pos1, pos2;
    Transform TargetObj;
    Camera this_cam;

    public void Set_State(string State_string)
    {
        switch(State_string)
        {
            case "Normal":
                StartCoroutine("Normal");
                break;
            case "Targeting":
                StartCoroutine("Targeting");
                break;

            default:
                Debug.LogError("CameraControl : '" + State_string + "' 라는 값은 존재하지 않습니다.");
                break;
        }
    }
    public void Set_Target(Transform obj)
    {
        TargetObj = obj;
    }

    IEnumerator Normal()
    {
        //위치들의 사이를 구해서 그위치에 카메라를 놓아 준 후에
        //카메라의 size를 조절. 현재는 가로의 길이만 조절하게 되어있음.

        State = CamState.Normal;
        Debug.Log("카메라 : 노말 모드");

        while (State == CamState.Normal)
        {
            float x_gap = (pos1.position.x - pos2.position.x);
            if (x_gap < 0) x_gap = -x_gap;

            Vector3 Middle_pos = Vector3.Lerp(pos1.position,pos2.position,0.5f);
            Vector3 TargetPos  = new Vector3(Middle_pos.x, Middle_pos.y, -10.0f);
            transform.position = Vector3.Lerp(transform.position, TargetPos, 0.1f);

            float cam_size = Mathf.Clamp((x_gap / 3.0f) + 0.2f,1.0f, 10.0f);
            this_cam.orthographicSize = cam_size;

            yield return null;
        }
    }

    IEnumerator Targeting()
    {
        //한가지 오브젝트를 비출때에 쓰여짐. 조금 확대되고 위치가 고정됨
        //포탄이 날아갈때, 결과 때에 쓰여질 것으로 보임.


        State = CamState.Targeting;
        Debug.Log("카메라 : 타게팅 모드");

        while(State == CamState.Targeting)
        {
            if (TargetObj == null)
            {
                yield return new WaitForSeconds(1.0f);
                Set_State("Normal");
            }

            transform.position = new Vector3(TargetObj.transform.position.x, TargetObj.transform.position.y,-10.0f);

            if (this_cam.orthographicSize != 1.0f) this_cam.orthographicSize = 1.0f;
            yield return null;
        }
    }

    private void Awake()
    {
        this_cam = GetComponent<Camera>();
        Set_State("Normal");
    }

}
