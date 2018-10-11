using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sound;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DonkeyKongController
{

    private GameObject _donkeyKongPrefab;
    private DonkeyKongView _donkeyKongView;

    [SerializeField] public float _projectileSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _shotCooldown;

    [Space(10)]

    private GameObject projectile;



    private float _timeSinceLastShot;
    public bool _collectableRunning;
    private const float MaxOffset = 6.5f;

    public DonkeyKongController(GameObject donkeyKongPrefab)
    {
        _donkeyKongPrefab = donkeyKongPrefab;
        _donkeyKongView = donkeyKongPrefab.GetComponent<DonkeyKongView>();
    }

    void Start()
    {

        _timeSinceLastShot = _shotCooldown;

    }


    private void GoLeft()
    {

        
        ClampXPosition();
    }

    private void GoRight()
    {
        
        ClampXPosition();
    }

    private void Fire()
    {
        
        

        SoundEffectService.Instance.PlayClip(ClipIdentifier.DonkeyKong);
    }

    void Update()
    {
        if (StateController.IsPaused) return;

        if (_timeSinceLastShot < _shotCooldown)
        {
            _timeSinceLastShot += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GoLeft();

        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GoRight();

        }
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A))
        {

        }

        

        if (Input.GetKey(KeyCode.Space))
        {
               TryFiring();
        }
            else
            {

            }
        


        



    }

    private void TryFiring()
        {
            if (_timeSinceLastShot < _shotCooldown) return;
            Fire();
            _timeSinceLastShot = 0f;
        }





        private void ClampXPosition()
        {
            
            
            
        }
}
