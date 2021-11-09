using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 5;

    [SerializeField]
    float _attackRange = 1;
    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        }        
    }    
    protected override void UpdateMoving() 
    {        
        // 플레이어가 몬스터 사정거리보다 가까우면 공격
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;

                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;

            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);            

            //transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }

        // 애니메이션        
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다
        anim.SetFloat("speed", _stat.MoveSpeed);

    }
    protected override void UpdateIdle() 
    {       
        GameObject player = Managers.Game.GetPlayer();
        if (player == null) return;        

        float distance = (player.transform.position - transform.position).magnitude;
        if (distance <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;

            return;
        }
    }
    protected override void UpdateSkill() 
    {       
        if (_lockTarget != null)
        { 
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }
    void OnHitEvent()
    {        
        if (_lockTarget != null)
        {            
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);            

            if (targetStat.HP > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                {
                    State = Define.State.Skill;
                }
                else
                {
                    State = Define.State.Moving;
                }
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}

