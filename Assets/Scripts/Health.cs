using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float _currentHealth;
    
    private PhotonView _photonView;

    public PhotonView View => _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [PunRPC]
    public void TakeDamage(float amt)
    {
        _currentHealth = Mathf.Max(_currentHealth - amt, 0);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
