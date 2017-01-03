using Otter;

namespace CanIReallyBeAnOtter.Core.StateMachine
{
  public class EntityState
  {
    public readonly Component[] components;

    public EntityState(params Component[] components)
    {
      this.components = components;
    }
  }
}
