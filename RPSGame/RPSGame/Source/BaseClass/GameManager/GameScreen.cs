using System;
using System.Collections.Generic;
using System.Text;

namespace BaseClass.GameManager
{
  public abstract class GameScreen:IGameScreen<GameControler>
  {


    public virtual void Update(GameControler gm)
    {
    }

    public virtual void Display()
    {
      Console.Clear();
    }

  }
}
