/* 
 * MoveDBC.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Game Data Base
 * > Each move definition
 * > Each rule for all game move
 * > Match result definition
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
 * WARNING: When the move is updated you have to:
 * > Keep the same order in the MoveType definition than the MoveTable
 * > All Move you had must be add in the MoveTable in the same order that the MoveType
 * */
using System;
using System.Collections.Generic;
using System.Text;

namespace RPSGame.GameDataBase
{
  /// <summary>
  /// Game Data Base Stataic class can be use every where
  /// </summary>
  public static class GameDB
  {
    /// <summary>
    /// List of move type
    /// !WARNING! This Enum order must be respect in the MoveTable
    /// </summary>
    public enum MoveType
    {
      ePaper,
      eRock,
      eScissor,
#if TEST_HAMMER
      eHammer
#endif
    };

    /// <summary>
    /// List of match result
    /// </summary>
    public enum MatchResult
    {
      eDrawn,
      eWin,
      eLoss
    };

    /// <summary>
    /// Dictionary Move => List of move that this one can win
    /// !WARNING! Must be defined in the same order that the enumeration
    /// 
    /// </summary>
    public static Dictionary<MoveType, List<MoveType>> MoveTable = new Dictionary<MoveType, List<MoveType>>()
    {
      {MoveType.ePaper, new List<MoveType> {MoveType.eRock}},
      {MoveType.eRock, new List<MoveType> {MoveType.eScissor}},
      {MoveType.eScissor, new List<MoveType> {MoveType.ePaper}},
#if TEST_HAMMER
      {MoveType.eHammer, new List<MoveType> {MoveType.eRock, MoveType.eScissor}},
#endif
      };

  }
}
