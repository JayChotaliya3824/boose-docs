# BOOSE Interpreter Documentation

Welcome to the BOOSE Interpreter project documentation.

## Overview

The BOOSE Interpreter is the graphical programming language that allows users to create drawings through code commands. It has the features of a complete parser, command execution system, and GUI interface.

## Key Features

- **Drawing Commands**: moveto, drawto, circle, rect, pencolour, write
- **Control Structures**: while loops, for loops, if-else conditionals
- **Variable Support**: integers, real numbers, arrays
- **Method Definitions**: Create and call custom methods with parameters
- **Expression Evaluation**: Mathematical expressions and variable operations
- **GUI Interface**: Form-based interface with program window, output window, and run button

## Architecture Highlights

The interpreter uses a central `ExecuteBlock` method to parse and execute BOOSE programs. It creates and runs `ICommand` objects through a `CommandFactory` using the **Singleton** and **Factory** design patterns.

### Core Components

- **Parser**: It reads the BOOSE code line by line and creates command objects
- **CommandFactory**: It creates the appropriate command objects (Factory Pattern)
- **ICommand Interface**: The all commands implement this interface (Strategy Pattern)
- **ExpressionEvaluator**: It handles the mathematical expressions and the variable resolution
- **MethodDefinition**: It stores and manages the custom method definitions


## Implementation Challenges

### Control Structures
The loops (while, for) were implemented by searching for block end markers (endwhile, endif) and  recursively calling the `ExecuteBlock` based on condition tested.

### Methods
The method system recognizes the signatures (e.g., `method draw(p)`), saves lines to the `MethodDefinition` object, and then creates new variable scopes when called. The proper parameter parsing was critical to avoid index-out-of-bound errors.

### Expression Evaluation
A dedicated `ExpressionEvaluator` class handles the expression resolution, separating the concerns from the main parser logic.

## Design Patterns

- **Factory Pattern**: `CommandFactory` creates the command objects based on user's input
- **Singleton Pattern**: It ensures single instance of the key components
- **Strategy Pattern**: `ICommand` interface allows the extensible command system
- **Interface Segregation**: The all commands implement `ICommand` for the consistency

## Repository

GitHub Repository: [ASE BOOSE Assignment](https://github.com/ASE2025repos/ase-boose-assignment-JayChotaliya3824)

## Author

**Group 3** - Jay Chotaliya  
Advanced Software Engineering Course  
October 2025