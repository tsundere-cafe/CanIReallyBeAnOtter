using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AnonymousResponses
{
  public class WhenEntered : Component
  {
    Action action;

    public WhenEntered(Action action)
    {
      this.action = action;
    }

    public override void Added()
    {
      base.Added();

      action();
    }
  }
}
