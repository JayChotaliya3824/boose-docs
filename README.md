[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/5EP0uYOE)
# ASE semester 1 2025 Portfolio
Basic Object Orientated Software Engineering (BOOSE).  
  

Fill in the fields below.

## Jay Chotaliya
## Group 3

I have used a central method called ExecuteBlock to implement the parser. It is a process that will run over the lines of code and create and run ICommand objects with the help of CommandFactory (that uses Singleton and Factory patterns).

The processes of putting into place the control structures and methods were the most challenging.

Loops (while, for): This was dealt with by searching to the end of the block containing the loop (e.g. endwhile, endif) and a recursive call to ExecuteBlock to execute the lines contained within it, depending upon the result of the checked condition.

Methods: This was tricky. The method command will recognize a signature (e.g. method draw(p)) and save the lines that follow the signature to a MethodDefinition object. The call command will then open a new scope on the variables of the method and copy the arguments and execute the lines of the method body. The Index was not within array bug is a vital problem here which was corrected by properly parsing the signature of the methodName(params).

Expression Evaluation: The expression evaluator is transferred to a special class called ExpressionEvaluator. This class is used to solve expressions such as x + 10 or p to one value and leave the primary parsing logic to be concerned with code structure.

Extensible Commands (Strategy Pattern): The system is based on the interface of ICommand. Every command (moveto, circle, var etc.) is a class that implements this interface. Such (Strategy) pattern implies that it is possible to add new commands to the interpreter simply by writing a new class, without changing the main parser loop.

Exception Handling:The try-catch block of the main loop contains exception handling where syntax and runtime errors (command not recognized or bad parameters) are addressed and reported without causing the crash of the application.

### Commit History
| Commit Message | Date |
|----------------|------|
| Fix Minor issues | Oct 25, 2025 |
| Final submission | Oct 25, 2025 |
| Implemented Factory, Singleton, and Interface patterns. | Oct 25, 2025 |
| Added Unit Tests for moveto, drawto, and multiline program. | Oct 25, 2025 |
| Added Parser class | Oct 24, 2025 |
| Added DrawingCanvas class. | Oct 24, 2025 |
| Feature: App successfully calls BOOSE.dll 'about' method. | Oct 24, 2025 |
Commits link: https://github.com/ASE2025repos/ase-boose-assignment-JayChotaliya3824/commits?author=JayChotaliya3824

# Checkpoint
#### 1 Version Control (1) 
	At least two documented commits to provided on GitHub Classroom BEFORE 12pm 7th October.
	At least 5 documented commits.
	Readme.md file filled in correctly.
#### 2 Unit Tests for basic drawing commands above (1) 
	Unit Test for moveto command (variables storing pen position are correct)
	Unit Test for drawTo command (variables storing pen position are correct)
	Unit Test for a multiline program.
	Done
#### 3 XML Comments/documentation produced (1) 
	Fully documented with XML comments and Documentation web site produced 
	Done
#### 4 Exception Handling (1) 
	Added a try-catch block in the main parser loop in order to deal with invalid commands and parameters.
#### 5 Library (DLL) installed and working with reasonable user interface (2)
	Form interface with program window, output window and run button
	BOOSE DLL library installed 
	call about method and display returned information in output/debug window
	you can use System.Diagnostics to output to the debug window
	Done
#### 6 Basic drawing commands of the library are implemented (2)
	Moveto, circle, rect, pencolour, write.
	Unrestricted drawing programs can be run (1unrestrictedDrawing.boose)
	DONE
### [You MUST fill in the this form before submission and submit your zipped up project to myBeckett](https://forms.gle/MJB6vEbgAPXC6A6G8)
Read the hand in instructions on myBeckett carefully to ensure you are submitting everything correctly, as if you don't you may be subject to a penalty.


# Final Submission
#### 1 Further Version Control (1)
	You must continue to use VC to a professional standard with frequent and clear commits
	Your Readme.md must be up to date
	DONE
#### 2 Use of Interfaces (1)
	Interfaces to be used for all classes where appropriate
	DONE
#### 3 Further Unit Testing (1)
	Tests for full program all of the facilities completed in 5,6 and 7 below
	It is up to you how design your tests (i.e. one test per facility or one test testing many but they must be clearly documented with XML comments)
	DONE
#### 4 Design Patterns (1)
	Use of factory Design Pattern for command creation
	Use of Singleton Design Pattern
	Demonstrate the use of another design pattern of your choice
	(for 5,6 and 7 you can click the links to see the example BOOSE programs that must be run, these are in your Portfolio. You must run the unrestricted programs and replace the image already there with an 	image of your BOOSE Interpreter running the BOOSE programs. You may show further programs in your portfolio.)]
	DONE
#### 5 Replaced variables (2) \*
	Int	
	Real	
	Array	
	DONE
#### 6 Replaced if, while, for (2) \*
	While 	
	For	
	If else	
	DONE
#### 7 Replaced Methods (2) \*
	Method, Endmethod and Call commands are all implemented and tested.
#### 8 Additional (web version, text-based version, extension to BOOSE itself, etc, discuss with your tutor) (2) 
I focused on the implementation of the GUI interface, design patterns and the unit testing.

\* You must rewrite the functionality and not try and "hack the system". I've tried to make it so you can't but who knows, either way, you've been told.
### [You MUST fill in this form whilst recording your YouTube Video AND submit it to myBeckett](https://forms.gle/j3eMcVbbjQ3sFrXw7)

Read the hand in instructions on myBeckett carefully to ensure you are submitting everything correctly, as if you don't you may be subject to a penalty.
## Links
These are for convinience. Check on myBeckett in the Assessment directory for any up to date links.  
	[BOOSE Documentation](https://dmullier.github.io/BOOSE-Docs/)  
	[BOOSE DLL Download](https://github.com/dmullier/BOOSE-Docs/blob/main/BOOSE.dll)  
	[Assignment Specification - including all university details](https://leedsbeckett-my.sharepoint.com/:b:/r/personal/d_mullier_leedsbeckett_ac_uk/Documents/Teaching/2025-2026/Level6_ASE/ASE_AssignmentSpec.pdf?csf=1&web=1&e=eYfpc5)  
	[Detailed Assignment Information - with marking scheme](https://leedsbeckett-my.sharepoint.com/:w:/r/personal/d_mullier_leedsbeckett_ac_uk/Documents/Teaching/2025-2026/Level6_ASE/Detailed%20Project%20Specification.docx?d=w5f5450df2c0d49fb968702d420314d2f&csf=1&web=1&e=cRDcet)  
	[Assignment Help Videos](https://leedsbeckettreplay.cloud.panopto.eu/Panopto/Pages/Sessions/List.aspx?folderID=ce4e861f-ed63-4714-97c1-b35300af453e)  


	Remember that BOOSE.DLL maybe updated, as it may have bugs in it, or new features may be added.  
	Keep updated on Discord and make sure you always have the latest version in your project.
 
### V1.0