using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Picking : MonoBehaviour {

    public GameObject _Player;
    public Animator _Anim;
    public bool _isMove = false;
    

    public GameObject _hitObject;

    private NavMeshAgent _Agent;
    private RaycastHit _hit;
    private Vector3 hitPoint;


    private int HashWalk = Animator.StringToHash("Walk");



    public bool _isClick = false;


    private void Awake()
    {
        //플레이어의 네비메쉬를 찾는다.
        _Agent = _Player.GetComponent<NavMeshAgent>();
        
        //네비메쉬의 자동방향전환을 꺼준다.
       // _Agent.updateRotation = false;

    }



    private void Update()
    {
        if (_isMove && !_isClick)
        {
            _isClick = false;
           // Quaternion rot = Quaternion.LookRotation((_hit.point - _Player.transform.position).normalized);

           // _Player.transform.rotation = Quaternion.Slerp(_Player.transform.rotation, rot, Time.deltaTime * 15.0f);
        }
       
        //클릭한 위치와 자신의 거리를 비교 해서 거의 다 도착햇다거나 정지해야되는 거리가 남은거리보다 크다면 정지해라.
        if (Vector3.Distance(_Player.transform.position, hitPoint) <= 0.8f || _Agent.remainingDistance <= _Agent.stoppingDistance)
        {
            _isMove = false;
        }

        //정지햇다면 
        if (!_isMove)
        {
            //걷는 애니메이션을 꺼주고 Idle로 변경
            _Anim.SetBool(HashWalk, _isMove);
            _Agent.destination = _Player.transform.position;
        }

        //왼쪽 마우스 클릭 하는데!           ui를 클릭할떄는 마우스클릭을 막는다.
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {

            //마우스위치로 레이를 쏜다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //레이가 맞은 _hit를 검출한다.
            if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
            {
                //검출된 오브젝트를 저장한다.
                _hitObject = _hit.collider.gameObject;
               // Debug.Log(_hitObject.name);


                //클릭한 위치를 저장한다.
                hitPoint = _hit.point;


                if (_hitObject.name == "Player")
                {
                    _isClick = true;
                }

                if (_isClick && _hitObject.gameObject.tag == "Floor")
                {
                    _isClick = false;
                }


                //클릭한 위치의 태그가 바닥이라면
                if (_hitObject.gameObject.tag == "Floor")
                {
                    if (_isClick) return;

                    //이동해라
                    _isMove = true;

                    //정지해야할 거리를 0으로 초기화
                    _Agent.stoppingDistance = 0;
                    //클릭한 위치로 이동한다.
                    _Agent.destination = hitPoint;

                    //이동할 남은 거리가 있다면 걷는 애니메이션 실행
                    if (_Agent.remainingDistance >= _Agent.stoppingDistance)
                        _Anim.SetBool(HashWalk, _isMove);

                }
                /*
                else if (_hitObject.gameObject.tag == "Enemy")
                {
                    if (_isClick) return;

                    //이동해라
                    _isMove = true;

                    //정지해야할 거리를 0으로 초기화
                    _Agent.stoppingDistance = 3;
                    
                    //클릭한 위치로 이동한다.
                    _Agent.destination = hitPoint;

                    //이동할 남은 거리가 있다면 걷는 애니메이션 실행
                    if (_Agent.remainingDistance >= _Agent.stoppingDistance)
                        _Anim.SetBool(HashWalk, _isMove);
                }
                */
                else
                {
                    return;
                }

            }

        }

    }


}
