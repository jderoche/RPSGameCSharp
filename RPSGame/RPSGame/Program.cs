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
 * */

using System.Collections.Generic;
using BaseClass.GameDataBase;
using Game.GameMenus;
using BaseClass.PlayerMgt;
using BaseClass.GameManager;

namespace RPSGameApplication
{

    class Program
    {
        static bool GameRunning = true;

        static void Main(string[] args)
        {
            // Instanciate GameControler Screen
            MainMenu mainmenu = new MainMenu();
            PlayMenu playmenu = new PlayMenu();
            GameoverMenu gameovermenu = new GameoverMenu();


            // Instanciate the GameControler Manager
            GameControler gamescreenControler = new GameControler();


            // Add all scene in the GameControler Manager
            gamescreenControler.AddScene("MainMenu", mainmenu);
            gamescreenControler.AddScene("PlayMenu", playmenu);
            gamescreenControler.AddScene("GameoverMenu", gameovermenu);
            gamescreenControler.OnSceneEntry += new GameControler.SceneEntry(gameControlerOnScreenEntry);
            gamescreenControler.OnExit += new GameControler.Exit(gameControlerOnScreenExit);

            // Start with Main Menu (Root Menu)
            gamescreenControler.GoToScene("MainMenu");
            while (GameRunning)
            {
                gamescreenControler.SceneUpdate();
            }

            return;
        }

        /// <summary>
        /// Exit Game Scren
        /// </summary>
        static void gameControlerOnScreenExit()
        {
            GameRunning = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scenename"></param>
        /// <param name="gm"></param>
        static void gameControlerOnScreenEntry(string scenename, GameControler gm)
        {
            if (scenename.ToLower() == "playmenu")
            {
                // Prepare the list of player moves
                List<GameDB.MoveType> PlayerMoves = new List<GameDB.MoveType>();

                foreach (KeyValuePair<GameDB.MoveType, List<GameDB.MoveType>> item in GameDB.MoveTable)
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
