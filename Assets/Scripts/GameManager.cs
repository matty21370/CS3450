using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float enemySpawnDelay = 2f;
    private float _enemySpawnTimer;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    
    private void Start()
    {
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity);
    }

    private void Update()
    {
        if(!PhotonNetwork.IsMasterClient) return;
        
        if (Time.time >= _enemySpawnTimer)
        {
            Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            SpawnEnemy(position);
            _enemySpawnTimer = Time.time + enemySpawnDelay;
        }
    }

    [PunRPC]
    private void SpawnEnemy(Vector3 position)
    {
        PhotonNetwork.Instantiate("enemy", position, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
