using System.Windows.Forms;

namespace FourInARowWindows
{
    public class ActionButton : Button
     {
          public ActionButton(int i_Index)
          {
               Text = i_Index.ToString();
               this.Height = 20;
               this.Width = 40;
          }
     }
}