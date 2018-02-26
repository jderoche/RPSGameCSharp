using System;
using System.Collections.Generic;
using System.Text;

namespace RPSGame.GameManager
{
  public abstract class GameScreen:IGameScreen<Game>
  {


    public virtual void Update(Game gm)
    {
    }

    public virtual void Display()
    {
      Console.Clear();
    }

  }
}
