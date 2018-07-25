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
    public float movementSpeed;

    #endregion

    #region Private Properties

    private Animator animator;
    private Rigidbody2D rb2D;
    private Vector3 defaultScale;
    private bool collision;

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
            collision = true;
        }
        else if (col.gameObject.CompareTag("BuildingCollider"))
        {
            collision = true;
        }
        else if (col.gameObject.CompareTag("RockCollider"))
        {
            collision = true;
        }
    }

    void UpdateTransform()
    {

        switch (state)
        {
            case State.RunningLeft:
                if (!collision)
                {
                    transform.localScale = new Vector2(defaultScale.x * -1, defaultScale.y);
                    transform.Translate(Vector3.left * Time.deltaTime, Camera.main.transform);
                }
                break;
            case State.RunningRight:
                if (!collision)
                {
                    transform.localScale = new Vector3(defaultScale.x, defaultScale.y, defaultScale.z);
                    transform.Translate(Vector3.right * Time.deltaTime, Camera.main.transform);
                }
                break;
            case State.RunningUp:
                if (!collision)
                {
                    transform.Translate(Vector3.up * Time.deltaTime, Camera.main.transform);
                }
                break;
            case State.RunningDown:
                if (!collision)
                {
                    transform.Translate(Vector3.down * Time.deltaTime, Camera.main.transform);
                }
                break;
            case State.Idle:
                break;
        }
    }
    #endregion

}
