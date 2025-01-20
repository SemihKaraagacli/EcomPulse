# Ecompulse API

This project represents the API part of an e-commerce platform called **Ecompulse**. Developed with .NET 8, this API is used to manage user management, payment processing, orders and more.

## Start

#### Requirements

- .NET 8 SDK
- SQL Server or any database server (Project requires database connection)
- Visual Studio or similar IDE (Optional)
- Git

### Running the Project

1. **Clone the Project**

   Clone the GitHub repo to your computer:

   ```bash
   git clone https://github.com/SemihKaraagacli/EcomPulse.git

2. **Install Required Packages**

   Navigate to the project directory and run the following command to install the required NuGet packages:

   ```bash
   dotnet restore

3. **Configure Database Connections**

   Open appsettings.json and update the database connection information.

   ```bash
    “ConnectionStrings": {
    “DefaultConnection": “Server=localhost;Database=ecompulse;User Id=sa;Password=your_password;”
   }
