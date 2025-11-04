# 🎮 RPG Playground

*A personal project for exploring and prototyping RPG mechanics in Unity.*

---

## 🕹️ About the Project
This is a personal learning project built to experiment with RPG game systems such as dialogue, quests, combat AI, and cinematics.

> ⚠️ **Note:** The game is still in development and does not yet include standalone builds. Some features and areas may be incomplete.

---

## 🚀 How to Run

1. Clone this repository: 
   git clone ![https://github.com/DeltaT71/RPG-Playground](https://github.com/DeltaT71/RPG-Playground)
   Open the project in Unity 2022.x or newer

2. Load the Island Scene located in Assets/Scenes/

3. Press Play in the Unity Editor

## ✨ Features
### 🧭 Basic UI

![Main Menu GIF](Assets/Media/Main-Menu.gif)

- Main menu and quest dialogue box

- Player health and potion display

- Enemy health bars

### 💬 Dialogue & Quest System

![NPC Dialogue](Assets/Media/NPC-talk.gif)
![NPC Dialogue](Assets/Media/Obtaining-quest.gif)

- Built using Ink and a state machine to track quest progress

- NPCs initiate quests and provide branching dialogue choices

- Rewards are given for completing NPC objectives

### ⚔️ Enemy AI (NavMesh)

![Enemy Ai Chase](Assets/Media/Enemy-Chase-AI.gif)
![Enemy Ai Return](Assets/Media/Enemy-Return.gif)

- Enemies chase and attack the player using Unity’s NavMesh

- Two AI types: Patrolling and Stationary

- AI behavior controlled via a state machine (idle, patrol, attack, death)

### 🎥 Cinematics

![Cutscene 1](Assets/Media/Cutscene.gif)
![Cutscene 2](Assets/Media/Switching-Scenes.gif)

- Short in-game cinematics created with Cinemachine and camera dolly tracks

### 🗡️ Combat

![Cutscene 2](Assets/Media/Enemy-Defeat.gif)

- Simple Space Bar click combat that can hit multiple enemies.

### 🕺 Character Animations

- Player and NPCs feature walk, run, attack, and death animations

- All managed via the Animator Controller

### 🧭 Future Plans / Roadmap

- 🎧 Add sound effects

- 🏝️ Improve and polish island & dungeon maps for atmosphere

- 🌊 Create custom shaders for water and reflections

- 💥 Design VFX for combat

### 💾 Player Saving

Implements persistent player progress using PlayerPrefs

Saves items, health, and completed quests

Automatically restores saved data when the game is reopened

### 🙌 Credits

Developed as part of the ZeroToMasters Unity Bootcamp course — huge thanks to the instructors!

Free assets used:

KayKit Free Assets
Kenney Nature Kit
