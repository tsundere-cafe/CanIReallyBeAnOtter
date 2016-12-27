using CanIReallyBeAnOtter.Core.ResponseComponents;
using Otter;
using System;

namespace CanIReallyBeAnOtter.Core.AnonymousResponses
{
  public class WhenResting : RestResponse
  {
    readonly Action action;

    public WhenResting(float restThreshold, Collider collider, Enum solid, Action action)
      : base(restThreshold, collider, solid)
    {
      this.action = action;
    }

    public WhenResting(float restThreshold, Collider collider, Enum[] solid, Action action)
      : base(restThreshold, collider, solid)
    {
      this.action = action;
    }

    protected override void OnRest()
    {
      action();
    }
  }
}
