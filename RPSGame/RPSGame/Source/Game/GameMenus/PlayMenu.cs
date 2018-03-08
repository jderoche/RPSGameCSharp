/* 
 * IPlayer.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Interface for player during the game
 * 
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

using BaseClass.PlayerMgt;
using BaseClass.GameDataBase;
using BaseClass.GameManager;

namespace Game.GameMenus
{
    /// <summary>
    /// Menu in GameControler
    /// </summary>
    public class PlayMenu : GameScreen
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
        public override void Update(GameControler gm)
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
}
