### Prerequisites
- .NET 7.0 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 or VS Code


### Default Test Account
- **Email**: userd@mail.ee
- **Password**: heromine


## Testing

### Running Unit Tests
```bash
cd Project.UnitTests
.\run-tests.ps1
dotnet test
```

### Running Integration Tests
```bash
cd Project.IntegrationTests
dotnet test
```

