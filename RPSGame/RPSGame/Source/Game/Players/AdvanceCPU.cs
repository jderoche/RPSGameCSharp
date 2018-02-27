﻿/* 
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

using RPSGame.PlayerMgt;
using RPSGame.GameDataBase;

namespace RPSGame
{
  /// <summary>
  /// Basic CPU : Random Move
  /// </summary>
  class AdvanceCPU : Player
  {
    /// <summary>
    /// Call default constructor
    /// </summary>
    /// <param name="name"></param>
    public AdvanceCPU(string name)
      : base(name)
    {
    }

    /// <summary>
    /// Move strategy : Random
    /// </summary>
    /// <returns></returns>
    public override GameDB.MoveType GetMove()
    {
      GameDB.MoveType res = (GameDB.MoveType)0;

      Console.WriteLine("CPU : " + this.GetName());

      bool FoundCpuMove = false;

      if (MatchResultHistory.Count > 0)
      {
        GameDB.MoveType prvMove = OpponentHistory[OpponentHistory.Count - 1];
        foreach (KeyValuePair<GameDB.MoveType, List<GameDB.MoveType>> listMove in GameDB.MoveTable)
        {
          foreach (GameDB.MoveType mvt in GameDB.MoveTable[listMove.Key])
          {
            if (mvt == prvMove)
            {
              res = listMove.Key;
              FoundCpuMove = true;
              break;
            }
          }
          if (FoundCpuMove)
            break;
        }
      }
      else
      {
        // Get the randome move from 1 to PlayerMove list count
        int value = new Random().Next(1, this.GetPlayerMoves().Count);
        res = (GameDB.MoveType)value;
      }

      return res;
    }
  }
}