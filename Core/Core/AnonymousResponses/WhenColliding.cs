using CanIReallyBeAnOtter.Core.ResponseComponents;
using Otter;
using System;
using System.Collections.Generic;

namespace CanIReallyBeAnOtter.Core.AnonymousResponses
{
  public class WhenColliding<T> : CollisionResponse<T>
    where T : Entity
  {
    readonly Action<IEnumerable<T>> action;

    public WhenColliding(Collider collider, Enum tag, Action<IEnumerable<T>> action)
      : base(collider, tag)
    {
      this.action = action;
    }

    public WhenColliding(Collider collider, Enum[] tags, Action<IEnumerable<T>> action)
      : base(collider, tags)
    {
      this.action = action;
    }

    protected override void OnCollision(IEnumerable<T> others)
    {
      action(others);
    }
  }
}
