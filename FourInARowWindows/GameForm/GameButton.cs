using System.Windows.Forms;

namespace FourInARowWindows
{
     public class GameButton : Button
     {
          public const string k_Player1Figure = "X";
          public const string k_Player2Figure = "O";
          public const string k_NullFigure = "";
        
          public GameButton()
          {
            this.Height = 40;
            this.Width = 40;
          }
     }
}
