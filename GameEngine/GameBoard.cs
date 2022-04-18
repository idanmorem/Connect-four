namespace GameEngine
{
     public class GameBoard
     {
          //[Rows, Cols]
          private readonly GameEngineLogic.ePlayerDisk[,] r_GameBoardMatrix;
          public GameEngineLogic.ePlayerDisk[,] GameBoardMatrix => r_GameBoardMatrix;
          private byte m_CellsFilled;
          private readonly int m_NumOfCols;
          private readonly int m_NumOfRows;

          public int NumOfCols => m_NumOfCols;

          public int NumOfRows => m_NumOfRows;

          public GameBoard(string i_StrNumOfRows, string i_StrNumOfCols)
          {
               //checks numbers were entered
               int.TryParse(i_StrNumOfRows, out m_NumOfRows);
               int.TryParse(i_StrNumOfCols, out m_NumOfCols);

               //initializes tables with 0s of the requested amount of rows and columns
               r_GameBoardMatrix = new GameEngineLogic.ePlayerDisk[m_NumOfRows, m_NumOfCols];
          }

          public void AddDisk(int i_UserChoice, GameEngineLogic.ePlayerDisk i_NewValue)
          {
               for (int i = m_NumOfRows - 1; i >= 0; i--)
               {
                    if (r_GameBoardMatrix[i, i_UserChoice - 1] == GameEngineLogic.ePlayerDisk.NullValue)
                    {
                         r_GameBoardMatrix[i, i_UserChoice - 1] = i_NewValue;
                         break;
                    }
               }

               m_CellsFilled++;
          }

          public GameEngineLogic.eGameStatus CheckGameStatus(GameEngineLogic.ePlayerDisk i_CurrentPlayer)
          {
               GameEngineLogic.eGameStatus gameStatus = GameEngineLogic.eGameStatus.ContinuePlayingRound;

               if (m_CellsFilled == (m_NumOfCols * m_NumOfRows))
               {
                    gameStatus = GameEngineLogic.eGameStatus.Tie;
               }
               else
               {
                    verticalCheck(ref gameStatus, i_CurrentPlayer);
                    horizontalCheck(ref gameStatus, i_CurrentPlayer);
                    ascendingDiagonalCheck(ref gameStatus, i_CurrentPlayer);
                    descendingDiagonalCheck(ref gameStatus, i_CurrentPlayer);
               }

               return gameStatus;
          }

          private void verticalCheck(ref GameEngineLogic.eGameStatus i_Status, GameEngineLogic.ePlayerDisk i_CurrentPlayer)
          {
               for (int j = 0; j < m_NumOfCols; j++)
               {
                    for (int i = 0; i < m_NumOfRows - 3; i++)
                    {
                         if ((r_GameBoardMatrix[i, j] == i_CurrentPlayer) && (r_GameBoardMatrix[i + 1, j] == i_CurrentPlayer)
                              && (r_GameBoardMatrix[i + 2, j] == i_CurrentPlayer) && (r_GameBoardMatrix[i + 3, j] == i_CurrentPlayer))
                         {
                              i_Status = GameEngineLogic.eGameStatus.Win;
                         }
                    }
               }
          }

          private void horizontalCheck(ref GameEngineLogic.eGameStatus i_Status, GameEngineLogic.ePlayerDisk i_CurrentPlayer)
          {
               for (int j = 0; j < m_NumOfCols - 3; j++)
               {
                    for (int i = 0; i < m_NumOfRows; i++)
                    {
                         if ((r_GameBoardMatrix[i, j] == i_CurrentPlayer) && (r_GameBoardMatrix[i, j + 1] == i_CurrentPlayer)
                              && (r_GameBoardMatrix[i, j + 2] == i_CurrentPlayer) && (r_GameBoardMatrix[i, j + 3] == i_CurrentPlayer))
                         {
                              i_Status = GameEngineLogic.eGameStatus.Win;
                         }
                    }
               }
          }

          private void ascendingDiagonalCheck(ref GameEngineLogic.eGameStatus i_Status, GameEngineLogic.ePlayerDisk i_CurrentPlayer)
          {
               for (int j = 0; j < m_NumOfCols - 3; j++)
               {
                    for (int i = 3; i < m_NumOfRows; i++)
                    {
                         if ((r_GameBoardMatrix[i, j] == i_CurrentPlayer) && (r_GameBoardMatrix[i - 1, j + 1] == i_CurrentPlayer)
                              && (r_GameBoardMatrix[i - 2, j + 2] == i_CurrentPlayer) && (r_GameBoardMatrix[i - 3, j + 3] == i_CurrentPlayer))
                         {
                              i_Status = GameEngineLogic.eGameStatus.Win;
                         }
                    }
               }
          }

          private void descendingDiagonalCheck(ref GameEngineLogic.eGameStatus i_Status, GameEngineLogic.ePlayerDisk i_CurrentPlayer)
          {
               for (int j = 0; j < m_NumOfCols - 3; j++)
               {
                    for (int i = 0; i < m_NumOfRows - 3; i++)
                    {
                         if ((r_GameBoardMatrix[i, j] == i_CurrentPlayer) && (r_GameBoardMatrix[i + 1, j + 1] == i_CurrentPlayer)
                              && (r_GameBoardMatrix[i + 2, j + 2] == i_CurrentPlayer) && (r_GameBoardMatrix[i + 3, j + 3] == i_CurrentPlayer))
                         {
                              i_Status = GameEngineLogic.eGameStatus.Win;
                         }
                    }
               }
          }

          public void Clear()
          {
               for (int i = 0; i < m_NumOfRows; i++)
               {
                    for (int j = 0; j < m_NumOfCols; j++)
                    {
                         r_GameBoardMatrix[i, j] = GameEngineLogic.ePlayerDisk.NullValue;
                    }
               }

               m_CellsFilled = 0;
          }
     }
}
