using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;

public class LuigiPipe : MonoBehaviour {

    public int duration;
    public MarioSpawner LuigiSpawner;

	// Use this for initialization
	void Start () {
        StartCoroutine(Die());
        RandomMove();
        SoundEffectService.Instance.PlayClip(ClipIdentifier.Luigi);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RandomMove()
    {
        int rand = Random.Range(0, 5);

        switch (rand)
        {
            case 0:
                transform.position = new Vector2(6.51998f, -2.40f);

                break;

            case 1:
                transform.position = new Vector2(6.51998f, 0.83f);

                break;

            case 2:
                transform.position = new Vector2(6.51998f, 0.67f);

                break;

            case 3:
                transform.position = new Vector2(6.51998f, 2.17f);

                break;

            case 4:
                transform.position = new Vector2(6.51998f, 3.8f);

                break;

            default:
                transform.position = new Vector2(6.51998f, -2.33f);
                break;
        }
    }

    public IEnumerator Die()
    {
        int state = 0;

        while (state == 0)
        {
            yield return new WaitForSeconds(4);
            state = 1;
        }



        while (state == 1)
        {
            
            yield return new WaitForSeconds(4);
            state = 2;
        }

        while (state == 2)
        {
            Destroy(LuigiSpawner);
            yield return new WaitForSeconds(4);
            state = 3;
        }

        Destroy(gameObject);
    }
}
