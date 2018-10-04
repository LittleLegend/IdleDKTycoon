using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownBarrel : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public void Start()
    {
        //Debug.Log(string.Format("Firing TopDownBarrel with speed: {0}", projectileSpeed));
        _rigidbody2D.velocity = Vector2.down * GameObject.Find("DonkeyKong").GetComponent<DonkeyKongController>()._projectileSpeed;
        Destroy(gameObject, 10f);
    }
}