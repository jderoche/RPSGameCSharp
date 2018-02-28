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
 * in debug mode : you need to write all the class in this file in order to make quick test
 * and prototype new fonctionnalities
 * */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RPSGame;
using RPSGame.GameDataBase;
using RPSGame.GameManager;
using RPSGame.PlayerMgt;

namespace RPSGameApplication
{
#if DEBUG
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
        Console.WriteLine(" " + ((playeronescore > playertwoscore) ? gm.PlayerList[0].GetName() : gm.PlayerList[1].GetName()));
      }
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~");

      Console.WriteLine("Press any key...");
      Console.ReadKey();
      gm.RemoveAllPlayer();
      gm.GoToScene("MainMenu");

    }
  }

  class MainMenu : GameScreen
  {
    public override void Display()
    {
      base.Display();
      // Might be a return of json for Web API
      Console.WriteLine("- Main Menu -");
      Console.WriteLine("-------------");
      Console.WriteLine("");
      Console.WriteLine("1) Play (Human Vs Human)");
      Console.WriteLine("2) Play (Human Vs Basic CPU)");
      Console.WriteLine("3) Play (Human Vs Advanced CPU)");
      Console.WriteLine("4) Play (Basic CPU vs Advanced CPU)");
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
      if ((key == 'E') || (key == 'e'))
        gm.ExitGame();
      else
      {
        // Add player type according to user selection
        switch (key)
        {
          // Player vs Player
          case '1':
            {
              //instanciate player
              HumanPlayer Player1 = new HumanPlayer("Player one");
              HumanPlayer Player2 = new HumanPlayer("Player Two");

              // Add Players to the Game Manager
              gm.AddPlayer(Player1);
              gm.AddPlayer(Player2);
              gm.GoToScene("PlayMenu");
            }
            break;

          // Player vs Basic CPU
          case '2':
            {
              //instanciate player
              HumanPlayer Player1 = new HumanPlayer("Player one");
              BasicCPU Player2 = new BasicCPU("Player Two");

              // Add Players to the Game Manager
              gm.AddPlayer(Player1);
              gm.AddPlayer(Player2);
              gm.GoToScene("PlayMenu");
            }
            break;

          // Player vs Advanced CPU
          case '3':
            {
              //instanciate player
              HumanPlayer Player1 = new HumanPlayer("Player one");
              AdvanceCPU Player2 = new AdvanceCPU("Player Two");

              // Add Players to the Game Manager
              gm.AddPlayer(Player1);
              gm.AddPlayer(Player2);
              gm.GoToScene("PlayMenu");
            }
            break;

          // Basic CPU vs Advanced CPU
          case '4':
            {
              //instanciate player
              BasicCPU Player1 = new BasicCPU("Player one");
              AdvanceCPU Player2 = new AdvanceCPU("Player Two");

              // Add Players to the Game Manager
              gm.AddPlayer(Player1);
              gm.AddPlayer(Player2);
              gm.GoToScene("PlayMenu");
            }
            break;
        }
      }
    }
  }

  class PlayMenu : GameScreen
  {
    /// <summary>
    /// Number of match
    /// </summary>
    private const int NumberOfMatch = 3;

    /// <summary>
    /// Player index
    /// </summary>
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

    /// <summary>
    /// Main Display
    /// </summary>
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
            Console.WriteLine("");
            Console.WriteLine(" Drawn Match ");
            Console.WriteLine("");
          }
          else
          {
            bool bPlayerTwoLoss = false;

            // Find if the opponent choose a lower move
            List<GameDB.MoveType> listmove = GameDB.MoveTable[PlayersMove[0]];
            foreach (GameDB.MoveType mt in listmove)
            {
              // Opponent move is in the list => Player One win
              if (mt == PlayersMove[1])
              {
                bPlayerTwoLoss = true;
                break;
              }
            }

            // If player two loss
            if (bPlayerTwoLoss)
            {
              gm.PlayerList[cPlayerOne].RecordGameResult(PlayersMove[cPlayerTwo], PlayersMove[cPlayerOne], GameDB.MatchResult.eWin);
              gm.PlayerList[cPlayerTwo].RecordGameResult(PlayersMove[cPlayerOne], PlayersMove[cPlayerTwo], GameDB.MatchResult.eLoss);

              Console.WriteLine("");
              Console.WriteLine(gm.PlayerList[cPlayerOne].GetName() + " Win...");
              Console.WriteLine("");
            }
            // Other it's player one
            else
            {
              gm.PlayerList[cPlayerOne].RecordGameResult(PlayersMove[cPlayerTwo], PlayersMove[cPlayerOne], GameDB.MatchResult.eLoss);
              gm.PlayerList[cPlayerTwo].RecordGameResult(PlayersMove[cPlayerOne], PlayersMove[cPlayerTwo], GameDB.MatchResult.eWin);
              Console.WriteLine("");
              Console.WriteLine(gm.PlayerList[cPlayerTwo].GetName() + " Win...");
              Console.WriteLine("");
            }
          }


          Console.WriteLine("Press any key to continue...");
          Console.ReadKey();
          // Reset Player counter for next game
          PlayerCounter = 0;
          // Clear previous move list
          PlayersMove.Clear();
        }

      }
      // Number of max game reached => End of the Match
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

  class HumanPlayer : Player
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
        if (keyindex > 1)
          Console.Write(" / ");
        Console.Write(keyindex.ToString() + " => " + mv.ToString());
      }
      Console.WriteLine("");

      bool validinput = false;

      while (validinput == false)
      {
        char input = Console.ReadKey().KeyChar;
        int value = (input - '0') - 1;
        if ((value >= 0) && (value <= keyindex))
        {
          validinput = true;
          res = (GameDB.MoveType)value;
        }
      }

      return res;
    }
  }

  class BasicCPU : Player
  {
    /// <summary>
    /// Call default constructor
    /// </summary>
    /// <param name="name"></param>
    public BasicCPU(string name)
      : base(name)
    {
    }

    /// <summary>
    /// Move strategy : Random
    /// </summary>
    /// <returns></returns>
    public override GameDB.MoveType GetMove()
    {
      GameDB.MoveType res = (GameDB.MoveType)0;

      Console.WriteLine("CPU : " + this.GetName());

      // Get the randome move from 1 to PlayerMove list count
      int value = new Random().Next(1, this.GetPlayerMoves().Count);
      res = (GameDB.MoveType)value;


      return res;
    }
  }

  class AdvanceCPU : Player
  {
    /// <summary>
    /// Call default constructor
    /// </summary>
    /// <param name="name"></param>
    public AdvanceCPU(string name)
      : base(name)
    {
    }

    /// <summary>
    /// Move strategy : Random
    /// </summary>
    /// <returns></returns>
    public override GameDB.MoveType GetMove()
    {
      GameDB.MoveType res = (GameDB.MoveType)0;

      Console.WriteLine("CPU : " + this.GetName());

      bool FoundCpuMove = false;

      if (MatchResultHistory.Count > 0)
      {
        GameDB.MoveType prvMove = OpponentHistory[OpponentHistory.Count - 1];
        foreach (KeyValuePair<GameDB.MoveType, List<GameDB.MoveType>> listMove in GameDB.MoveTable)
        {
          foreach (GameDB.MoveType mvt in GameDB.MoveTable[listMove.Key])
          {
            if (mvt == prvMove)
            {
              res = listMove.Key;
              FoundCpuMove = true;
              break;
            }
          }
          if (FoundCpuMove)
            break;
        }
      }
      else
      {
        // Get the randome move from 1 to PlayerMove list count
        int value = new Random().Next(1, this.GetPlayerMoves().Count);
        res = (GameDB.MoveType)value;
      }

      return res;
    }
  }
#endif

  class Program
  {
    static bool GameRunning = true;

    static void Main(string[] args)
    {
      // Instanciate Game Screen
      MainMenu mainmenu = new MainMenu();
      PlayMenu playmenu = new PlayMenu();
      GameoverMenu gameovermenu = new GameoverMenu();


      // Instanciate the Game Manager
      Game Game_T = new Game();


      // Add all scene in the Game Manager
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scenename"></param>
    /// <param name="gm"></param>
    static void Game_T_OnSceneEntry(string scenename, Game gm)
    {
      if (scenename.ToLower() == "playmenu")
      {
        // Prepare the list of player moves
        List<GameDB.MoveType> PlayerMoves = new List<GameDB.MoveType>();

        foreach(KeyValuePair<GameDB.MoveType,List<GameDB.MoveType>> item in GameDB.MoveTable)
        {
          PlayerMoves.Add(item.Key);
        }


        foreach (IPlayer p in gm.PlayerList)
        {
          p.ConfigureMove(PlayerMoves);
        }
      }
    }
  }
}
