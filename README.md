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

# Project Summary

## Topic
“How to create a believable agent behaviour system for an action game by applying simple solutions in Godot?”
  
- Steering Behaviours + FSM
  - Believable AI behaviour system for Action-RPG (Outward, Soulslike)
- Focus steering and behaviour, not locomotion or combat
  - “where” and “why” not “how”
- Simple as possible

## Result

### Framing Conditions
- Time limit of 2 months
- Godot v4.1-stable.mono

### Steering Behaviours
- Steering
  - Collection of SteeringBehaviour
    - SteeringBehaviour
      - Weight, Active, “Vector3 Calculate()”
      - Individually calculated
    - Summed up
  - Returned as weighted truncated running sum with prioritization
- Vehicle
  - Add steering force every update

### Finite-State Machine
- State Design Pattern
	- Embedded rules for transitions in states
- State (abstract, generic, singleton)
	- Enter, Exit
	- Execute
		- Returns new state
- State Machine
	- Current State, Previous State
	- ChangeState
	- Global State
		- For repeating code

### Agents
- Bandit
	- follow path -> chase -> orbit -> attack
- Deer
	- wander -> watch -> flee
- Wolf
	- wander -> watch -> chase -> attack -> flee
- Bandit Leader
	- follow path -> command follow

### Assembling an Agent
- Vehicle
	- Mesh
	- Cognition
		- State Machine
		- Memory
	- GameplayStats
	- Steering
	- Perception
