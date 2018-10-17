using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongView : MonoBehaviour {

    private Animator _donkeyKongAnimator;
    


    void Start () {
        _donkeyKongAnimator = gameObject.GetComponent<Animator>();

        _donkeyKongAnimator.SetBool("move", true);
        _donkeyKongAnimator.SetBool("move", true);
        _donkeyKongAnimator.SetBool("move", false);
        _donkeyKongAnimator.SetBool("shoot", false);
        _donkeyKongAnimator.SetBool("shoot", true);

        //gameObject.transform.position += Vector3.right * Time.deltaTime * _movementSpeed;
        //var barrelInstance = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
    }
	
}
