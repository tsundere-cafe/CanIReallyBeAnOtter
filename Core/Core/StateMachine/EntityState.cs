using Otter;
using System.Collections.Generic;

namespace CanIReallyBeAnOtter.Core.StateMachine
{
  public class EntityState
  {
    Component[] components;

    public EntityState(params Component[] components)
    {
      this.components = components;
    }

    public void Install(Entity entity)
    {
      entity.AddComponents(components);
    }

    public void Uninstall(Entity entity)
    {
      components.Each(_ => entity.RemoveComponent(_));
    }
  }
}
