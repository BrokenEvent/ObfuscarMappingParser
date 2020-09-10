using System;
using System.Runtime.CompilerServices;

namespace TestAssembly
{
  internal class PrivateClass1
  {
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Crash()
    {
      throw new Exception("Crash in private class");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Crash(SubClass sc)
    {
      sc.Crash();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CrashDeeper()
    {
      Crash();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CrashSubclass()
    {
      new SubClass().Crash();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CrashSubclassDeeper()
    {
      new SubClass().CrashDeeper();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void CrashSubclassParam()
    {
      Crash(new SubClass());
    }

    private class SubClass
    {
      [MethodImpl(MethodImplOptions.NoInlining)]
      public void Crash()
      {
        throw new Exception("Crash in subclass of private class");
      }

      [MethodImpl(MethodImplOptions.NoInlining)]
      public void CrashDeeper()
      {
        Crash();
      }
    }
  }
}
