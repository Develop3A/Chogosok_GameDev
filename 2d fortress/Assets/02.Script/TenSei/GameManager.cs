using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log(typeof(GameManager).ToString() + " 생성 및 싱글톤 화");
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if(_instance == null)
                {
                    Debug.LogError("There's no active GameManager object");
                }
            }
            return _instance;
        }
    }
    #endregion

    Player player;

    void Start ()
    {
        DontDestroyOnLoad(this);
        player = FindObjectOfType<Player>();
	}

    public void moveTank()
    {
        Debug.Log("게임매니저에서 Player함수 불러오기 버튼");
        player.Player_Move(1);
    }



    


}
