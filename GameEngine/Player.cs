namespace GameEngine
{
     public class Player
     {
          private string m_Name;
          private readonly bool m_IsAnAi;
          private int m_Score;

          public string Name
          {
               get => m_Name;
               set => m_Name = value;
          }

          public Player(bool i_IsPlayerAnAi)
          {
               m_IsAnAi = i_IsPlayerAnAi;
               m_Score = 0;
          }

          public bool IsAnAi
          {
               get => m_IsAnAi;
          }

          public int Score
          {
               get => m_Score;
               set => m_Score = value;
          }
     }
}
