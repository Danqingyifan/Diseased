using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
//using UnityStandardAssets.CrossPlatformInput;
using Cinemachine;

//Direction Enum
[Flags]
public enum Orientation
{
    Up,
    Down,
    Left,
    Right
}

public class D_PlayerController : MonoBehaviour
{
    public static D_PlayerController instance;

    private Orientation _orientation;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _moveCollider;

    [SerializeField] private float _moveSpeed = 5.0f;
    private bool _canMove = true;
    public string areaTransitionName; // Add this property to track the transition name

    //for rigidBody.Cast
    private Vector2 _movementInput;
    private ContactFilter2D _movementFilter = new ContactFilter2D();
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();

    private float _collsionOffset = 0.1f;

    //for interact Raycast
    [SerializeField] private LayerMask layerMask;


    //Map OrientationEnum to Vector2 for raycast
    private Dictionary<Orientation, Vector2> OrientationMap = new Dictionary<Orientation, Vector2>
    {
        { Orientation.Up, Vector2.up },
        { Orientation.Down, Vector2.down },
        { Orientation.Left, Vector2.left },
        { Orientation.Right, Vector2.right }
    };

    //Interact Distance
    [SerializeField] private float _interactDistance = 1.0f;

    // Awake is called when the script instance is being loaded, before Start
    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        _rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        _animator = this.gameObject.GetComponent<Animator>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _moveCollider = this.gameObject.GetComponent<BoxCollider2D>();
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero && _canMove)
        {
            bool success = TryMove(_movementInput);
            // sperate x and y movement
            if (!success && Mathf.Abs(_movementInput.x) > 0)
            {
                success = TryMove(new Vector2(_movementInput.x, 0));
            }

            if (!success && Mathf.Abs(_movementInput.y) > 0)
            {
                success = TryMove(new Vector2(0, _movementInput.y));
            }

            _animator.SetFloat("Speed", success ? _movementInput.sqrMagnitude : 0);
        }
    }

    // event
    void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();

        _animator.SetFloat("Horizontal", _movementInput.x);
        _animator.SetFloat("Vertical", _movementInput.y);

        //Decide the Orientation of the player
        if (_movementInput.x > 0)
        {
            _orientation = Orientation.Right;
        }
        else if (_movementInput.x < 0)
        {
            _orientation = Orientation.Left;
        }
        else if (_movementInput.y > 0)
        {
            _orientation = Orientation.Up;
        }
        else if (_movementInput.y < 0)
        {
            _orientation = Orientation.Down;
        }

        //kind of weird
        _animator.SetFloat("Orientation", (float)_orientation / 3);

        if (_movementInput == Vector2.zero)
        {
            _animator.SetFloat("Speed", 0);
        }

    }

    void OnInteract()
    {
        //set up contact filter
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(layerMask);
        contactFilter.useTriggers = true;

        //check if the player is in front of the interactable object
        RaycastHit2D[] hits = Physics2D.RaycastAll(_spriteRenderer.transform.position, OrientationMap[_orientation],
            _interactDistance, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            // ignore Collider2D of the player
            if (hit.collider != null && hit.collider != _moveCollider)
            {
                GameObject target = hit.collider.gameObject;

                // if for Trigger Dialog
                if (target.GetComponent<D_DialogTrigger>() != null)
                {
                    target.GetComponent<D_DialogTrigger>().TriggerDialog();
                }

                break;
            }
        }
    }

    //function
    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return false;
        }

        int count = _rigidBody.Cast(
            direction,
            _movementFilter,
            _castCollisions,
            _moveSpeed * Time.deltaTime + _collsionOffset
        );
        if (count == 0)
        {
            //change transform
            _rigidBody.MovePosition(_rigidBody.position + direction * _moveSpeed * Time.deltaTime);
            return true;
        }

        return false;
    }


    public void LockMovement()
    {
        _canMove = false;
    }

    public void UnlockMovement()
    {
        _canMove = true;
    }
}
