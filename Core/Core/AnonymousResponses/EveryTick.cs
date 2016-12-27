using CanIReallyBeAnOtter.Core.ResponseComponents;
using System;

namespace CanIReallyBeAnOtter.Core.AnonymousResponses
{
  public class EveryTick : TickResponse
  {
    readonly Action action;

    public EveryTick(float interval, Action action)
      : base(interval)
    {
      this.action = action;
    }

    protected override void OnTick()
    {
      action();
    }
  }
}
