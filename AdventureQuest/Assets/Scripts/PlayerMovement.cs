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
        animator = GetComponent<Animator>();
        defaultScale = transform.localScale;
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

        switch (state)
        {
            case State.RunningLeft:
                transform.Translate(Vector3.left * Time.deltaTime, Camera.main.transform);
                transform.localScale = new Vector3(defaultScale.x * -1, defaultScale.y, defaultScale.z);
                break;
            case State.RunningRight:
                transform.Translate(Vector3.right * Time.deltaTime, Camera.main.transform);
                transform.localScale = new Vector3(defaultScale.x, defaultScale.y, defaultScale.z);
                break;
            case State.RunningUp:
                transform.Translate(Vector3.up * Time.deltaTime, Camera.main.transform);
                break;
            case State.RunningDown:
                transform.Translate(Vector3.down * Time.deltaTime, Camera.main.transform);
                break;
            case State.Idle:
                break;
        }
    }
    #endregion

}
