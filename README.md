# RPSGameCSharp
Rock, Paper, Scissors Game

# Requirement:
Simple application to process a match of rock, paper, scissors.

# Game Rules

A match takes place between 2 players and is made up of 3 games, 
with the overall winner being the first player to win 2 games (i.e. best of 3).
Each game consists of both players selecting one of Rock, Paper or Scissors; 
the game winner is determined based on the following rules:

· Rock beats scissors
· Scissors beats paper
· Paper beats rock

## Requirements

Your application must support three types of player:

· Human Player The user must be prompted for a selection of Rock, Paper or Scissors for each turn
· Random Computer Player The random computer player should automatically select one of Rock, Paper or Scissors at random for each turn
· Tactical Computer Player The tactical computer player should always select the choice that would have beaten its last choice, e.g. if it played Scissors in game 2, it should play Rock in game 3.

## Extensions

The following are some of the possible extensions that may be made to the application at a later date. You do not need to implement these, but they should be considered in your design.

· New player types We may want to add new computer player implementations as tactics improve
· Longer matches We may want to change the match format to “best of 5” at a later date
· New “moves” We may expand the possible moves that each player can make (e.g. Rock, Paper, Scissors, Lizard, Spock)

## Approach for Game Design
This game is like a RPG card game (Example : Heartstone)
### Specificities
> Player have a list of move (action or card)  that he can used one time in one match. 
> It's not a turn base game everyone play at the same time
 
# Preliminary Class Design

## Move: Enum

## MoveDataBase
A database to manage all move interaction
For example
[
  [
    Paper : 
    [
      Rock
    ]
  ],
  [
    Rock : 
    [
      Scissor
    ]
  ],
  ...
  >> Paper win Rock but loss with Scissor drawn with Paper
  >> Rock win Scissor but loss with Paper ...
  Can be update for example like this
  [
    Well : 
    [
      Paper,
	  Scissor
    ]
  ],
  >> Well win Paper and Scissor but loss with rock

# MoveManager
This is a list of move that we can build by default for all player but also add a specific 
behavior for an update of the game rules for exemple after 3 match "Rock" 
cannot be play or for a specific player "rock can be play only one time for all the game".
+ List<Move>

# Player
IPlayer<Interface>
+ GetMove() : move
>> Get Player Input or apply computation for CPU move
+ RecordGameResult(Move OpponentMove, Move PlayerMove, MatchResult Result)

# Base player model (abstract) : IPlayer
+ Name 
+ current game Win Counter
+ List<Move>
+ Type (Kind of player, humand, computer)
## This properties can be used for basic computer strategy process or advanced
+ List<Move> HistoryOpponentMove: List of all previous Opponent move
+ List<Move> HistoryPlayerMove : List of all previous player move
+ List<MatchResult> MatchResultHistory : List of all previous match result


## Game Scene
+ Display() => Methode to display a screen view
+ Update(GameManager) 
>>> Methode to get user input, execute player action, etc...
>>> Give this methode access to gamemanager methode in order to load another scene or quit

## IGameManager
+ GoToScene(string) : Go to a scene add in local container
+ AddScene(string , GameScreen ) : Add scene in local container
+ ExitGame()
+ SceneUpdate()

# EXTENSIONS
## Menu can be added for example : a menu to let the player enter his name
+ We only have to make a new class derivated from GameScreen and override the Display and Update methode
+ In the Update the Game object is given in argument in order to expose the services:
>> To Add Player
>> To Launch a new Game Screen
>> etc... see Game Class
## New move can be added thanks to GameDB static classic
>> You only need to add the new move definition in the MoveType (enum)
>> !WARNING! Restriction is to keep the same order and you MUST define the move behavior in the
MoveTable and Respect the same Order as defined in the MoveType (Enum)
>> There is an example with eHammer (Change in the project compile symbol '_TEST_HAMMER' into 'TEST_HAMMER')

# IMPROVE
The architecture can be improve in order to take more that two players
> The main modification should be in the Game Screen "the algorithm to find the winner"


 
