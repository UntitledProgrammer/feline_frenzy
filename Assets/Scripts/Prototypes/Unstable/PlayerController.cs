using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum States { GROUNDED, JUMPING, LENGTH }

//https://www.youtube.com/watch?v=OjreMoAG9Ec

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Attributes:
    public float raycastLength;
    private Rigidbody2D uRigidbody;
    private Animator uAnimator;
    private float stamina;
    private FiniteStateMachine<System.Type> stateMachine;
    private IState<System.Type>[] states = new IState<System.Type>[(int)States.LENGTH];

    //Properties:
    public bool IsGrounded { get => Physics2D.Raycast(transform.position, -transform.up, raycastLength).collider != null; }
    public IState<System.Type> GroundedState { get => states[(int)States.GROUNDED]; }
    public Rigidbody2D URigidbody { get => uRigidbody; }

    //Methods:
    private void Awake()
    {
        //Initialise components.
        uRigidbody = GetComponent<Rigidbody2D>();
        uAnimator = GetComponent<Animator>();

        stateMachine = new FiniteStateMachine<System.Type>();
        stateMachine.BlackBoard.Add(typeof(PlayerController), this);
        stateMachine.ChangeState(GroundedState);

        //Initialise states.
        states[(int)States.GROUNDED] = new GroundedState(stateMachine);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * raycastLength);
    }
}

public class GroundedState : IState<System.Type>
{
    //Attributes:
    private PlayerController controller;

    //Constructor:
    public GroundedState(FiniteStateMachine<System.Type> fsm) : base(fsm)
    {
        controller = fsm.BlackBoard.Get<PlayerController>(typeof(PlayerController));
    }

    //Methods:
    public override void Enter()
    {
        Debug.Log("Enter");

        if (controller == null) StateMachine.ChangeState(null);
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }

    public override void Update()
    {
        controller.URigidbody.AddForce(controller.transform.right * 10);
    }
}