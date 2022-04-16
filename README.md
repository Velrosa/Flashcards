# Flashcards
- A C# Console Application, Allows the user to study "Flashcards". 
- User can view, create, update and delete "Stacks" of "Cards" which have questions and answers belonging to them.
- Stacks must have unique names, a Card must belong to a stack. If a Stack is deleted its Cards are also.
- Stacks may be studied in a "Session", you will recieve a score upon completetion.
- All Study Sessions are saved and can be reviewed overall or yearly, if you delete a Stack its Sessions will also be removed.

# Features

* Console UI MENU for interaction
  - ![MainMenuFlashcards](https://user-images.githubusercontent.com/101323127/163473688-a26f56c4-578b-4402-837a-cb11e100621d.png)
* SQL CRUD functionailty for Cards and Stacks
  - ![CRUDFlashcards](https://user-images.githubusercontent.com/101323127/163473705-4c804dd0-b334-4749-b386-77e235000bfb.png)
* Studying of Stacks
  - ![StudyFlashcards](https://user-images.githubusercontent.com/101323127/163473724-55f0e26a-71d9-43c9-9b6f-59fa0f8ee99a.png)
* Storing and Reporting of Study Session Data (Stack, Score, Date and Time)
  - ![SessionFlashcards](https://user-images.githubusercontent.com/101323127/163473732-33247a05-e894-4754-ac5b-8f6d089c2416.png)
* Use of ConsoleTableExt. for creating user-friendly data views
  - ![TableFlashcards](https://user-images.githubusercontent.com/101323127/163473710-ed6143d9-97c1-48d5-a58c-bac3c63b203d.png)
* Use of SQL PIVOT for Yearly score summary
  - ![YearlyFlashcards](https://user-images.githubusercontent.com/101323127/163473734-87994424-dfca-441c-b44c-2c6504620939.png)
* Use of a DatabaseController Class for handling all database requests

* Use of a Validation Class for ensuring clean inputs from the user

* Use of DTO's for transfer of object data around the program and into the database

* Use of SQL server (not SQlite)

* Use of Linked SQL tables (foreign keys)
