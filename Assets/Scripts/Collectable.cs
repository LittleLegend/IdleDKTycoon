using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	private CollectableEnum _type;
	
	void Start()
	{
		StartCoroutine(Destroy());
	}

	public void SetType(CollectableEnum type, Sprite icon)
	{
		_type = type;
		this.GetComponent<SpriteRenderer>().sprite = icon;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Barrel")
		{
			GameObject.Find("DonkeyKong").GetComponent<DonkeyKongController>().ReceivedCollectable(_type);
			Destroy(this.gameObject);
		}
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(GameObject.Find("Spawner").GetComponent<CollectableSpawner>()._destroyTime);
		Destroy(this.gameObject);
	}
}
