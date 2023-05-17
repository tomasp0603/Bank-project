# Bank project
 Repository for bank project in C#.
 It has basic funcionalities like pin verification via database, operations such as transfer, deposit and withdraw, and a chance to see your balance, that is modified every time an operation occurs. The project connects to a database that includes the table for checking the pin and modifying and saving the balance.
 To run the project, you need visual studio 2022, net6.0, run the sql script in Sql Server, and change the server credentials in DatabaseRepository.cs line 121.
Version 2.0 Add funcionalities like an user and password system, which replaces the pin system, in the program and the database. You can use the user in transfer also and you can change the user or password every time you want. It also adds a movement record, to keep track of every operation that has been done, and the user also can see all operations that were done. 
