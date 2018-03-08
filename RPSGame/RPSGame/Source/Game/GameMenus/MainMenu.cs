/* 
 * MainMenu.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Interface for player
 * User can exit game or play
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
using BaseClass.GameManager;
using Game.Players;

namespace Game.GameMenus
{
    /// <summary>
    /// Main Screen Menu
    /// </summary>
    public class MainMenu : GameScreen
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
            Console.WriteLine("4) Play (Basic CPU vs Advanced CPU) ^^");
            Console.WriteLine("E) Exit");
            Console.WriteLine("");

        }

        /// <summary>
        /// Specific User Input Treatement
        /// </summary>
        /// <param name="gm"></param>
        public override void Update(GameControler gm)
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

                            // Add Players to the GameControler Manager
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

                            // Add Players to the GameControler Manager
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

                            // Add Players to the GameControler Manager
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

                            // Add Players to the GameControler Manager
                            gm.AddPlayer(Player1);
                            gm.AddPlayer(Player2);
                            gm.GoToScene("PlayMenu");
                        }
                        break;
                }
            }
        }
    }

}
