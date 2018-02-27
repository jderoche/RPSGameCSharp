using System;
using System.Collections.Generic;
using System.Text;

namespace RPSGame.GameDataBase
{
  /// <summary>
  /// Move Data Base
  /// </summary>
  public static class GameDB
  {
    public enum MoveType
    {
      eRock,
      ePaper,
      eScissor
    };

    public enum MatchResult
    {
      eDrawn,
      eWin,
      eLoss
    };

    /// <summary>
    /// Dictionary 
    /// </summary>
    public static Dictionary<MoveType, List<MoveType>> MoveTable = new Dictionary<MoveType, List<MoveType>>()
    {
      {MoveType.ePaper, new List<MoveType> {MoveType.eRock}},
      {MoveType.eRock, new List<MoveType> {MoveType.eScissor}},
      {MoveType.eScissor, new List<MoveType> {MoveType.ePaper}},
    };

  }
}
