using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{  
    protected override void Init()
    {
        base.Init();
        #region 오브젝트 풀링 테스트 코드
        //for (int i = 0; i < 10; i++)
        //{
        //    Managers.Resource.Instantiate("Player");
        //}
        #endregion
        SceneType = Define.Scene.Game;
        //Managers.UI.ShowSceneUI<UI_Inven>();        
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Player, "Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }
    
    public override void Clear()
    {

    }  
}
