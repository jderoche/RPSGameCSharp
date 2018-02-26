using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RPSGame.GameManager;


namespace RPSGame
{

  class MainMenu : GameScreen
  {
    public override void Display()
    {
      base.Display();
      // Might be a return of json for Web API
      Console.WriteLine("- Main Menu -");
      Console.WriteLine("-------------");
      Console.WriteLine("");
      Console.WriteLine("1) Play");
      Console.WriteLine("2) Exit");
      Console.WriteLine("");

    }

    /// <summary>
    /// Specific User Input Treatement
    /// </summary>
    /// <param name="gm"></param>
    public override void Update(Game gm)
    {
      if (Console.ReadKey().KeyChar == '2')
        gm.ExitGame();
    }
  }


  class Program
  {
    static bool GameRunning = true;

    static void Main(string[] args)
    {
      MainMenu mainmenu = new MainMenu();

      Game Game_T = new Game();
      

      Game_T.AddScene("MainMenu",mainmenu);

      Game_T.OnExit += new Game.Exit(Game_T_OnExit);
      Game_T.GoToScene("eoi");
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
  }
}
