using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    #region Public Properties
    public bool rightArrow;
    public bool leftArrow;
    public bool upArrow;
    public bool downArrow;
    #endregion

    #region Private Properties

    private Animator animator;
    private Rigidbody2D rb2D;
    private Vector3 defaultScale;
    private float movementSpeed;
    private float fullSpeed;
    private float reducedSpeed;
    private bool stopCollision;
    private bool playerIsSlowed;

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
        defaultScale = transform.localScale;
        fullSpeed = 1;
        reducedSpeed = fullSpeed / 2;
    }

    public void Update() {
        rightArrow = Input.GetKey("right");
        leftArrow = Input.GetKey("left");
        upArrow = Input.GetKey("up");
        downArrow = Input.GetKey("down");
        movementSpeed = fullSpeed;

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("WaterCollider"))
        {
            playerIsSlowed = true;
        }
        if (col.gameObject.CompareTag("TreeCollider"))
        {

        }
        if (col.gameObject.CompareTag("CoinCollider"))
        {
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("WaterCollider"))
        {
            playerIsSlowed = false;
        }
    }

    void UpdateTransform()
    {
        if (playerIsSlowed == true)
        {
            movementSpeed = reducedSpeed;
        }
        else
        {
            movementSpeed = fullSpeed;
        }

        switch (state)
        {
            case State.RunningLeft:
                    transform.localScale = new Vector2(defaultScale.x * -1, defaultScale.y);
                    transform.Translate(Vector2.left * Time.deltaTime * movementSpeed, 0);
                break;
            case State.RunningRight:
                    transform.localScale = new Vector2(defaultScale.x, defaultScale.y);
                    transform.Translate(Vector2.right * Time.deltaTime * movementSpeed, 0);
                break;
            case State.RunningUp:
                    transform.Translate(Vector2.up * Time.deltaTime * movementSpeed, 0);
                break;
            case State.RunningDown:
                    transform.Translate(Vector2.down * Time.deltaTime * movementSpeed, 0);
                break;
            case State.Idle:
                break;
        }
    }
    #endregion

}
