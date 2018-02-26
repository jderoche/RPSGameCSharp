using System;
using System.Collections.Generic;
using System.Text;
using RPSGame;
using RPSGame.PlayerMgt;

/// Game Manager System
namespace RPSGame.GameManager
{
  /// <summary>
  /// Interface for game manager loop
  /// </summary>
  interface IGameManager
  {

    /// <summary>
    /// Load a scene from the scenes container into current scene view
    /// </summary>
    /// <param name="gamescene"></param>
    void GoToScene(string scenename);

    /// <summary>
    /// Add scene in the scenes container
    /// </summary>
    /// <param name="scenename"></param>
    /// <param name="gamescene"></param>
    void AddScene(string scenename, GameScreen gamescene);

    /// <summary>
    /// Exit Game Manager
    /// </summary>
    void ExitGame();

    /// <summary>
    /// Manage the main UI interface
    /// > Display 
    /// > Get User Input
    /// </summary>
    void SceneUpdate();

    /// <summary>
    /// Add player in game manager system
    /// </summary>
    /// <param name="player"></param>
    void AddPlayer(Player player);

  }
}
