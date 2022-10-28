# modsen-pretrainee-MeetupAPI
Modsen test pre-trainee task.

# How to run the project.

1. Download and install MSSQL and MSSQL Management Studio, MS Visual Studio 2022, .NET 6, if you haven't one.

2. Find out server's name. MSSQL Management Studio has connection window with db attributes.

3. Download Repository to your computer.

4. Prepare work with IIS Express:
https://learn.microsoft.com/ru-ru/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio#tabpanel_9_visual-studio

And if you use Mozila Firefox this guide may help you:
https://learn.microsoft.com/ru-ru/aspnet/core/security/enforcing-ssl?view=aspnetcore-6.0&tabs=visual-studio#trust-ff

5. Inside the project MeetupAPI open appsettings.json and replace string 'Lancer\\ERGOSERVER' on your server's name from p.2.

6. Now, you can run the project.

7. To obtain right to use various method you should authorize by post 'admin'-'admin' and copy the response.

8. Click Green button authorize, enter word 'Bearer' with space and insert response token from p.7.

9. Now, all function available to use.

10. Have fun!