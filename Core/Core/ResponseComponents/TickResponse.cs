using Otter;
using System;

namespace CanIReallyBeAnOtter.Core.ResponseComponents
{
  public abstract class TickResponse : Component
  {
    readonly float interval;

    float lastTick = 0.0f;

    public TickResponse(float interval)
    {
      this.interval = interval;
    }

    public override void Added()
    {
      base.Added();

      lastTick = Timer;
    }

    public override void Update()
    {
      base.Update();

      while (Timer > lastTick + interval)
      {
        lastTick += interval;
        OnTick();
      }
    }

    protected abstract void OnTick();
  }
}
