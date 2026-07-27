```
dotnet ef dbcontext scaffold "Server=.\SQLEXPRESS;Database=CustomAuthorizationLoginExample;User ID=sa;Password=zinlinoo1;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o AppDbContextModels -c AppDbContext --no-onconfiguring
```