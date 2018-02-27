/* 
 * Player.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Abstract class of Player
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
 * By default move return is the first define in the GameDatabase.MoveType
 * 
 * */

using System;
using System.Collections.Generic;
using System.Text;
using RPSGame.GameDataBase;

namespace RPSGame.PlayerMgt
{
  /// <summary>
  /// Player abtract class
  /// </summary>
  public abstract class Player:IPlayer
  {
    #region Private_Properties

    /// <summary>
    /// Current Game win counter
    /// </summary>
    private int currentGameWinCnt;


    /// <summary>
    /// Player move list (can be configure by high level module (Game Manager))
    /// </summary>
    List<GameDB.MoveType> PlayerMove;

    /// <summary>
    /// History of player move
    /// </summary>
    protected List<GameDB.MoveType> PlayerMoveHistory;

    /// <summary>
    /// History of opponent move
    /// </summary>
    protected List<GameDB.MoveType> OpponentHistory;

    /// <summary>
    /// History of Player vs Opponent match result
    /// </summary>
    protected List<GameDB.MatchResult> MatchResultHistory;

    string mName = "";

    #endregion

    #region Interface

    /// <summary>
    /// Read the current win counter
    /// </summary>
    /// <returns></returns>
    public int ReadWinCounter()
    {
      return currentGameWinCnt;
    }

    /// <summary>
    /// Get player move
    /// </summary>
    /// <returns></returns>
    public virtual GameDB.MoveType GetMove()
    {
      // move to return
      GameDB.MoveType res = (GameDB.MoveType)0;



      return res;
    }

    /// <summary>
    /// Configure move for the player
    /// </summary>
    /// <param name="movelist"></param>
    public void ConfigureMove(List<GameDB.MoveType> movelist)
    {
      PlayerMove.AddRange(movelist.ToArray());
    }

    /// <summary>
    /// Record last match informations
    /// </summary>
    /// <param name="opponentmov"></param>
    /// <param name="playermov"></param>
    /// <param name="matchres"></param>
    public void RecordGameResult(GameDB.MoveType opponentmov, GameDB.MoveType playermov, GameDB.MatchResult matchres)
    {
      OpponentHistory.Add(opponentmov);
      PlayerMoveHistory.Add(playermov);
      MatchResultHistory.Add(matchres);

      // Increase Game win counter if win
      if (matchres == GameDB.MatchResult.eWin)
        currentGameWinCnt++;
    }

    /// <summary>
    /// Get player move list
    /// </summary>
    /// <returns></returns>
    public List<GameDB.MoveType> GetPlayerMoves()
    {
      return PlayerMove;
    }

    public string GetName()
    {
      return mName;
    }

    #endregion



    #region Public_Methode

    /// <summary>
    /// Constructor
    /// </summary>
    public Player(string name)
    {
      mName = name;
      PlayerMove = new List<GameDB.MoveType>();
      OpponentHistory = new List<GameDB.MoveType>();
      PlayerMoveHistory = new List<GameDB.MoveType>();
      MatchResultHistory = new List<GameDB.MatchResult>();
    }

    public int Score
    {
      set
      {

      }
    }

    #endregion
  }
}
