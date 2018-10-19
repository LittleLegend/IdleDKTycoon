using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongModel{

    
    private IDonkeyKongAI _donkeyKongAI;

	public DonkeyKongModel(DonkeyKongController donkeyKongController)
    {
        _donkeyKongAI = new DumbDonkeyKongAI(this);
    }

    public void StartAI()
    {
        _donkeyKongAI.Start();
    }

    public void StopAI()
    {
        _donkeyKongAI.Start();
    }

    public void Throw()
    {
        _donkeyKongController.Throw();

    }

    public void MoveLeft()
    {
        _donkeyKongController.MoveLeft();
    }

    public void MoveRight()
    {
        _donkeyKongController.MoveRight();
    }

}
