using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float detectionRadius;
    
    private Transform _centreTransform;
    
    private PhotonView _photonView;
    private Movement _movement;
    
    private Player[] _players;

    private readonly float _aiUpdateTime = 0.2f;
    private readonly float _playerUpdateTime = 1f;
    private float _updateTimer;
    private float _playerUpdateTimer;

    private bool _hasTarget = false;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _players = FindObjectsOfType<Player>();
        _centreTransform = GameObject.Find("Centre").transform;
        _updateTimer = _aiUpdateTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _playerUpdateTimer)
        {
            _playerUpdateTimer = Time.time + _playerUpdateTime;
            UpdatePlayers();
        }
        
        if(!_photonView.IsMine) return;
        
        if (Time.time >= _updateTimer)
        {
            _updateTimer = Time.time + _aiUpdateTime;
            HandleAI();
        }
    }

    private void HandleAI()
    {
        foreach (var player in _players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
            {
                if (PhotonNetwork.IsConnected)
                {
                    _photonView.RPC("SetTarget", RpcTarget.All, player.transform.position);
                }
                else
                {
                    SetTarget(player.transform.position);
                }
            }
        }

        if (_hasTarget)
        {
            _movement.SetDestination(_targetPosition);
        }
        else
        {
            _movement.SetDestination(_centreTransform.position);
        }
    }

    [PunRPC]
    public void SetTarget(Vector3 target)
    {
        _hasTarget = true;
        _targetPosition = target;
    }

    public void UpdatePlayers()
    {
        _players = FindObjectsOfType<Player>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
