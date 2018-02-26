using System;
#if TRACE
using System.Diagnostics;
#endif
using System.Collections.Generic;
using System.Text;
using RPSGame;
using RPSGame.UnitTest;

namespace RPSGame.GameManager
{
  public class Game:IGameManager
  {

#region Events
    // Add Event Notifycation for game scene
    public delegate void SceneEntry();
    public event SceneEntry OnSceneEntry;

    // Add Event Notifycation for game scene exit
    public delegate void Exit();
    public event Exit OnExit;
#endregion

    /// <summary>
    /// Scenes container
    /// </summary>
    private Dictionary<string, GameScreen> SceneList;

    /// <summary>
    /// Current Game Scene Instance
    /// </summary>
    private GameScreen CurrentScene;

    /// <summary>
    public Game()
    {
      SceneList = new Dictionary<string, GameScreen>();
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
          Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
          Trace.AutoFlush = true;
          Trace.Indent();
          Trace.WriteLine("Scene null +"+scenename+" cannot be load");
          Trace.Unindent();
#endif
        }
      }
      else
      {
#if TRACE
      Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
      Trace.AutoFlush = true;
      Trace.Indent();
      Trace.WriteLine("Scene "+scenename+" already exist");
      Trace.Unindent();
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
          Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
          Trace.AutoFlush = true;
          Trace.Indent();
          Trace.WriteLine("Scene " + scenename + " is null");
          Trace.Unindent();
#endif
        }

        // Call Scene entry notification
        if (OnSceneEntry != null)
        {
          // Call entry Notification
          if (OnSceneEntry != null)
            OnSceneEntry();
        }
      }
      else
      {
#if TRACE
       Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
       Trace.AutoFlush = true;
       Trace.Indent();
       Trace.WriteLine("Entering Main");
       Console.WriteLine("Hello World.");
       Trace.WriteLine("Exiting Main"); 
       Trace.Unindent();
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

  
}
}
