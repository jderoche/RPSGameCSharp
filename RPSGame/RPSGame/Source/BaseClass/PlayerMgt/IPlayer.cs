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
using BaseClass.GameDataBase;

namespace BaseClass.PlayerMgt
{
  interface IPlayer
  {
    /// <summary>
    /// Get the player move (user input or compute)
    /// </summary>
    /// <returns></returns>
    GameDB.MoveType GetMove();

    /// <summary>
    /// Use to configure the player move
    /// </summary>
    /// <param name="movelist">list of move that we want to add to the player</param>
    void ConfigureMove(List<GameDB.MoveType> movelist);

    /// <summary>
    /// Store the history of the previous match for player compute process
    /// </summary>
    /// <param name="opponentmov">last opponent move</param>
    /// <param name="playermov">last player move</param>
    /// <param name="matchres">last match result</param>
    void RecordGameResult(GameDB.MoveType opponentmov, GameDB.MoveType playermov, GameDB.MatchResult matchres);

    /// <summary>
    /// return player name
    /// </summary>
    /// <returns></returns>
    string GetName();

    /// <summary>
    /// Get Player move list
    /// </summary>
    /// <returns></returns>
    List<GameDB.MoveType> GetPlayerMoves();

    /// <summary>
    /// Get current win counter
    /// </summary>
    /// <returns></returns>
    int ReadWinCounter();

  }
}
