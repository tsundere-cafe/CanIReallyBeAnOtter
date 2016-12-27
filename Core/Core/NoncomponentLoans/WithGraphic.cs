using Otter;
using System;
using System.Linq;

namespace CanIReallyBeAnOtter.Core.NoncomponentLoans
{
  public class WithGraphic<T> : Component
    where T : Graphic
  {
    public readonly T graphic;

    public WithGraphic(T graphic)
    {
      this.graphic = graphic;
    }

    public override void Added()
    {
      base.Added();
      Entity.AddGraphic(graphic);
    }

    public override void Removed()
    {
      base.Removed();
      Entity.RemoveGraphic(graphic);
    }
  }
}
