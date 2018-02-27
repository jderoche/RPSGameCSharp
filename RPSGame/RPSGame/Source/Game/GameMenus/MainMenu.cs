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
      if ((key == 'E') || (key == 'e'))
        gm.ExitGame();

      if ((key == 'P') || (key == 'p'))
        gm.GoToScene("PlayMenu");
    }
  }

}
