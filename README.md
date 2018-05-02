# ToDoList
A simple program to keep User's To-do list with the ability to perform CRUD operations. The application uses SQL Server Compact 4.0 as a database.
# Program description
1. There are 2 tabs in application main window: "New Task", which is used to enter new tasks and "List of Tasks", where the User can view, update and delete tasks.

2. To add a new task, in the "Task Description" field of the "New Task" tab, enter the content of the task, then select the task deadline (the date can't be earlier than today) and press the "Add Task" button. After pressing the "Add Task" button, the task is added to the database (the application uses the SQL Server Compact 4.0 as a database).

3. On the screen of the "New Task" tab it is also possible to check the number of tasks for the next days as well as the number of outstanding tasks (those which have a deadline earlier than today and which have not been removed from the database).

4. To view the list of tasks for a given day, select the appropriate date from the calendar in the upper left corner of the "List of Tasks" tab and confirm the selection by clicking the "Load Tasks List" button. If there are no tasks on a given day, the User will be informed via MessageBox.

5. Tasks are updated by changing the content of the "Task Description" field and / or setting a new due date in the calendar, at the bottom of the "List of Tasks" tab window.

6. Changes are confirmed by pressing one of the 2 buttons: Update or Delete (before deleting the task, the User is asked to confirm the selection).

7. The installation file creates a database file (ToDoListDB.sdf) in the User's application data folder: C:\Users\[User]\AppData\Roaming
