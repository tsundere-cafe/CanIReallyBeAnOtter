using Otter;
using System;
using System.Linq;

namespace CanIReallyBeAnOtter.Core.NoncomponentLoans
{
  public class WithCollider<T> : Component
    where T : Collider
  {
    public readonly T collider;

    float disabledUntil;

    public WithCollider(T collider)
    {
      this.collider = collider;
    }

    public void DisableFor(float frames)
    {
      disabledUntil = Math.Max(disabledUntil, Timer + frames);
    }

    public override void Update()
    {
      base.Update();

      collider.Collidable = disabledUntil < Timer;
    }

    public override void Added()
    {
      base.Added();
      Entity.AddCollider(collider);
    }

    public override void Removed()
    {
      base.Removed();
      Entity.RemoveCollider(collider);
    }

    public static implicit operator T(WithCollider<T> loan)
    {
      return loan.collider;
    }
  }
}
