using System;
using System.Runtime.CompilerServices;

namespace TestAssembly
{
  public class PublicClass1
  {
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PublicCrash()
    {
      throw new Exception("Crash in public non-obfuscated method");
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PrivateCrash()
    {
      new PrivateClass1().Crash();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PrivateCrashDeeper()
    {
      new PrivateClass1().CrashDeeper();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PrivateCrashSubclass()
    {
      new PrivateClass1().CrashSubclass();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PrivateCrashSubclassParam()
    {
      new PrivateClass1().CrashSubclassParam();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PrivateCrashSubclassDeeper()
    {
      new PrivateClass1().CrashSubclassDeeper();
    }
  }
}
