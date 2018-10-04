using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class ShoutForHelp : MonoBehaviour
{

	[SerializeField] private GameObject _help;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(Help());
	}

	IEnumerator Help()
	{
		yield return new WaitForSeconds(3);
		while (true)
		{
			yield return new WaitForSeconds(15);
			_help.SetActive(true);
			yield return new WaitForSeconds(5);
			_help.SetActive(false);
		}
	}
	
}
