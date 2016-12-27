using Otter;
using System;

namespace CanIReallyBeAnOtter.Core.ResponseComponents
{
  public abstract class RestResponse : Component
  {
    readonly float restThresholdSquared;
    readonly Collider collider;
    readonly Enum[] solid;

    float lastX = float.NaN;
    float lastY = float.NaN;

    public RestResponse(float restThreshold, Collider collider, Enum solid)
    {
      this.restThresholdSquared = restThreshold * restThreshold;
      this.collider = collider;
      this.solid = new [] { solid };
    }

    public RestResponse(float restThreshold, Collider collider, Enum[] solid)
    {
      this.restThresholdSquared = restThreshold * restThreshold;
      this.collider = collider;
      this.solid = solid;
    }

    public override void Added()
    {
      base.Added();
      lastX = float.NaN;
      lastY = float.NaN;
    }

    public override void Update()
    {
      base.Update();

      if (!float.IsNaN(lastX))
      {
        var deltaX = Entity.X - lastX;
        var deltaY = Entity.Y - lastY;

        // if we aren't moving and we're grounded
        if (deltaX * deltaX + deltaY * deltaY < restThresholdSquared && collider.Collide(collider.X, collider.Y + 1.0f, solid) != null)
        {
          OnRest();
        }
      }

      lastX = Entity.X;
      lastY = Entity.Y;
    }

    protected abstract void OnRest();
  }
}
