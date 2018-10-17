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
    private DonkeyKongModel _donkeyKongModel;
    

    public DonkeyKongController(GameObject donkeyKongPrefab, DonkeyKongModel donkeyKongModel)
    {
        _donkeyKongPrefab = donkeyKongPrefab;
        _donkeyKongView = donkeyKongPrefab.GetComponent<DonkeyKongView>();
        _donkeyKongModel = donkeyKongModel;
    }

    public void Start()
    {
        _donkeyKongModel.StartAI();

    }

    public void Stop()
    {
        _donkeyKongModel.StopAI();

    }


    public void Throw()
    {
        SoundEffectService.Instance.PlayClip(ClipIdentifier.DonkeyKong);

    }

    public void MoveLeft()
    {

    }

    public void MoveRight()
    {

    }
    
}
