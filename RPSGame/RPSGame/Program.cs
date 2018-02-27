/* 
 * Program.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Entry point and main loop of the game
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
 * in debug mode : all the class were defined in this file in order to make quick test
 * and prototype new fonctionnalies
 * */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RPSGame;
using RPSGame.GameDataBase;
using RPSGame.GameManager;


namespace RPSGame
{
  /// <summary>
  /// Main Screen Menu
  /// </summary>
  class MainMenu : GameScreen
  {
    public override void Display()
    {
      base.Display();
      // Might be a return of json for Web API
      Console.WriteLine("- Main Menu -");
      Console.WriteLine("-------------");
      Console.WriteLine("");
      Console.WriteLine("P) Play");
      Console.WriteLine("E) Exit");
      Console.WriteLine("");

    }

    /// <summary>
    /// Specific User Input Treatement
    /// </summary>
    /// <param name="gm"></param>
    public override void Update(Game gm)
    {

      char key = Console.ReadKey().KeyChar;
      if ( (key == 'E') || (key == 'e') )
        gm.ExitGame();

      if ( (key == 'P') || (key == 'p') )
        gm.GoToScene("PlayMenu");
    }
  }

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
      char key = Console.ReadKey().KeyChar;

      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("Player: ");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");

      Console.WriteLine("Press any key...");
      gm.GoToScene("MainMenu");

    }
  }

  class PlayMenu : GameScreen
  {
    /// <summary>
    /// Number of match
    /// </summary>
    private const int NumberOfMatch = 5;

    /// <summary>
    /// Match and player counter
    /// </summary>
    private int MatchCounter = 0;
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
      if (MatchCounter < NumberOfMatch)
      {

        if (PlayerCounter < gm.PlayerList.Count)
        {

          // Get all players moves
          PlayerMgt.IPlayer player = gm.PlayerList[PlayerCounter];
          PlayerCounter++;
          PlayersMove.Add(player.GetMove());
        }
        else
        {
          MatchCounter++;
          Console.WriteLine("End of Match " + MatchCounter.ToString());
          Console.WriteLine("");
          Console.WriteLine("Press any key to continue...");
          Console.ReadKey();
          // Reset Player counter
          PlayerCounter = 0;
        }

      }
      else
      {
        Console.WriteLine("End of Game " + MatchCounter.ToString());
        Console.WriteLine("");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        gm.GoToScene("GameoverMenu");
      }

    }
  }

  class HumanPlayer : PlayerMgt.Player
  {

    public HumanPlayer(string name)
      : base(name)
    {
    }

    public override GameDB.MoveType GetMove()
    {
      GameDB.MoveType res = (GameDB.MoveType)0;

      Console.WriteLine("Player : " + this.GetName());
      Console.WriteLine("");
      Console.WriteLine("Choose your move : ");
      Console.WriteLine("");
      int keyindex = 0;

      // Show list of move to player
      foreach (GameDB.MoveType mv in this.GetPlayerMoves())
      {
        // Increase index 1 => Move1 2 => Move2 so and so
        keyindex++;

        // Add separator before excepted for the first one XX / YY / ZZ
        if (keyindex>1)
          Console.Write(" / ");
        Console.Write(keyindex.ToString() + " => " + mv.ToString());
      }
      Console.WriteLine("");

      bool validinput = false;

      while (validinput == false)
      {
        char input = Console.ReadKey().KeyChar;
        int value = (input - '0');
        if ((value > 0) && (value <= keyindex))
        {
          validinput = true;
          res = (GameDB.MoveType)value;
        }
      }

      return res;
    }
  }

  class BasicCPU : PlayerMgt.Player
  {

    public BasicCPU(string name)
      : base(name)
    {
    }


    public override GameDB.MoveType GetMove()
    {
      GameDB.MoveType res = (GameDB.MoveType)0;

      Console.WriteLine("CPU : " + this.GetName());
      int keyindex = 0;
      foreach (GameDB.MoveType mv in this.GetPlayerMoves())
      {
        keyindex++;
      }
      
      int value = new Random().Next(1, keyindex);
      if ((value > 0) && (value <= keyindex))
      {
        res = (GameDB.MoveType)value;
      }


      return res;
    }
  }

  class Program
  {
    static bool GameRunning = true;

    static void Main(string[] args)
    {
      // Instanciate Game Screen
      MainMenu mainmenu = new MainMenu();
      PlayMenu playmenu = new PlayMenu();
      GameoverMenu gameovermenu = new GameoverMenu();

      //instanciate player
      HumanPlayer human = new HumanPlayer("Player one");
      BasicCPU basiccpu = new BasicCPU("Player Two");

      Game Game_T = new Game();

      Game_T.AddPlayer(human);
      Game_T.AddPlayer(basiccpu);

      Game_T.AddScene("MainMenu", mainmenu);
      Game_T.AddScene("PlayMenu", playmenu);
      Game_T.AddScene("GameoverMenu", gameovermenu);
      Game_T.OnSceneEntry += new Game.SceneEntry(Game_T_OnSceneEntry);
      Game_T.OnExit += new Game.Exit(Game_T_OnExit);
      Game_T.GoToScene("MainMenu");
      while (GameRunning)
      {
        Game_T.SceneUpdate();
      }

      return;
    }

    static void Game_T_OnExit()
    {
      GameRunning = false;
    }

    static void Game_T_OnSceneEntry(string scenename, Game gm)
    {
      if (scenename.ToLower() == "playmenu")
      {
        // Prepare the list of player moves
        List<GameDB.MoveType> PlayerMoves = new List<GameDB.MoveType>() 
        {
          GameDB.MoveType.ePaper,
          GameDB.MoveType.eRock,
          GameDB.MoveType.eScissor
        };

        foreach (PlayerMgt.IPlayer p in gm.PlayerList)
        {
          p.ConfigureMove(PlayerMoves);
        }
      }
    }
  }
}
