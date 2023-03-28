# 数据库迁移说明

## 模板

```sh

# 可以手动移动Migrations文件并更改其命名空间。 你可以在生成时指定目录，如下所示：
# 还可以使用 -Namespace独立于目录更改命名空间。
PM> Add-Migration InitialCreate -OutputDir Your\Directory
PM> Add-Migration {迁移标识} -Project {项目名称} -StartupProject {启动项目名称}  -o {迁移代码存放的目标文件夹} 

# 列出所有现有迁移
PM> Get-Migration

PM> Remove-Migration -Project {项目名称} （To undo this action, use Remove-Migration）
PM> Update-Database -Project NbSites.Migrations

# 有时可能在添加迁移后意识到需要在应用迁移前对 EF Core 模型作出其他更改。 要删除上个迁移，请使用如下命令。
PM> Remove-Migration

```

## 开发期间常用命令

```sh


# 如果 DbContext 与启动项目位于不同程序集中，可以在包管理器控制台工具或 .NET Core CLI 工具中显式指定目标和启动项目。
Add-Migration InitDb -Project DemoMigrationLib

```


##

```csharp

//当某个操作可能会导致数据丢失（例如删除某列），搭建迁移基架过程将对此发出警告。 如果看到此警告，务必检查迁移代码的准确性。
//删除重建
migrationBuilder.DropColumn(
    name: "Name",
    table: "Customers");
migrationBuilder.AddColumn<string>(
    name: "FullName",
    table: "Customers",
    nullable: true);

//改名
migrationBuilder.RenameColumn(
    name: "Name",
    table: "Customers",
    newName: "FullName");

//为了从旧列传输数据，我们重新排列迁移并引入原始 SQL 操作，如下所示：
migrationBuilder.AddColumn<string>(
    name: "FullName",
    table: "Customer",
    nullable: true);

migrationBuilder.Sql(
@"
    UPDATE Customer
    SET FullName = FirstName + ' ' + LastName;
");

migrationBuilder.DropColumn(
    name: "FirstName",
    table: "Customer");

migrationBuilder.DropColumn(
    name: "LastName",
    table: "Customer");


```
