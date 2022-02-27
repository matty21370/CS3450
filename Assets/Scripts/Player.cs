using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7;
    
    private PhotonView _photonView;
    private Rigidbody _rigidbody;

    private Vector3 _movement;
    
    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_photonView.IsMine) return;

        _movement.x = Input.GetAxis("Horizontal");
        _movement.z = Input.GetAxis("Vertical");
        
        _rigidbody.MovePosition(transform.position + _movement * Time.deltaTime * movementSpeed);
    }
}
