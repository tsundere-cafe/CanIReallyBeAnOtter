using Otter;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace CanIReallyBeAnOtter.Core.StateMachine
{
  public class EntityStates<T> : Component
  {
    public readonly Stack<EntityState> States = new Stack<EntityState>();

    readonly Dictionary<T, EntityState> StateLookup = new Dictionary<T, EntityState>();

    public T ActiveState
    {
      get
      {
        var top = TopState;

        foreach (var entry in StateLookup)
        {
          if (entry.Value == top)
          {
            return entry.Key;
          }
        }

        throw new InvalidOperationException("Either no state is active or the active state wasn't registered to this state machine");
      }
    }

    EntityState TopState
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

    void UpdateComponents(IEnumerable<Component> oldComponents, IEnumerable<Component> newComponents)
    {
      foreach (var component in oldComponents)
      {
        if (!newComponents.Contains(component))
        {
          Entity.RemoveComponent(component);
        }
      }

      foreach (var component in newComponents)
      {
        if (!oldComponents.Contains(component))
        {
          Entity.AddComponent(component);
        }
      }
    }

    public void ReplaceState(T stateIndex)
    {
      var newState = StateLookup[stateIndex];

      if (TopState != null)
      {
        PushState(stateIndex);
      }
      else
      {
        var oldState = States.Pop();
        UpdateComponents(oldState.components, newState.components);
        States.Push(newState);
      }
    }

    public void ReplaceAll(T stateIndex)
    {
      var allOldComponets = States.SelectMany(_ => _.components).Distinct();
      var newState = StateLookup[stateIndex];

      States.Clear();
      UpdateComponents(allOldComponets, newState.components);
      States.Push(newState);
    }

    public void PushState(T stateIndex)
    {
      EntityState newState = StateLookup[stateIndex];
      if (TopState == null)
      {
        Entity.AddComponents(newState.components);
      }
      else
      {
        var oldState = States.Pop();
        UpdateComponents(oldState.components, newState.components);
      }

      States.Push(newState);
    }

    public void PopState()
    {
      if (TopState != null)
      {
        var oldState = States.Pop();

        if (TopState != null)
        {
          UpdateComponents(oldState.components, TopState.components);
        }
        else
        {
          foreach (var component in oldState.components)
          {
            Entity.RemoveComponent(component);
          }
        }
      }
    }

    /// <summary>
    /// Pops state and all of its child states
    /// </summary>
    /// <param name="state"></param>
    public void PopState(T stateIndex)
    {
      var newState = StateLookup[stateIndex];

      if (States.Contains(newState))
      {
        var allOldComponets = new List<Component>();

        while (TopState != newState)
        {
          allOldComponets.AddRange(TopState.components);
          PopState();
        }

        allOldComponets.AddRange(TopState.components);
        PopState();

        UpdateComponents(allOldComponets, TopState.components);
      }
      else
      {
        throw new InvalidOperationException("Trying to pop a state that isn't on the stack.");
      }
    }
  }
}
