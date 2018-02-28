/* 
 * Game.cs
 * ======================================================================================
 * Description:
 * ======================================================================================
 * Manage the player, the game screen
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
 * 
 * */
using System;
#if TRACE
using System.Diagnostics;
#endif
using System.Collections.Generic;
using System.Text;
using RPSGame;
using RPSGame.PlayerMgt;
using RPSGame.UnitTest;

namespace RPSGame.GameManager
{
  public class Game:IGameManager
  {

#region Events
    // Add Event Notifycation for game scene
    public delegate void SceneEntry(string name, Game gm);
    public event SceneEntry OnSceneEntry;

    // Add Event Notifycation for game scene exit
    public delegate void Exit();
    public event Exit OnExit;
#endregion

    #region Private_Properties
    /// <summary>
    /// Scenes container
    /// </summary>
    private Dictionary<string, GameScreen> SceneList;

    /// <summary>
    /// Current Game Scene Instance
    /// </summary>
    private GameScreen CurrentScene;
    #endregion

    #region Public
    /// <summary>
    /// List of player
    /// </summary>
    public List<Player> PlayerList;

    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    public Game()
    {
      SceneList = new Dictionary<string, GameScreen>();
    }


    public void RemoveAllPlayer()
    {
      PlayerList.Clear();
    }

    /// <summary>
    /// Add scene in the scenes container
    /// </summary>
    /// <param name="scenename"></param>
    /// <param name="gamescene"></param>
    public void AddScene(string scenename, GameScreen gamescene)
    {
      if (!SceneList.ContainsKey(scenename))
      {
        if (gamescene != null)
        {
          SceneList.Add(scenename, gamescene);
        }
        else
        {
#if TRACE
          Trace.WriteLine("Scene null +"+scenename+" cannot be load");
#endif
        }
      }
      else
      {
#if TRACE
      Trace.WriteLine("Scene "+scenename+" already exist");
#endif
      }
    }


    /// <summary>
    /// Load a scene from the scenes container into current scene view
    /// </summary>
    /// <param name="gamescene"></param>
    public void GoToScene(string scenename)
    {
      if ( SceneList.ContainsKey(scenename))
      {
        GameScreen gs = SceneList[scenename];

        // Call Scene entry notification
        if (gs != null)
        {
          // Load the next scene
          CurrentScene = gs;
        }
        // Otherwise switch to the new scene
        else
        {
#if TRACE
          Trace.WriteLine("Scene " + scenename + " is null");
#endif
        }

        // Call Scene entry notification
        if (OnSceneEntry != null)
        {
          // Call entry Notification
          if (OnSceneEntry != null)
            OnSceneEntry(scenename,this);
        }
      }
      else
      {
#if TRACE
       Trace.WriteLine("Scene Not exist"); 
#endif
      }
    }


    public void ExitGame()
    {
      if (OnExit != null)
        OnExit();
    } 

    /// <summary>
    /// Scene View Loop
    /// </summary>
    public void SceneUpdate()
    {
      // Execute Main Loop of the game
      if (CurrentScene != null)
      {
        CurrentScene.Display();
        // Give access to game manager in order to load new scene view
        CurrentScene.Update(this);
      }

    }

    /// <summary>
    /// Add player in game manager system
    /// </summary>
    /// <param name="player"></param>
    public void AddPlayer(Player player)
    {
      if (PlayerList == null)
        PlayerList = new List<Player>();
      PlayerList.Add(player);
    }
}
}
