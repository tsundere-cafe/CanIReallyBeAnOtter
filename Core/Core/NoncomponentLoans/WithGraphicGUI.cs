using Otter;
using System;
using System.Linq;

namespace CanIReallyBeAnOtter.Core.NoncomponentLoans
{
  public class WithGraphicGUI<T> : Component
    where T : Graphic
  {
    public readonly T graphic;

    public WithGraphicGUI(T graphic)
    {
      this.graphic = graphic;
    }

    public override void Added()
    {
      base.Added();
      Entity.AddGraphicGUI(graphic);
    }

    public override void Removed()
    {
      base.Removed();
      Entity.RemoveGraphic(graphic);
    }
  }
}
