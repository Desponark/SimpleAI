# Basics
Game Engine: Godot 4.1 mono  
Language: C#  
IDE: VS Code  
Status: Working & Finished (but not finished everything planned)  

## Context
During my education as Game Engineer at S4G School for Games Hamburg I created this project as part of my final thesis.  
If more detailed information is desired, the written part of my thesis can be found [here](https://docs.google.com/document/d/1TaqpgCDXjyLnH98ccysHczYeL2GXz1UuKVceg6GqspY/edit?usp=sharing) (Don't expect the highest acedemic strictness as it was not required for this work. I think it still turned out okay and I did get the maximum score).
 
The goal of this project is to combine steering behaviours with a simple finite state machine in order to to explore how they can be used to create a believable AI behaviour system for an Action-RPG that focuses on small encounters and third person action combat (imagine Outward, a Souls game or something of that nature).

Also I'm currently working on another follow up project in which I am creating a GOAP (Goal Oriented Action Planning) AI. I wil publish it once there is enough progress.

# Showcase
https://github.com/user-attachments/assets/d5207230-0b06-4def-a218-edbc1f636b5d

[Better Quality Link](https://drive.google.com/file/d/1JAko0W-VNtZXy5gSlPrsZiTP0O_p7gT6/view?usp=drive_link)

# Basic Framework
This explains the basic framework for the project.  

## Steering Behaviour
Simplified, an autonomous agent consists of three parts. The “why”, the “where” and the “how”. Why do this action? Where do I go? How do I move? I like to think of these parts as the brain that decides what to do **(Action Selection)**, an internal compass that tells it where to go **(Steering)** and the legs that execute the movement **(Locomotion)**.

- Action Selection => Implemented via [Cognition class](https://github.com/Desponark/SimpleAI/blob/main/Nodes/Cognition.cs)
- Steering => Implemented via [Steering class](https://github.com/Desponark/SimpleAI/blob/main/Nodes/Steering.cs)
- Locomotion => Implemented via [Vehicle class](https://github.com/Desponark/SimpleAI/blob/main/Nodes/Vehicle.cs)


## Finite-State Machine
I have decided to go with a variant that embeds the rules for state transitions within the states themselves also known as a state design pattern.  

First there is the [State class](https://github.com/Desponark/SimpleAI/blob/main/CustomTypes/AI/State.cs) which is generic for flexibility and serves as the parent for all other states. Note that the Execute method is the only one that returns something because it is used to switch states.  

Next up is the [StateMachine class](https://github.com/Desponark/SimpleAI/blob/main/CustomTypes/AI/StateMachine.cs) which essentially works by calling the Execute() method of the State class on every Update().  
The globalState is used for having a simple way of implementing things that always apply to the agent regardless of which state they are actually in. Like a hunger meter decreasing over time.

# Implementation
Here I'll try to explain each component/class shortly. It roughly follows the structure of the project.

## CustomTypes
- Steering Behaviours
  - Approach
    - Approach returns a steering force that steers an agent towards the target position up to a certain distance from it.
  - FollowPath
    - FollowPath returns a steering force that moves an agent along an array of positions in sequence.
  - Orbit
    - Orbit returns a steering force that steers an agent in an orbit around the target.
  - WallAvoidance
    - WallAvoidance returns a steering force that steers an agent away from walls via an array of feelers.
- DebugDrawer
  - This class can visualize vectors via coloured arrows and is intended for debugging and easier developing.
## Nodes
- Cognition
  - The Cognition class is the brain of my agents. The idea of this class is to encapsulate all logic that represents the thinking and decision making of an agent.
  - Memory
    - Memory is a property intended as a quick and easy way for the agent to “memorize” data for later use.
  - Threat Evaluation
    - This feature is intended as a way for an agent to evaluate all other perceivable agents and decide which target to focus on.
- Commanding
  - This node is the most recent addition and not completely finished.
The idea of this class is for leaders to offer commands to followers and also memorize current followers, for the leader, so that commands might be issued to a specific follower.
- GameplayStats
  - The idea of this class is to hold gameplay statistics and information for the agent like flight, fight, alertness and hp or similar.
- Perception
  - The idea of this class is to handle any and all ways of perception an agent can possess, be it sight, hearing, smell or even touch, and store everything that is perceived on corresponding properties which then can be accessed by Cognition (i.e. the brain) to react and use this information as it sees fit.
- PlayerCamera
  - This is just a very simple class to keep the camera attached to the player character without any rotation.
- PlayerInput
  - Its main purpose is to provide access to the last clicked mouse position so the player’s Cognition can use this information and move the player accordingly.
- Steering
  - This class handles all steering and contains a collection of steering behaviours which get individually calculated, added up and returned as a weighted truncated running sum with prioritization.
- Vehicle
  - This class handles translating the steering force (a vector) into a velocity for moving the agents, i.e. it provides the locomotion for all agents.
## Resources(Godot)
- InitialState
  - This class tells an agent's cognition what its initial state is.
## Agents (Scenes)
- Bandit
  - The basic Idea is to have a generic humanoid agent that has a somewhat simple but aggressive behaviour and can serve as a rough base template for other humanoid agents.
- Deer
  - Here the intention was to have an agent that shows fleeing / skittish behaviour and how it could be implemented.
- Wolf
  - The idea of the wolf was to demonstrate an agent that first needs to be provoked, by getting too close or staying in its sight too long, before attacking.
- Player
  - The plan behind the Player was to have it be like any other agent. It simply receives information not from the Perception but from the PlayerInput and indirectly the PlayerCamera.
- BanditLeader
  - The idea was that the bandit leader is always traveling with a bandit escort in some kind of formation and can dynamically commandeer surrounding bandits to either follow, attack or maybe defend.
## Singletons
- AIState_Empty
  - This state serves as a blank placeholder for when an InitialState is created and there are no states that can be returned yet or there is no need for a global state like in case of the Player.
- AIState_BanditPatrolling
  - The patrolling state is responsible for letting the bandit follow a given path and is also its initial state. Most other agents instead don’t follow a path but wander randomly.
- AIState_Bandit_Global
  - To reiterate shortly, the global state is for having a simple way of implementing logic that always executes regardless of the current state the agent is in.
