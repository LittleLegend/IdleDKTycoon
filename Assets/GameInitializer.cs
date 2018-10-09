using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public References _references;

    void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        InstantiatePrefabs();
    }

    public void InstantiatePrefabs()
    {
        Instantiate(_references._soundPrefab);
        Instantiate(_references._levelPrefab);
        Instantiate(_references._leftPipePrefab);
        Instantiate(_references._rightPipePrefab);
        Instantiate(_references._donkeyKongPrefab);
    }
}
