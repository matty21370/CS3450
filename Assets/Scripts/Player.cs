using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7;
    
    private PhotonView _photonView;
    private Rigidbody _rigidbody;

    private Vector3 _movement;

    private CinemachineVirtualCamera _camera;
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _rigidbody = GetComponent<Rigidbody>();

        _camera = FindObjectOfType<CinemachineVirtualCamera>();
        _mainCamera = Camera.main;

        if (_photonView.IsMine)
        {
            FindObjectOfType<CameraFollow>().Init(transform);
            _camera.Follow = FindObjectOfType<CameraFollow>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!_photonView.IsMine) return;

        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        _movement.x = Input.GetAxis("Horizontal");
        _movement.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + _movement * Time.deltaTime * movementSpeed);
    }
}
