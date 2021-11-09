using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _destPos;

    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    [SerializeField]
    protected GameObject _lockTarget;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;
    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    //anim.Play("RUN");
                    //anim.SetFloat("speed", _stat.MoveSpeed);
                    //anim.SetBool("attack", false);
                    break;
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    //anim.Play("WAIT");
                    //anim.SetFloat("speed", 0);
                    //anim.SetBool("attack", false);
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    //anim.Play("ATTACK");
                    //anim.SetBool("attack", true);
                    break;
            }
        }
    }
    private void Start()
    {
        Init();
    }    
    void Update()
    {

        switch (State)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
        #region 하드코딩을 통한 이동 및 애니메이션 기본 코드
        //if (_moveToDest)
        //{
        //    Vector3 dir = _destPos - transform.position;
        //    if (dir.magnitude < 0.0001f)
        //    {
        //        _moveToDest = false;
        //    }
        //    else
        //    {
        //        float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
        //        transform.position += dir.normalized * moveDist;
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);                
        //    }
        //}

        //if (_moveToDest)
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        //    Animator anim = GetComponent<Animator>();
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}
        //else
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        //    Animator anim = GetComponent<Animator>();
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}
        #endregion
    }

    public abstract void Init();
    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateSkill() { }

}
