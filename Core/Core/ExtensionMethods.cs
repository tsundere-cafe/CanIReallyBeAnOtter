﻿using CanIReallyBeAnOtter.Core.NoncomponentLoans;
using Otter;
using System;

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
}
