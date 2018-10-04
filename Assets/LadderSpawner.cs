using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderSpawner : MonoBehaviour {

    public GameObject Ladder;


	void Start () {
		
	}
	
	void Update () {
		
	}

    public IEnumerator SpawnLadders()
    {
        for(int i =0; i <= Random.Range(1, 5); i++)
        {
            
                

            yield return true;
        }




    }
}
