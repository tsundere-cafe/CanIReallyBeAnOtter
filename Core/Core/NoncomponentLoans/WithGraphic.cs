using Otter;
using System;
using System.Linq;

namespace CanIReallyBeAnOtter.Core.NoncomponentLoans
{
  public class WithGraphic<T> : Component
    where T : Graphic
  {
    public readonly Func<T> graphicGenerator;

    T graphic;

    public WithGraphic(T graphic)
    {
      graphicGenerator = () => graphic;
    }

    public WithGraphic(Func<T> graphic)
    {
      graphicGenerator = graphic;
    }

    public override void Added()
    {
      base.Added();
      graphic = Entity.AddGraphic(graphicGenerator());
    }

    public override void Removed()
    {
      base.Removed();
      Entity.RemoveGraphic(graphic);
    }
  }
}
