using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbDonkeyKongAI : DonkeyKongAI
{
    public DumbDonkeyKongAI(DonkeyKongModel donkeyKongModel)
    {
        _donkeyKongModel = donkeyKongModel;
    }

    public override void Start()
    {
        _donkeyKongModel.Throw();
    }

    public override void Stop()
    {
        
    } 
}
