# Technical Task

## Before we start

The goal of this assignment is to let us better understand your technical skills and thought process.

However, we understand you have other important things to do, therefore we aim for a short and concise work – we ask
that you invest in this assignment up to three hours.

The assignment can be completed in many different ways; we’re as interested in your thoughts, decisions and dilemmas as
much as we’re interested in the code you write. In fact, the amount of code or features you squeeze into the
assignment’s timeframe is of less concern to us than how you wrote the code and which decisions you took – and why. This
also includes your decisions regarding how to manage your time, what to invest in and what not.

We know that to get a slick appearance and great UX, lots of time and effort have to be invested. Thus we don’t expect
to get a world-class UI in this assignment; however, we do expect to get a functional and reasonably planned piece of
code.

## What we expect to get

1. The code you developed, complete in a way that we can run, along with any documentation or tests you wrote.
2. A short text document describing the development process, important decisions you took and why you took them, any
   issues you had, and what else you would do, had you have more time. Please plan ahead your time and make sure to
   allocate a while to this part as well! Following the assignment’s submission, we’ll discuss the process over the
   phone.

## The assignment

In this exercise you will make a space invaders game.

Don’t worry about the graphics, use whatever shapes and colors you like, as long as there’s a visual difference between
enemies, player, projectiles, etc

### Main menu

The first screen you see is the menu, which has only one button “play” that will make the game start

### Start of the game

The game starts paused with all the enemies and the player in positions, after a short delay a timer of 3 seconds will
appear and the game will start at the end of the count down.

### Player

The player is located at the bottom of the screen (always starts in the middle). Using left and right arrows will move
the player, make sure you can’t go outside the screen. Pressing space will make the player shoot.

### Lives

The player will have 3 lives and are indicated (in a way of your choosing) at the top left corner, and each time the
player is hit by an enemy projectile one life will be subtracted, the level will pause as the player reappear in the
middle (provided you have enough lives) and a timer of 3 seconds will appear in the middle of the screen, once it hits
0 (no need to show “0”) the level will start. Once the player loses all lives, the game will be over and a message will
appear in the middle of the screen.

### Score

The score is located at the top left corner above the lives, you start the game with a score of 0. Whenever you destroy
an enemy a floating fading text (“2” for example) will appear at the location of impact.

### Enemies

Enemies start as a group of 6x6 in the top middle of the screen, once the game starts they will start moving to the
right as a group, when one of them hits the edge of the screen they will all go down a row and start moving (a little
bit faster) to the left with the same behaviour. There are two types of enemies: Normal: they can just move with the
group, this enemy scores 1 point when destroyed Shooting: they move with the group and shoot every 2 seconds (their
projectiles go through the player projectiles and can’t hit other enemies), this enemy scores 2 points when destroyed
The first and last enemy of the second and fourth rows are shooting enemies. If an enemy collides with the player the
player loses (regardless of the remaining lives) and an appropriate message will appear in the middle of the screen.

### Mothership

Every 4 seconds there is a 40% chance that a mothership will appear at either the right or left side at random going to
the opposite direction, the mothership will appear above all the enemies and will be located at the starting position of
the second enemies row, the mothership will start just outside of the screen and will move until its outside again on
the opposite end. Every 2 seconds the mothership will shoot. A mothership scores 10 points when destroyed.

### Win condition

When all enemies are destroyed the game is over and an appropriate message will appear in the middle of the screen. If
all enemies are destroyed but there’s still one or more mothership present, the game will give the player the chance to
destroy them and will end only after they’ve been destroyed or escaped.

### End game

When the game ends, either by winning or losing, a popup will be shown giving the player an option to start a new game
or return to the main menu.

# Result

Almost at the end realized that I write code more like for hackathon. So the final result is not well written and
optimized, need to be refactored. I have spent almost 4 hours on this task.  
Development:

1. UI or Sprites? Chose UI for easier aspect ratios support + in love with RectTransforms)
   What I’ll do If I had more time:
2. Finish all features (mothership, floating score text).
3. Split scipts for more single responsibility behaviours.
4. Refactor code. And make it more abstract, for example IRestartable, IPausable.
5. Add ObjectPool for bullets, enemies.
6. Add Zenject
7. Add DoTween
8. Tune gameplay (speeds for enemies, player, bullets)
