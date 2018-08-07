using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

    #region Public Properties

    public float movementSpeed = 4;
    public bool rightArrow;
    public bool leftArrow;
    public bool upArrow;
    public bool downArrow;
    private bool stopCollision;
    private Rigidbody2D rb2D;
    #endregion

    #region Private Properties

    Animator animator;
    Vector3 defaultScale;
    float stateStartTime;

    float timeInState
    {
        get { return Time.time - stateStartTime; }
    }

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
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb2D.interpolation = RigidbodyInterpolation2D.Extrapolate;
    }

    public void Update() {
        rightArrow = Input.GetKey("right");
        leftArrow = Input.GetKey("left");
        upArrow = Input.GetKey("up");
        downArrow = Input.GetKey("down");

        ContinueState();
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
    void OnColisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Beach"))
        {
            stopCollision = true;
        }

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
        stateStartTime = Time.time;
    }

    void ContinueState()
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

    void UpdateTransform()
    {

        switch (state) { 
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
