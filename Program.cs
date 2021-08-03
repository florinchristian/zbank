// Decompiled with JetBrains decompiler
// Type: Z_Bank_Final.Program
// Assembly: Z Bank Final, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7535C92C-6010-4363-AB0C-1391C8D89DC6
// Assembly location: C:\Users\Cristi\Documents\zbank\Z Bank\Z Bank\Z Bank Final.exe

using System;
using System.Windows.Forms;

namespace Z_Bank_Final
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
