using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectableSpawner : MonoBehaviour
{

	private float _setupTime;
	[SerializeField] private float _spawnTime;
	[SerializeField] public float _destroyTime;
	[SerializeField] private GameObject _collectablePrefab;
	
	void Start ()
	{
		_setupTime = GameObject.Find("Spawner").GetComponent<MarioSpawner>()._setupTime;
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		yield return new WaitForSeconds(_setupTime);
		while (true)
		{
			while (StateController.IsPaused)
            {
                yield return null;
            }
			
			GameObject collectable = Instantiate(_collectablePrefab);
			CollectableEnum type = RandomCollectable();
			collectable.GetComponent<Collectable>().SetType(type,InventoryManager.Instance._collectableIconsDictionary[type]);
			collectable.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-4, 2), 0);
			yield return new WaitForSeconds(_spawnTime);
		}
	}

	private CollectableEnum RandomCollectable()
	{
		List<CollectableEnum> collectables = new List<CollectableEnum>
		{
			CollectableEnum.Big, CollectableEnum.Luigi, CollectableEnum.Pow, CollectableEnum.Daisy
		};
		
		return collectables[Random.Range(0,4)];
	}
	
}
