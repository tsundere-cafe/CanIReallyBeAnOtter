using Otter;
using System.Collections;

namespace Core
{
  public class EffectEntity : Entity
  {
    public EffectEntity(int x, int y, params Component[] components)
      : base(x, y)
    {
      AddComponents(components);
    }

    public EffectEntity(int x, int y, float lifespan, params Component[] components)
      : base(x, y)
    {
      LifeSpan = lifespan;
      AddComponents(components);
    }

    public IEnumerator WaitForFinish()
    {
      while (Scene != null)
      {
        yield return null;
      }
    }
  }
}
