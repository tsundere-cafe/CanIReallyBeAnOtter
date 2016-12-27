using Otter;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CanIReallyBeAnOtter.Core.StateMachine
{
  public class EntityStates<T> : Component, IEnumerable<KeyValuePair<T, EntityState>>
  {
    public readonly Stack<EntityState> States = new Stack<EntityState>();

    readonly Dictionary<T, EntityState> StateLookup = new Dictionary<T, EntityState>();

    public EntityState ActiveState
    {
      get
      {
        return States.Count > 0 ? States.Peek() : null;
      }
    }

    public EntityState Add(T stateIndex, EntityState state)
    {
      StateLookup[stateIndex] = state;
      return state;
    }

    public void ReplaceState(T stateIndex)
    {
      EntityState state = StateLookup[stateIndex];
      if (ActiveState != null)
      {
        ActiveState.Uninstall(Entity);
        States.Pop();
      }

      States.Push(state);

      ActiveState.Install(Entity);
    }

    public void ReplaceAll(T stateIndex)
    {
      while (ActiveState != null)
      {
        PopState();
      }

      PushState(stateIndex);
    }

    public void PushState(T stateIndex)
    {
      EntityState state = StateLookup[stateIndex];
      if (ActiveState != null)
      {
        ActiveState.Uninstall(Entity);
      }

      States.Push(state);

      ActiveState.Install(Entity);
    }

    public void PopState()
    {
      if (ActiveState != null)
      {
        ActiveState.Uninstall(Entity);
        States.Pop();
      }

      if (ActiveState != null)
      {
        ActiveState.Install(Entity);
      }
    }

    /// <summary>
    /// Pops state and all of its child states
    /// </summary>
    /// <param name="state"></param>
    public void PopState(T stateIndex)
    {
      EntityState state = StateLookup[stateIndex];
      if (States.Contains(state))
      {
        while (ActiveState != state)
        {
          PopState();
        }

        PopState();
      }
      else
      {
        throw new InvalidOperationException("Trying to pop a state that isn't on the stack.");
      }
    }

    public IEnumerator<KeyValuePair<T, EntityState>> GetEnumerator()
    {
      return StateLookup.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return StateLookup.GetEnumerator();
    }
  }
}
