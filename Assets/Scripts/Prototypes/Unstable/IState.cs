using UnityEngine;
using System.Collections.Generic;

public class Blackboard<KeyType>
{
    //Attributes:
    private Dictionary<KeyType, Component> components;

    //Constructor:
    public Blackboard()
    {
        components = new Dictionary<KeyType, Component>();
    }

    //Methods:
    public void Add(KeyType key, Component component)
    {
        components[key] = component;
    }

    public Component Get(KeyType key)
    {
        if (!components.ContainsKey(key)) return default;

        return components[key];
    }

    public ComponentType Get<ComponentType>(KeyType key) where ComponentType : Component
    {
        if (!components.ContainsKey(key)) return default;

        return (ComponentType)(components[key]);
    }

    //Operators:
    public Component this[KeyType index]
    {
        get => components[index];
    }

}

public abstract class IState<BlackboardType>
{
    //Attributes:
    private FiniteStateMachine<BlackboardType> stateMachine;

    //Properties:
    public FiniteStateMachine<BlackboardType> StateMachine { get => stateMachine; }

    //Constructor:
    protected IState(FiniteStateMachine<BlackboardType> fsm)
    {
        stateMachine = fsm;
    }

    //Methods:
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

public sealed class FiniteStateMachine<Data>
{
    //Attributes:
    private IState<Data> currentState;
    private Blackboard<Data> blackboard;

    //Constructor:
    public FiniteStateMachine(IState<Data> state) { currentState = state; blackboard = new Blackboard<Data>(); }
    public FiniteStateMachine() { currentState = null; blackboard = new Blackboard<Data>(); }

    //Properties:
    public Blackboard<Data> BlackBoard { get => blackboard; }
    public IState<Data> ActiveState { get => currentState; }

    //Methods:
    public void ChangeState(IState<Data> state)
    {
        if(currentState != null) currentState.Exit();

        currentState = state;

        currentState.Enter();
    }

    public void Update()
    {
        if(currentState != null) currentState.Update();
    }
}