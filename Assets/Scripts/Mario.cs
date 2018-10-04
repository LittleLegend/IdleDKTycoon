using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using UnityEngine.UI;

public class Mario : MonoBehaviour {

    [SerializeField] private float _speed;
    [SerializeField] private float _boundaries;
    [SerializeField] private float _jumpChance;

    [Space(10)]
    [SerializeField] private Animator _marioAnimator;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private BoxCollider2D _hitBox;

    private bool _isJumping;
    private bool _isClimbing;
    private bool _isDead = false;
    private bool _shouldWalkTowardsDaisy;
    private Transform _latestDaisyTransform;
    private Vector2 _movementDirection;
    
	private void Start ()
	{
        _body = gameObject.GetComponent<Rigidbody2D>();
        _marioAnimator = GetComponent<Animator>();
        StartWalking();
        StartCoroutine(JumpCheck());
    }
	
	private void Update ()
	{
	    if (StateController.IsPaused)
	    {
	        Move(Vector2.zero);
	    }
	    else
	    {
            Move(_movementDirection);
            if (_shouldWalkTowardsDaisy && !_isClimbing && !_isJumping && _latestDaisyTransform)
                WalkTowardsDaisy();
	        Boundary();
        }
    }

    public IEnumerator JumpCheck()
    {
        while (true)
        {
            if (Random.Range(1, 101) <= _jumpChance && 
                !_isJumping && 
                !_isClimbing && 
                !_isDead &&
                !StateController.IsPaused)
            {
                StartCoroutine(Jump(0.5f,3));
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void Jump()
    {

    }

    public IEnumerator Jump(float height, float jumpspeed)
    {
        _isJumping = true;
        float jumppower = 1;
        GetComponent<BoxCollider2D>().enabled = false;

        while (jumppower > 0)
        {
            _body.velocity = new Vector2(_movementDirection.x,jumppower) * jumpspeed;

            yield return new WaitForSeconds(height/100);
            jumppower = jumppower - 0.1f;

        }

        jumppower = -1;

        while (jumppower < 0)
        {
            _body.velocity = new Vector2(_movementDirection.x, jumppower) * jumpspeed;

            yield return new WaitForSeconds(height/100);
            jumppower = jumppower + 0.1f;

        }
        _isJumping = false;
        Move(_movementDirection);
        GetComponent<BoxCollider2D>().enabled = true;
        EndOfJumpingOrClimbing();
    }

    private void Boundary()
    {
        if( transform.position.x > _boundaries)
            Move(Vector2.left);

        if (transform.position.x < -_boundaries)
            Move(Vector2.right);
    }


    public void StartWalking()
    {
        if (transform.position.x > 0)
            Move(Vector2.left);

        if (transform.position.x < 0)
            Move(Vector2.right);
    }

    public void Move(Vector2 movement)
    {
        if (StateController.IsPaused)
            movement = Vector2.zero;
        if (movement != Vector2.zero)
            _movementDirection = movement;
        _body.velocity = movement * _speed;
        ChooseDirection(movement);
    }

    public void ChooseDirection(Vector2 movement)
    {
        if (movement == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (movement == Vector2.right)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void RandomMove()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                Move(Vector2.right);
                break;

            case 1:
                Move(Vector2.left);
                break;

            default:
                Move(Vector2.zero);
                break;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("Ladder"): WalkUp();  break; 
            case ("Barrel"): Die(); break; 
            case ("Luigi"): Die(); break;
            case ("Pow"): Die(); break;
            case ("Pauli"): StateController.GameOver(); break;
            case ("Boundary"): WalkAwayFromBoundary(collision.transform); break;
            case ("Daisy"): StartWalkingTowardsDaisy(collision.transform); break;
        }
    }

    private void WalkUp()
    {
        if (_shouldWalkTowardsDaisy) return;
        _marioAnimator.SetInteger("climb", 1);
        _isClimbing = true;
        Move(Vector2.up);
    }

    private void Die()
    {
        if (_isDead) return;
        SoundEffectService.Instance.PlayClip(ClipIdentifier.Death);
        
        GameObject.Find("Score").GetComponent<ScoreManager>().AddPoint();
        _marioAnimator.SetBool("dead", true);
        StartCoroutine(Dying());
    }

    public IEnumerator Dying()
    {
        int state = 0;
        _isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;

        while (state == 0)
        {
            _speed = 1;
            Move(Vector2.up);
            yield return new WaitForSeconds(0.5f);
            state = 1;
        }

        while (state == 1)
        {
            _speed = 7;
            Move(Vector2.down);
            yield return new WaitForSeconds(2);
            state = 2;
        }

        Destroy(gameObject);
    }

    public void WalkAwayFromBoundary(Transform boundaryTransform)
    {
        var differenceX = transform.position.x - boundaryTransform.position.x;
        Move(differenceX < 0 ? Vector2.left : Vector2.right);
    }

    private void StartWalkingTowardsDaisy(Transform daisyTransform)
    {
        if (_isDead) return;
        _shouldWalkTowardsDaisy = true;
        _latestDaisyTransform = daisyTransform;
    }

    private void WalkTowardsDaisy()
    {
        var differenceX = transform.position.x - _latestDaisyTransform.position.x;
        if (differenceX >= 0.2)
        {
            Move(differenceX < 0 ? Vector2.right : Vector2.left);

        }
        else
        {if (_isDead==false)
            {
                Move(Vector2.zero);
                _jumpChance = 0;
            }
        }
    }

    public void EndOfJumpingOrClimbing()
    {
        if (!_shouldWalkTowardsDaisy) return;
        _shouldWalkTowardsDaisy = false;
        if (!_latestDaisyTransform) return;
        StartWalkingTowardsDaisy(_latestDaisyTransform);
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Ladder")) return;
        _marioAnimator.SetInteger("climb", 0);
        _isClimbing = false;
        RandomMove();
        EndOfJumpingOrClimbing();
    }
}