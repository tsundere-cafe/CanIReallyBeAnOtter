using CanIReallyBeAnOtter.Core.NoncomponentLoans;
using Otter;
using System;
using System.Collections;

/// <summary>
/// No namespace extension method is.....rude!!!!
/// </summary>
public static class ExtensionMethods
{
  public static void SetFloorOrigin(this Graphic graphic)
  {
    graphic.OriginX = graphic.Width / 2;
    graphic.OriginY = graphic.Height;
  }

  public static void SetCeilingOrigin(this Graphic graphic)
  {
    graphic.OriginX = graphic.Width / 2;
    graphic.OriginY = 0;
  }

  public static void SetFloorOrigin(this Collider collider)
  {
    collider.OriginX = collider.Width / 2;
    collider.OriginY = collider.Height;
  }

  public static void SetCeilingOrigin(this Collider collider)
  {
    collider.OriginX = collider.Width / 2;
    collider.OriginY = 0;
  }

  public static T Configure<T>(this T thing, Action<T> configuration)
  {
    configuration(thing);
    return thing;
  }

  public static int Run(this IEnumerator coroutine)
  {
    return Coroutine.Instance.Start(coroutine);
  }

  public static string JoinString<T>(this IEnumerable<T> collection, string joinWith = ", ")
  {
    if (collection.IsEmpty())
    {
      return "";
    }
    else
    {
      var result = new StringBuilder();

      foreach (var thing in collection)
      {
        result.Append(thing.ToString());
        result.Append(joinWith);
      }

      return result.Remove(result.Length - joinWith.Length, joinWith.Length).ToString();
    }
  }
}
