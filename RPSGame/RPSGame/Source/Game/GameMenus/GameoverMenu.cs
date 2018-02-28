/* 
 * GameoverMenu.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Interface for player at the end of Match
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
 * 
 * */
using System;
using System.Collections.Generic;
using System.Text;
using RPSGame.GameDataBase;
using RPSGame.GameManager;

namespace RPSGame
{
  /// <summary>
  /// Main Screen Menu
  /// </summary>
  class GameoverMenu : GameScreen
  {
    public override void Display()
    {
      base.Display();
      // Might be a return of json for Web API
      Console.WriteLine("- Game Over -");
      Console.WriteLine("-------------");
      Console.WriteLine("The winner is...:");
      Console.WriteLine("");

    }

    /// <summary>
    /// Specific User Input Treatement
    /// </summary>
    /// <param name="gm"></param>
    public override void Update(Game gm)
    {
      Console.WriteLine("Press any key...");
      Console.ReadKey();

      Console.WriteLine("");
      Console.WriteLine("");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");
      int playeronescore = gm.PlayerList[0].ReadWinCounter();
      int playertwoscore = gm.PlayerList[1].ReadWinCounter();

      if (playeronescore == playertwoscore)
      {
        Console.WriteLine("None...(Drawn Match)");
      }
      else
      {
        Console.WriteLine(" "+((playeronescore>playertwoscore)?gm.PlayerList[0].GetName():gm.PlayerList[1].GetName()));
      }
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");

      Console.WriteLine("Press any key...");
      Console.ReadKey();
      gm.RemoveAllPlayer();
      gm.GoToScene("MainMenu");

    }
  }
}
