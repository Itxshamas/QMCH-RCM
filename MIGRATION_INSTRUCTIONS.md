# Migration Instructions for ServiceType Model Update

After updating the ServiceType model from:
- `Name` ? `Description` (Required field)
- `Description` ? `ShortDescription` (Optional field)

Run the following commands in the Package Manager Console or terminal in your project directory:

## Option 1: Using Package Manager Console (Visual Studio)

```powershell
Add-Migration UpdateServiceTypeModel
Update-Database
```

## Option 2: Using .NET CLI (Terminal/Command Prompt)

```bash
dotnet ef migrations add UpdateServiceTypeModel
dotnet ef database update
```

## Migration Details

This migration will:
1. Rename the existing `Name` column to `Description`
2. Rename the existing `Description` column to `ShortDescription`
3. Update the `ShortDescription` to be nullable (optional)
4. Keep the `CreatedAt`, `Code`, `DefaultRate`, and `IsActive` columns unchanged

After running the migration, the database schema will match the updated model.
