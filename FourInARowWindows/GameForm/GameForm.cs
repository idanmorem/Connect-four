using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GameEngine;

namespace FourInARowWindows
{
     public partial class GameForm : Form
     {
          private readonly IFourInARow r_Engine;
          private readonly List<ActionButton> r_ActionButtons;
          private readonly List<GameButton> r_GameButtons;

          public GameForm(IFourInARow i_Engine)
          {
               r_ActionButtons = new List<ActionButton>();
               r_GameButtons = new List<GameButton>();
               r_Engine = i_Engine;

               for (int i = 0; i < r_Engine.GetNumOfCols(); i++)
               {
                    r_ActionButtons.Add(new ActionButton(i + 1));
               }
               for (int j = 0; j < r_Engine.GetNumOfCols() * r_Engine.GetNumOfRows(); j++)
               {
                    r_GameButtons.Add(new GameButton());
               }

               InitializeComponent();

               gameTable.RowCount = r_Engine.GetNumOfRows() + 1;
               gameTable.ColumnCount = r_Engine.GetNumOfCols();
               this.gameTable.Size = new Size(r_Engine.GetNumOfCols() * 45, r_Engine.GetNumOfRows() * 50);
               this.Size = new Size(gameTable.Size.Width + 50, gameTable.Size.Height + 90);

               foreach (ActionButton button in r_ActionButtons)
               {

                    button.Click += OnActionClicked;
                    gameTable.Controls.Add(button);

               }
               foreach (GameButton button in r_GameButtons)
               {
                    gameTable.Controls.Add(button);
                    button.Enabled = true;

               }
               updatePlayerStampsFromEngine();
               ShowDialog();
          }


          private void updatePlayerStampsFromEngine()
          {
               Player player1 = r_Engine.GetPlayer1();
               Player player2 = r_Engine.GetPlayer2();

               label3.Text = player1.Name + ": " + player1.Score;
               label4.Text = player2.Name + ": " + player2.Score;
          }

          private void OnActionClicked(object i_Sender, EventArgs e)
          {
               ActionButton pressedButton = i_Sender as ActionButton;
               string stringToEngine = pressedButton.Text;
               turn(stringToEngine);
               if (r_Engine.GetCurrentPlayer().IsAnAi == true)
               {
                    stringToEngine = null;
                    turn(stringToEngine);
               }
          }

          private void turn(string i_Input)
          {
               GameEngineLogic.eGameStatus status = r_Engine.CommitTurn(i_Input);
               markFinishedColumnsIfExist();
               drawTable();

               if (status == GameEngineLogic.eGameStatus.Win)
               {
                    gameWon();
               }
               else if (status == GameEngineLogic.eGameStatus.Tie)
               {
                    gameTie();
               }
               if (status != GameEngineLogic.eGameStatus.ContinuePlayingRound)
               {
                    prepareNewGame();
               }
          }

          private void prepareNewGame()
          {
               foreach (ActionButton button in r_ActionButtons)
               {
                    button.Enabled = true;
               }

               r_Engine.NewRound();
               drawTable();
          }

          private void gameTie()
          {
               if (MessageBox.Show("Tie!!" + System.Environment.NewLine + "Another Round?", "A Tie!", MessageBoxButtons.YesNo) == DialogResult.No)
               {
                    this.Close();
               }
          }

          private void gameWon()
          {
               if (MessageBox.Show(r_Engine.GetOppositePlayer().Name + "Won!!" + System.Environment.NewLine + "Another Round ?", "A Win!", MessageBoxButtons.YesNo) == DialogResult.Yes)
               {
                    updatePlayerStampsFromEngine();
               }
               else
               {
                    this.Close();
               }
          }

          private void markFinishedColumnsIfExist()
          {
               if (r_Engine.GetMatrixValue(0, r_Engine.GetLastColMove() - 1) !=
                   (byte)GameEngineLogic.ePlayerDisk.NullValue)
               {
                    r_ActionButtons[r_Engine.GetLastColMove() - 1].Enabled = false;
               }
          }

          private void drawTable()
          {
               for (int i = 0; i < r_Engine.GetNumOfRows(); i++)
               {
                    for (int j = 0; j < r_Engine.GetNumOfCols(); j++)
                    {
                         GameEngineLogic.ePlayerDisk currentDisk = r_Engine.GetMatrixValue(i, j);
                         if (currentDisk == GameEngineLogic.ePlayerDisk.Player1)
                         {
                              r_GameButtons[i * r_Engine.GetNumOfCols() + j].Text = GameButton.k_Player1Figure;
                         }
                         else if (currentDisk == GameEngineLogic.ePlayerDisk.Player2)
                         {
                              r_GameButtons[i * r_Engine.GetNumOfCols() + j].Text = GameButton.k_Player2Figure;
                         }
                         else
                         {
                              r_GameButtons[i * r_Engine.GetNumOfCols() + j].Text = GameButton.k_NullFigure;
                         }
                    }
               }
          }
     }
}
