using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.CrossPlatformInput;

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
    //Make instance of this script to be able reference from other scripts!
    public static D_PlayerController instance;

    [HideInInspector] public Orientation orientation;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public BoxCollider2D moveCollider;

    [SerializeField] private float _moveSpeed = 5.0f;
    private bool _canMove = true;

    //for rigidBody.Cast
    private Vector2 _movementInput;
    private ContactFilter2D _movementFilter = new ContactFilter2D();
    private List<RaycastHit2D> _castCollisions = new List<RaycastHit2D>();
    [Tooltip("The offset applying to the cast to prevent the player from getting stuck on walls")]
    [SerializeField] private float _collsionOffset = 0.1f;

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
    [SerializeField]
    private float _interactDistance = 1.0f;

    // Awake is called when the script instance is being loaded, before Start
    // Use this for initialization
    void Awake()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        moveCollider = this.gameObject.GetComponent<BoxCollider2D>();
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
        }
    }

    // event
    void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
        //Decide the Orientation of the player
        if (_movementInput.x > 0)
        {
            orientation = Orientation.Right;
        }
        else if (_movementInput.x < 0)
        {
            orientation = Orientation.Left;
        }
        else if (_movementInput.y > 0)
        {
            orientation = Orientation.Up;
        }
        else if (_movementInput.y < 0)
        {
            orientation = Orientation.Down;
        }
    }

    void OnInteract()
    {
        //set up contact filter
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(layerMask);
        contactFilter.useTriggers = true;

        //check if the player is in front of the interactable object
        RaycastHit2D[] hits = Physics2D.RaycastAll(spriteRenderer.transform.position, OrientationMap[orientation], _interactDistance, layerMask);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject target = hit.collider.gameObject;
            // ignore Collider2D of the player
            if (hit.collider != null && hit.collider != moveCollider)
            {
                D_DialogTrigger dialogTrigger = target.GetComponent<D_DialogTrigger>();
                if (dialogTrigger != null)
                {
                    dialogTrigger.TriggerDialogue();
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
        int count = rigidBody.Cast(
            direction,
            _movementFilter,
            _castCollisions,
            _moveSpeed * Time.deltaTime + _collsionOffset
        );
        if (count == 0)
        {
            //change transform
            rigidBody.MovePosition(rigidBody.position + direction * _moveSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }


    private void LockMovement()
    {
        _canMove = false;
    }
    private void UnlockMovement()
    {
        _canMove = true;
    }
}

