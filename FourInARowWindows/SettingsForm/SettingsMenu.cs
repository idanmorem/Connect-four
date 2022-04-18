using System;
using System.Text;
using System.Windows.Forms;
using GameEngine;


namespace FourInARowWindows
{
     public partial class SettingsMenu : Form
     {
          private const string k_ComputerTextBoxName = "[Computer]";
          private const string k_ErrorFormTitle = "Error log:";
          private const string k_ComputerDisplayName = "Computer";
          private const string k_IsAiOpponent = "1";
          private const string k_IsHumanOpponent = "2";

          public SettingsMenu()
          {
               InitializeComponent();
          }

          private void OnOpponentChanged(object i_Sender, EventArgs e)
          {
               CheckBox checkBox = i_Sender as CheckBox;

               if (checkBox.Checked == true)
               {
                    textInputPlayer2Name.Clear();
                    textInputPlayer2Name.Enabled = true;
               }
               else
               {
                    textInputPlayer2Name.Enabled = false;
                    textInputPlayer2Name.Text = k_ComputerTextBoxName;
               }
          }

          private void OnStartClicked(object i_Sender, EventArgs e)
          {
               StringBuilder errorStringForm = new StringBuilder(k_ErrorFormTitle + Environment.NewLine);
               bool errorAtForm = validateForm(errorStringForm);

               if (errorAtForm == false)
               {
                    IFourInARow engine = new GameEngineLogic(NUDRows.Text, NUDCols.Text);
                    engine.GetPlayer1().Name = TextInputPlayer1Name.Text;
                    string userOpponentChoice = this.checkBoxPlayerTwo.Checked ? k_IsAiOpponent : k_IsHumanOpponent;
                    engine.InitializePlayer2AndOpponent(userOpponentChoice, textInputPlayer2Name.Text);
                    this.Hide();
                    openGameForm(engine);
               }
               else // errorAtForm == true
               {
                    MessageBox.Show(errorStringForm.ToString());
               }
          }

          private bool validateForm(StringBuilder io_SystemOutPutToUserBuilder)
          {
               bool errorAtForm = false;

               if (TextInputPlayer1Name.Text == "")
               {
                    io_SystemOutPutToUserBuilder.Append("Please make sure you enter a name for player 1" + Environment.NewLine);
                    errorAtForm = true;
               }

               if (textInputPlayer2Name.Text == "")
               {
                    io_SystemOutPutToUserBuilder.Append("Please make sure you enter a name for player 2" + Environment.NewLine);
                    errorAtForm = true;
               }

               return errorAtForm;
          }


          private void openGameForm(IFourInARow i_Engine)
          {
               if (i_Engine.GetPlayer2().IsAnAi == true)
               {
                    i_Engine.GetPlayer2().Name = k_ComputerDisplayName;
               }

               GameForm gameForm = new GameForm(i_Engine);
               this.Close();
          }
     }
}
