using System;

namespace GameEngine
{
     public class GameEngineLogic : IFourInARow
     {
          private readonly GameBoard r_GameBoard;
          private const string k_AiOpponent = "2";
          private const string k_RealOpponent = "1";
          private int m_LastColColMove;
          private Player m_Player1;
          private Player m_Player2;
          private bool m_IsCurrentPlayer1;
          private Player m_CurrentOpponent;
          private Player m_CurrentPlayer;

          public GameEngineLogic.ePlayerDisk GetMatrixValue(int i_Row, int i_Col)
          {
               return r_GameBoard.GameBoardMatrix[i_Row, i_Col];
          }

          public int LastColMove
          {
               get => m_LastColColMove;
               set => m_LastColColMove = value;
          }

          public int GetLastColMove()
          {
               return LastColMove;
          }

          public int GetNumOfCols()
          {
               return r_GameBoard.NumOfCols;
          }

          public int GetNumOfRows()
          {
               return r_GameBoard.NumOfRows;
          }

          public enum eGameStatus
          {
               ContinuePlayingRound,
               Tie,
               Win,
          }

          public enum ePlayerDisk
          {
               NullValue,
               Player1,
               Player2
          }

          public GameEngineLogic(string i_RowsInTable, string i_ColsInTable)
          {
               r_GameBoard = new GameBoard(i_RowsInTable, i_ColsInTable);
               Player1 = new Player(false); //fields are initialized to not AI, score - 0
               isCurrentPlayer1 = true;
               CurrentPlayer = Player1;
               CurrentOpponent = Player2;
          }

          //assumes no turns have been made into the game yet, a
          public void InitializePlayer2AndOpponent(string i_ReadLine, string i_PlayerName)
          {
               if (i_ReadLine == k_AiOpponent)
               {
                    Player2 = new Player(true);
               }
               else if (i_ReadLine == k_RealOpponent)
               {
                    Player2 = new Player(false);
               }
               else //then error, throw it
               {
                    throw new Exception("You may only enter 1 for an AI or 2 for a real opponent");
               }

               Player2.Name = i_PlayerName;
               CurrentOpponent = Player2;
          }

          public eGameStatus CommitTurn(string io_UserChoice)
          {
               int userChoice = 0;
               ePlayerDisk currentPlayer = ePlayerDisk.Player1;
               eGameStatus currentStatus = eGameStatus.ContinuePlayingRound;

               if (io_UserChoice != null)
               {
                    userChoice = int.Parse(io_UserChoice);
                    LastColMove = userChoice;
               }

               if (this.isCurrentPlayer1)
               {
                    r_GameBoard.AddDisk(userChoice, ePlayerDisk.Player1);
               }
               else
               {
                    r_GameBoard.AddDisk(Player2.IsAnAi ? SimpleAiLogic() : userChoice, ePlayerDisk.Player2);
                    currentPlayer = GameEngineLogic.ePlayerDisk.Player2;
               }

               currentStatus = r_GameBoard.CheckGameStatus(currentPlayer);

               if (currentStatus == eGameStatus.Win)
               {
                    increaseScoreToPlayer(CurrentPlayer);
               }

               SwitchToOtherPlayer();
               return currentStatus;
          }

          public int SimpleAiLogic()
          {
               int lastMoveForAI = new Random().Next(1, r_GameBoard.NumOfCols + 1);

               while (r_GameBoard.GameBoardMatrix[0, lastMoveForAI - 1] != GameEngineLogic.ePlayerDisk.NullValue)
               {
                    lastMoveForAI = new Random().Next(1, r_GameBoard.NumOfCols + 1);
               }

               return lastMoveForAI;
          }

          private void increaseScoreToPlayer(Player o_Player)
          {
               ++o_Player.Score;
          }

          public GameBoard GameBoard
          {
               get => r_GameBoard;
          }

          private Player Player1
          {
               get => m_Player1;
               set => m_Player1 = value;
          } //fields are initialized to not AI, score - 0

          public Player GetPlayer1()
          {
               return Player1;
          }

          public Player GetPlayer2()
          {
               return Player2;
          }

          public Player Player2
          {
               get => m_Player2;
               set => m_Player2 = value;
          }

          public Player CurrentPlayer
          {
               get => m_CurrentPlayer;
               set => m_CurrentPlayer = value;
          }

          public Player GetCurrentPlayer()
          {
               return CurrentPlayer;
          }

          public Player GetOppositePlayer()
          {
               return CurrentOpponent;
          }

          public Player CurrentOpponent
          {
               get => m_CurrentOpponent;
               set => m_CurrentOpponent = value;
          }

          public bool isCurrentPlayer1
          {
               get => m_IsCurrentPlayer1;
               set => m_IsCurrentPlayer1 = value;
          }

          public bool isPlayer1()
          {
               return isCurrentPlayer1;
          }

          public void SwitchToOtherPlayer()
          {
               isCurrentPlayer1 = !isCurrentPlayer1;

               if (CurrentPlayer == Player1)
               {
                    CurrentPlayer = Player2;
                    CurrentOpponent = Player1;
               }
               else
               {
                    CurrentPlayer = Player1;
                    CurrentOpponent = Player2;
               }
          }

          public void NewRound()
          {
               r_GameBoard.Clear();
          }
     }
}
