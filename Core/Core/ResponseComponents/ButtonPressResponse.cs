using Otter;

namespace CanIReallyBeAnOtter.Core.ResponseComponents
{
  public abstract class ButtonPressResponse : Component
  {
    Button button;
    
    public ButtonPressResponse(Button button)
    {
      this.button = button;
    }

    public override void Update()
    {
      base.Update();

      if (button.Pressed)
      {
        OnPress();
      }
    }

    protected abstract void OnPress();
  }
}
