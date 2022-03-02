using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] private float maxHealth;
    private float _currentHealth;
    
    private PhotonView _photonView;

    public PhotonView View => _photonView;

    private Player _killer = null;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amt, Player dealer)
    {
        _currentHealth = Mathf.Max(_currentHealth - amt, 0);

        if (_currentHealth <= 0)
        {
            _killer = dealer;
            photonView.RPC("Kill", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Kill()
    {
        _killer.AddScore(10);
        Destroy(gameObject);
    }
}
