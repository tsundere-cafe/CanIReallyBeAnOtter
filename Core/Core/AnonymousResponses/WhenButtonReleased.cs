using CanIReallyBeAnOtter.Core.ResponseComponents;
using Otter;
using System;

namespace CanIReallyBeAnOtter.Core.AnonymousResponses
{
  public class WhenButtonReleased : ButtonReleaseResponse
  {
    Action action;
    
    public WhenButtonReleased(Button button, Action action)
      : base(button)
    {
      this.action = action;
    }

    protected override void OnRelease()
    {
      action();
    }
  }
}
