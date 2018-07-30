using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    #region Public Properties

    public GameObject tree;
    public bool rightArrow;
    public bool leftArrow;
    public bool upArrow;
    public bool downArrow;
    public float movementSpeed;

    #endregion

    #region Private Properties

    private Animator animator;
    private Rigidbody2D rb2D;
    private Vector3 defaultScale;
    private bool stopCollision;
    private bool interactCollision;

    const string IdleAnimation = "Idle";
    const string MovingUp = "MovingUp";
    const string MovingDown = "MovingDown";
    const string MovingRight = "MovingRight";
    const string MovingLeft = "MovingRight";


    enum State
    {
        Idle,
        RunningRight,
        RunningLeft,
        RunningUp,
        RunningDown
    }

    State state;

    #endregion


    #region MonoBehaviour Events

    public void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
        defaultScale = transform.localScale;
    }

    public void Update() {
        rightArrow = Input.GetKey("right");
        leftArrow = Input.GetKey("left");
        upArrow = Input.GetKey("up");
        downArrow = Input.GetKey("down");

        CheckState();
        UpdateTransform();
    }

    #endregion

    #region Private Methods

    void SetOrKeepState(State state)
    {
        if (this.state == state) return;
        EnterState(state);
    }

    void ExitState()
    {

    }

    void EnterState(State state)
    {
        ExitState();
        switch (state)
        {
            case State.Idle:
                break;
            case State.RunningLeft:
                animator.StopPlayback();
                animator.Play(MovingRight);
                break;
            case State.RunningRight:
                animator.StopPlayback();
                animator.Play(MovingRight);
                break;
            case State.RunningUp:
                animator.StopPlayback();
                animator.Play(MovingUp);
                break;
            case State.RunningDown:
                animator.StopPlayback();
                animator.Play(MovingDown);
                break;
        }

        this.state = state;
    }

    void CheckState()
    {
        switch (state)
        {

            case State.Idle:
                Moving();
                break;

            case State.RunningLeft:
            case State.RunningRight:
            case State.RunningUp:
            case State.RunningDown:
                if (!Moving()) EnterState(State.Idle);
                else Moving();
                break;
        }
    }

    bool Moving()
    {
        if (rightArrow) SetOrKeepState(State.RunningRight);
        else if (upArrow) SetOrKeepState(State.RunningUp);
        else if (leftArrow) SetOrKeepState(State.RunningLeft);
        else if (downArrow) SetOrKeepState(State.RunningDown);
        else return false;
        return true;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("WallCollider"))
        {
            stopCollision = true;
        }
        else if (col.gameObject.CompareTag("BuildingCollider"))
        {
            stopCollision = true;
        }
        else if (col.gameObject.CompareTag("RockCollider"))
        {
            stopCollision = true;
        }
        else if (col.gameObject.CompareTag("TreeCollider"))
        {
            stopCollision = false;

            //Destroy(col.rigidbody.gameObject);
            //make tree transparent
            SpriteRenderer spRend = tree.transform.GetComponent<SpriteRenderer>();
            // copy the SpriteRenderer’s color property 
            spRend.color = new Color(1f, 1f, 1f, .0f);
            // change col’s alpha value (0 = invisible, 1 = fully opaque) 
            // change the SpriteRenderer’s color property to match the copy with the altered alpha value 
        }
    }

    void UpdateTransform()
    {

        switch (state)
        {
            case State.RunningLeft:
                if (!stopCollision)
                {
                    transform.localScale = new Vector2(defaultScale.x * -1, defaultScale.y);
                    transform.Translate(Vector2.left * Time.deltaTime, 0);
                }
                break;
            case State.RunningRight:
                if (!stopCollision)
                {
                    transform.localScale = new Vector2(defaultScale.x, defaultScale.y);
                    transform.Translate(Vector2.right * Time.deltaTime, 0);
                }
                break;
            case State.RunningUp:
                if (!stopCollision)
                {
                    transform.Translate(Vector2.up * Time.deltaTime, 0);
                }
                break;
            case State.RunningDown:
                if (!stopCollision)
                {
                    transform.Translate(Vector2.down * Time.deltaTime, 0);
                }
                break;
            case State.Idle:
                break;
        }
    }
    #endregion

}
