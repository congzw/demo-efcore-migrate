# 数据库迁移说明

## 模板

```sh


# 可以手动移动Migrations文件并更改其命名空间。 你可以在生成时指定目录，如下所示：
# 还可以使用 -Namespace独立于目录更改命名空间。
PM> Add-Migration {迁移标识} -Project {项目名称} -StartupProject {启动项目名称}  -o {迁移代码存放的目标文件夹} 
PM> Add-Migration InitialCreate -OutputDir Your\Directory


# 列出所有现有迁移
PM> Get-Migration -Project WebApp.Migrate

# 删除上个迁移
PM> Remove-Migration -Project WebApp.Migrate

```

## 修改历史

```sh

PM> Add-Migration EmptyDb -Project WebApp.Migrate
PM> Add-Migration V1.0 -Project WebApp.Migrate
PM> Add-Migration V1.1 -Project WebApp.Migrate
PM> ...

```



