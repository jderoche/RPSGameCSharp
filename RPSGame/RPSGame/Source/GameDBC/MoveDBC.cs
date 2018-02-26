using System;
using System.Collections.Generic;
using System.Text;

namespace RPSGame.GameMove
{
  /// <summary>
  /// Move Data Base
  /// </summary>
  public static class MoveDBC
  {
    public enum MoveType
    {
      eRock,
      ePaper,
      eScissor
    };

    /// <summary>
    /// Dictionary 
    /// </summary>
    Dictionary<MoveType, List<MoveType>> MoveTable = new Dictionary<MoveType, List<MoveType>>()
    {
      {MoveType.ePaper, new List<MoveType> {MoveType.eRock}},
      {MoveType.eRock, new List<MoveType> {MoveType.eScissor}},
      {MoveType.eScissor, new List<MoveType> {MoveType.ePaper}},
    };

  }
}
