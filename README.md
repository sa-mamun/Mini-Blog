Configuration:
1. You need to create database first and then change connection string.
2. Second, change connection string inside BlogApp.Core/Settings/NHibernateSQLContext.cs class.
3. Third, change connection string inside web.config
4. No need to create table. I also added seed for Admin account and Role for both Admin, User. Applied Role Base Authorization
5. Just run the project and seed will be applied.
