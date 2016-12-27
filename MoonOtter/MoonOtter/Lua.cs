using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace MoonOtter
{
    /// <summary>
    /// A coroutine interface to MoonSharp
    /// </summary>
    public class Lua
  {
    static Lua instance;
    public static Lua Instance
    {
      get { return instance = instance ?? new Lua(); }
    }

    Script script;

    private Lua()
    {
      script = new Script();

      // basically equivalent to Coroutine.Start but in lua because of some EnumerableWrapper hijinks
      script.DoString(@"
        function run(iterator)
          for current in iterator do
            if (type(current) == ""userdata"") then
              run(current)
            else
              coroutine.yield(current)
            end
          end
        end
      ");

      foreach (var method in LuaRunnableAttribute.GetAllMethods())
      {
        script.Globals[method.Name] = method;
      }
    }

    [LuaRunnable]
    public static IEnumerator Bark(int times)
    {
      Console.WriteLine(string.Join(" ", Enumerable.Repeat("BARK", times).ToArray()) + "!!!");
      yield break;
    }

    /// <summary>
    /// Gets a coroutine that executes a lua script as a coroutine
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public IEnumerator Load(string filename, Action<ScriptRuntimeException> onError = null)
    {
      return LoadSafely(LoadInternal(filename), filename, onError);
    }

    IEnumerator LoadSafely(IEnumerator inner, string filename, Action<ScriptRuntimeException> onError)
    {
      bool moveNext;

      do
      {
        try
        {
          moveNext = inner.MoveNext();
        }
        catch (ScriptRuntimeException e)
        {
          moveNext = false;
          Console.WriteLine("Exception in " + filename + ":\n" + e.DecoratedMessage);
          if (onError != null)
          {
            onError(e);
          }
        }

        if (moveNext)
        {
          yield return inner.Current;
        }
      } while (moveNext);
    }

    IEnumerator LoadInternal(string filename)
    {
      filename = Path.Combine("Scripts", filename);

      if (File.Exists(filename))
      {
        var function = script.DoString("return function()\n" + File.ReadAllText(filename) + "\nend");
        var coroutine = script.CreateCoroutine(function);
        foreach (var current in coroutine.Coroutine.AsTypedEnumerable())
        {
          if (current.Type == DataType.Void)
          {
            yield break;
          }
          else
          {
            yield return current.ToObject();
          }
        }
      }
      else
      {
        Console.WriteLine("Trying to run a missing script! " + filename);
      }
    }
  }
}
