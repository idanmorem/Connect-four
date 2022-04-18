namespace GameEngine
{
     public interface IFourInARow
     {
          void NewRound();
          void InitializePlayer2AndOpponent(string i_ReadLine, string i_PlayerName);
          int SimpleAiLogic();
          GameEngineLogic.eGameStatus CommitTurn(string io_UserChoice);
          Player GetPlayer1();
          Player GetPlayer2();
          Player GetCurrentPlayer();
          int GetLastColMove();
          GameEngineLogic.ePlayerDisk GetMatrixValue(int i_Row, int i_Col);
          int GetNumOfCols();
          int GetNumOfRows();
          Player GetOppositePlayer();
     }
}
