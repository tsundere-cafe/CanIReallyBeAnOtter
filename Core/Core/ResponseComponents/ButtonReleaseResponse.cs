using Otter;

namespace CanIReallyBeAnOtter.Core.ResponseComponents
{
  public abstract class ButtonReleaseResponse : Component
  {
    Button button;
    
    public ButtonReleaseResponse(Button button)
    {
      this.button = button;
    }

    public override void Update()
    {
      base.Update();

      if (button.Released)
      {
        OnRelease();
      }
    }

    protected abstract void OnRelease();
  }
}
