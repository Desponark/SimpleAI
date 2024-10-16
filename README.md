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

## CustomTypes
## Nodes
## Resources(Godot)
## Singletons
