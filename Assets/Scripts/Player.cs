using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7;
    
    private PhotonView _photonView;
    private Rigidbody _rigidbody;

    private Vector3 _movement;

    private CinemachineVirtualCamera _camera;
    private Camera _mainCamera;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem shootParticle;

    [SerializeField] private float shootCooldown;
    private float _shootTimer;

    private int _score;
    private string _playerName;

    public int Score => _score;
    public string PlayerName => _playerName;
    
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

        _playerName = _photonView.Owner.NickName;
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
        
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= _shootTimer)
            {
                _shootTimer = Time.time + shootCooldown;
                _photonView.RPC("Shoot", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void Shoot()
    {
        RaycastHit shoot;
        
        Debug.DrawRay(shootPoint.position, transform.forward, Color.magenta);

        GameObject particle = Instantiate(shootParticle, shootPoint.position, Quaternion.Euler(90, 0, 0)).gameObject;
        Destroy(particle, 2f);
        
        if (Physics.Raycast(shootPoint.position, transform.forward, out shoot))
        {
            if (shoot.transform.CompareTag("Enemy"))
            {
                Health enemy = shoot.transform.GetComponent<Health>();

                if (enemy != null)
                {
                    enemy.TakeDamage(10, this);
                }
            }
        }
    }

    public void AddScore(int amt)
    {
        _score += amt;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + _movement * Time.deltaTime * movementSpeed);
    }
}
