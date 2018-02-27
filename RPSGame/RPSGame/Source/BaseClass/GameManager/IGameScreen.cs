using System;
using System.Collections.Generic;
using System.Text;
using RPSGame;

/// Game Manager System
namespace RPSGame.GameManager
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
