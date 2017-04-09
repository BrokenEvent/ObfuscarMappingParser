using System.Drawing;
using System.Windows.Forms;

namespace ObfuscarMappingParser
{
  public class MyListView: ListView
  {

    /// <summary>
    /// http://stackoverflow.com/questions/3255046/disallow-listview-to-have-zero-selected-items
    /// </summary>
    /// <param name="m"></param>
    protected override void WndProc(ref Message m)
    {
      // Swallow mouse messages that are not in the client area
      if (m.Msg >= 0x201 && m.Msg <= 0x209)
      {
        Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
        var hit = this.HitTest(pos);
        switch (hit.Location)
        {
          case ListViewHitTestLocations.AboveClientArea:
          case ListViewHitTestLocations.BelowClientArea:
          case ListViewHitTestLocations.LeftOfClientArea:
          case ListViewHitTestLocations.RightOfClientArea:
          case ListViewHitTestLocations.None:
            return;
        }
      }
      base.WndProc(ref m);
    }
  }
}
