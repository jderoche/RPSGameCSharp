/* 
 * IPlayer.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Interface for player
 * 
 * ======================================================================================
 * History:
 * ======================================================================================
 * Name         Date         Description
 * J.Deroche    27/02/2018   Creation
 * 
 * ======================================================================================
 * Notes:
 * ======================================================================================
 * List of move must be setup before get move
 * */
using System;
using System.Collections.Generic;
using System.Text;

using RPSGame.PlayerMgt;
using RPSGame.GameDataBase;
using RPSGame.GameManager;

namespace RPSGame
{
  class PlayMenu : GameScreen
  {
    /// <summary>
    /// Number of match
    /// </summary>
    private const int NumberOfMatch = 5;

    private const int cPlayerOne = 0;
    private const int cPlayerTwo = 1;

    /// <summary>
    /// Match and player counter
    /// </summary>
    private int GameCounter = 0;
    private int PlayerCounter = 0;

    /// <summary>
    /// Move log
    /// </summary>
    private List<GameDB.MoveType> PlayersMove = new List<GameDB.MoveType>();

    public override void Display()
    {
      base.Display();
      // Might be a return of json for Web API
      Console.WriteLine("- Play Menu -");
      Console.WriteLine("-------------");
      Console.WriteLine("");

    }

    /// <summary>
    /// Specific User Input Treatement
    /// </summary>
    /// <param name="gm"></param>
    public override void Update(Game gm)
    {

      // Play until the end of matches
      if (GameCounter < NumberOfMatch)
      {
        // Get each player move
        if (PlayerCounter < gm.PlayerList.Count)
        {

          // Get all players moves
          IPlayer player = gm.PlayerList[PlayerCounter];
          PlayerCounter++;
          PlayersMove.Add(player.GetMove());
        }
        // Find the Match result
        else
        {
          GameCounter++;
          Console.WriteLine("End of Game " + GameCounter.ToString());
          Console.WriteLine(PlayersMove[cPlayerOne] + " Vs " + PlayersMove[cPlayerTwo]);

          // If drawn game
          if (PlayersMove[cPlayerOne] == PlayersMove[cPlayerTwo])
          {
            gm.PlayerList[cPlayerOne].RecordGameResult(PlayersMove[cPlayerTwo], PlayersMove[cPlayerOne], GameDB.MatchResult.eDrawn);
            gm.PlayerList[cPlayerTwo].RecordGameResult(PlayersMove[cPlayerOne], PlayersMove[cPlayerTwo], GameDB.MatchResult.eDrawn);
            Console.WriteLine("Drawn Game...");
            Console.WriteLine("");
          }
          else
          {
            bool bPlayerTwoLoss = false;

            // Find if the opponent choose a lower move
            List<GameDB.MoveType> listmove = GameDB.MoveTable[PlayersMove[0]];
            foreach (GameDB.MoveType mt in listmove)
            {
              // Opponent move is in the list => Player 0 win
              if (mt == PlayersMove[1])
              {
                bPlayerTwoLoss = true;
                break;
              }
            }

            if (bPlayerTwoLoss)
            {
              gm.PlayerList[cPlayerOne].RecordGameResult(PlayersMove[cPlayerTwo], PlayersMove[cPlayerOne], GameDB.MatchResult.eWin);
              gm.PlayerList[cPlayerTwo].RecordGameResult(PlayersMove[cPlayerOne], PlayersMove[cPlayerTwo], GameDB.MatchResult.eLoss);

              Console.WriteLine("Player"+gm.PlayerList[cPlayerOne].GetName()+" Win...");
              Console.WriteLine("");
            }
            else
            {
              gm.PlayerList[cPlayerOne].RecordGameResult(PlayersMove[cPlayerTwo], PlayersMove[cPlayerOne], GameDB.MatchResult.eLoss);
              gm.PlayerList[cPlayerTwo].RecordGameResult(PlayersMove[cPlayerOne], PlayersMove[cPlayerTwo], GameDB.MatchResult.eWin);
              Console.WriteLine("");
              Console.WriteLine("Player" + gm.PlayerList[cPlayerTwo].GetName() + " Win...");
              Console.WriteLine("");
            }
          }


          Console.WriteLine("Press any key to continue...");
          Console.ReadKey();
          // Reset Player counter
          PlayerCounter = 0;
          
          PlayersMove.Clear();
        }

      }
      else
      {
        Console.WriteLine("End of Match ");
        Console.WriteLine("");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        GameCounter = 0;
        gm.GoToScene("GameoverMenu");
      }

    }
  }
}
