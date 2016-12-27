using Otter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CanIReallyBeAnOtter.Core.ResponseComponents
{
  public abstract class CollisionResponse<T> : Component
    where T : Entity
  {
    readonly Collider collider;
    readonly Enum[] tags;

    public bool RenderCollider { get; set; }

    public CollisionResponse(Collider collider, Enum tag)
    {
      this.collider = collider;
      this.tags = new [] { tag };
    }

    public CollisionResponse(Collider collider, params Enum[] tags)
    {
      this.collider = collider;
      this.tags = tags;
    }

    public override void Update()
    {
      base.Update();

      var others = collider.CollideEntities(Entity.X, Entity.Y, tags).OfType<T>();
      if (others.IsNotEmpty())
      {
        OnCollision(others);
      }
    }

    protected abstract void OnCollision(IEnumerable<T> others);

    public override void Render()
    {
      base.Render();

      if (RenderCollider)
      {
        collider.Render();
      }
    }
  }
}
