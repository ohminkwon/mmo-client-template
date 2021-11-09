using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
        #region 오브젝트 풀링 테스트 코드
        //List<GameObject> list = new List<GameObject>();
        //for (int i = 0; i < 5; i++)
        //{
        //    list.Add(Managers.Resource.Instantiate("Player"));
        //}

        //foreach (GameObject obj in list)
        //{
        //    Managers.Resource.Destroy(obj);
        //}
        #endregion
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //SceneManager.LoadScene("Game");
            Managers.Scene.LoadScene(Define.Scene.Game);            
        }
    }
    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }
}
