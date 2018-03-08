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
using BaseClass.PlayerMgt;
using BaseClass.GameDataBase;

namespace Game.Players
{
    /// <summary>
    /// Basic CPU : Random Move
    /// </summary>
    public class BasicCPU : Player
    {
        /// <summary>
        /// Call default constructor
        /// </summary>
        /// <param name="name"></param>
        public BasicCPU(string name)
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

            // Get the randome move from 1 to PlayerMove list count
            int value = new Random().Next(1, this.GetPlayerMoves().Count);
            res = (GameDB.MoveType)value;


            return res;
        }
    }
}
