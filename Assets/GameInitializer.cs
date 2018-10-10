using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public References _references;
    private StateMachine _stateMachine;
    private DonkeyKongController _donkeyKongController;

    void Start()
    {
        _stateMachine = new StateMachine(_references);
        _donkeyKongController = new DonkeyKongController(_references._donkeyKongPrefab);

        InstantiatePrefabs();
    }

    

    public void InstantiatePrefabs()
    {
        Instantiate(_references._soundPrefab);
        Instantiate(_references._levelPrefab);
        Instantiate(_references._leftPipePrefab);
        Instantiate(_references._rightPipePrefab);
        
    }
}
