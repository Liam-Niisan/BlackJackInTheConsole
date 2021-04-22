README.md

Blackjack
Sample Code Louisville Project

A simple command line Blackjack game. The program will start and Display the starting hands of the dealer and the player. 
The player can choose to "Hit" or to "Stand". To 'Hit' is to ask for another card. To 'Stand' is to hold your total and end your turn.
The goal of blackjack is to beat the dealer's hand without going over 21. If you go over 21 you bust, and the dealer wins regardless of the dealer's hand.
Dealer will hit until his/her cards total 17 or higher.

Requirements:

1.) Create a class, then create at least one object of that class and populate it with data.
	The "BlackJack", "Member" and "Deck" class qualify
	

2.)Create and call at least 3 functions, at least one of which must return a value that is used.
	CanDealerHit, CanPlayerHit, and GetResult all qualify. 

3.)Implement a “master loop” console application where the user can repeatedly enter commands/perform actions, including choosing to exit the program.
	I have a while loop around the main method of the program that allows the user to repeatly put in input to finish the game and has the option to play another game or to exit the program.

4.) Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program.
	The card class uses a list to store all of the IDS and SUITS.


5.) Create an additional class which inherits one or more properties from its parent.
	The "Deck" class inherits from Stack<card>.

