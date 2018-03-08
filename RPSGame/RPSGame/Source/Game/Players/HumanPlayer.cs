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
  public class HumanPlayer : Player
  {

    public HumanPlayer(string name)
      : base(name)
    {
    }

    public override GameDB.MoveType GetMove()
    {
      GameDB.MoveType res = (GameDB.MoveType)0;

      Console.WriteLine("Player : " + this.GetName());
      Console.WriteLine("");
      Console.WriteLine("Choose your move : ");
      Console.WriteLine("");
      int keyindex = 0;

      // Show list of move to player
      foreach (GameDB.MoveType mv in this.GetPlayerMoves())
      {
        // Increase index 1 => Move1 2 => Move2 so and so
        keyindex++;

        // Add separator before excepted for the first one XX / YY / ZZ
        if (keyindex > 1)
          Console.Write(" / ");
        Console.Write(keyindex.ToString() + " => " + mv.ToString());
      }
      Console.WriteLine("");

      bool validinput = false;

      while (validinput == false)
      {
        char input = Console.ReadKey().KeyChar;
        int value = (input - '0')-1;
        if ((value >= 0) && (value <= keyindex))
        {
          validinput = true;
          res = (GameDB.MoveType)value;
        }
      }

      return res;
    }
  }
}
