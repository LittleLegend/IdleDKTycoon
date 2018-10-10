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
        projectile = InventoryManager.Instance._collectablePrefabDictionary[CollectableEnum.Normal];
    }


    private void GoLeft()
    {

        gameObject.transform.position += Vector3.left * Time.deltaTime * _movementSpeed;
        ClampXPosition();
    }

    private void GoRight()
    {
        gameObject.transform.position += Vector3.right * Time.deltaTime * _movementSpeed;
        ClampXPosition();
    }

    private void Fire()
    {
        DonkeyAnimator.SetBool("shoot", true);
        var barrelInstance = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);

        SoundEffectService.Instance.PlayClip(ClipIdentifier.DonkeyKong);
    }

    void Update ()
    {
        if (StateController.IsPaused) return;

        if (_timeSinceLastShot < _shotCooldown)
        { 
                _timeSinceLastShot += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GoLeft();
            DonkeyAnimator.SetBool("move", true);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GoRight();
            DonkeyAnimator.SetBool("move", true);
        }
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) )
        {
            DonkeyAnimator.SetBool("move", false);
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (InventoryManager.Instance.GetActiveSpecialAmount()!=0 && !_collectableRunning)
            {
                var obj = InventoryManager.Instance.GetActiveSpecial();
                if (InventoryManager.Instance._collectableProjectileDictionary[obj])
                {
                    projectile = InventoryManager.Instance._collectablePrefabDictionary[obj];
                   
                else
                {
                    if (obj == CollectableEnum.Daisy)
                    {
                      
                    }
                    else
                    {
                        
                    }
                }
            
                SoundEffectService.Instance.PlayClip(ClipIdentifier.ItemsActivate);
                InventoryManager.Instance.RemoveCollectableFromActive();
               
            }
            
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            TryFiring();
        }
        else
        {
            DonkeyAnimator.SetBool("shoot", false);
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
        var position = transform.position;
        position.x = Mathf.Clamp(position.x, -MaxOffset, MaxOffset);
        transform.position = position;
    }

    

    
}
