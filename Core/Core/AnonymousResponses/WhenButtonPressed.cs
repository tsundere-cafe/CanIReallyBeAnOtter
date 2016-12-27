using CanIReallyBeAnOtter.Core.ResponseComponents;
using Otter;
using System;

namespace CanIReallyBeAnOtter.Core.AnonymousResponses
{
  public class WhenButtonPressed : ButtonPressResponse
  {
    Action action;
    
    public WhenButtonPressed(Button button, Action action)
      : base(button)
    {
      this.action = action;
    }

    protected override void OnPress()
    {
      action();
    }
  }
}
