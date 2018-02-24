# RPSGameCSharp
Rock, Paper, Scissors Game

# Requirement:
Simple application to process a match of rock, paper, scissors.

# Game Rules

A match takes place between 2 players and is made up of 3 games, with the overall winner being the first player to win 2 games (i.e. best of 3).
Each game consists of both players selecting one of Rock, Paper or Scissors; the game winner is determined based on the following rules:

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


### DESIGN
Using MVC Design
# Models
Player
 > Name
 > Type
 > Tactics
 > WinCounter
 > LossCounter
 > Move

GamePlay
 > PlayerList
 > Number of turn
 > Win Condition
 
# View

# Controler
 > GameViewController
 >>> Display Player
 >>> Input or Play PlayerMove
 >>> Display Results
 >>> Display Final Result
 
 
