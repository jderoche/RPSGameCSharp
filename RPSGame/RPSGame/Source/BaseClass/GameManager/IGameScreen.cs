using System;
using System.Collections.Generic;
using System.Text;
using RPSGame;

/// GameControler Manager System
namespace BaseClass.GameManager
{
  /// <summary>
  /// Interface for game manager loop
  /// </summary>
  interface IGameScreen<T>
  {

    /// <summary>
    /// Get Users input
    /// </summary>
    void Update(T gm);

    /// <summary>
    ///  Display information to player
    /// </summary>
    void Display();
  }
}
