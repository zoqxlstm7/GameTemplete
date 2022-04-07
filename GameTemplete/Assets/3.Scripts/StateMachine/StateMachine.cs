using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTemplete
{
    public abstract class State<T>
    {
        #region Variables
        protected T owner;
        protected StateMachine<T> stateMachine;
        #endregion Variables

        #region Main Methods
        public void SetOwnerAndStateMachine(T owner, StateMachine<T> stateMachine)
        {
            this.owner = owner;
            this.stateMachine = stateMachine;
            OnInit();
        }

        // 상태 초기화 함수
        public virtual void OnInit() { }
        // 상태 진입 함수
        public virtual void OnEnter() { }
        // 상태 업데이트 함수
        public abstract void OnUpdate(float deltaTime);
        // 상태 탈출 함수
        public virtual void OnExit() { }
        #endregion Main Methods
    }

    public class StateMachine<T>
    {
        #region Variables
        private T owner;

        private State<T> currentState;
        public State<T> CurrentState => currentState;

        private State<T> priviousState;
        public State<T> PriviousState => priviousState;

        private float elapsedTimeInState = 0.0f;
        public float ElapsedTimeInState => elapsedTimeInState;

        private Dictionary<System.Type, State<T>> states = new Dictionary<System.Type, State<T>>();
        #endregion Variables

        #region Generator
        public StateMachine(T owner, State<T> initialState)
        {
            this.owner = owner;

            AddState(initialState);

            currentState = initialState;
            currentState.OnEnter();
        }
        #endregion Generator

        #region Main Methods
        public void AddState(State<T> newState)
        {
            if (states.ContainsKey(newState.GetType()))
                return;

            newState.SetOwnerAndStateMachine(owner, this);
            states.Add(newState.GetType(), newState);
        }

        public void Update(float deltaTime)
        {
            elapsedTimeInState += deltaTime;
            currentState.OnUpdate(deltaTime);
        }

        public R ChangeState<R>() where R : State<T>
        {
            System.Type type = typeof(R);

            if (!states.ContainsKey(type))
                return null;
            if (currentState.GetType() == type)
                return currentState as R;

            currentState.OnExit();

            priviousState = currentState;
            currentState = states[type];

            elapsedTimeInState = 0.0f;
            currentState.OnEnter();

            return currentState as R;
        }
        #endregion Main Methods
    }
}
