# Tutorial_WebApiBasic


[ASPNET6 WebApi Basic Tutorial 1/2](https://youtu.be/2Jxu0K25Csc?si=YZUkTiSphz5ruzAI)

[ASPNET6 WebApi Basic Tutorial 2/2](https://youtu.be/OFG9cYsILWI?si=q9W4g7XcVS2WDg_9)



```bash

dotnet ef migrations add InitialCreate --context MyDbContext --output-dir Migrations/MsSqlMigrations --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic

dotnet ef migrations remove --project Tutorial_WebApiBasic

dotnet ef database update --context MyDbContext --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic


// ���� �� ���Ӱ� ����(�Ӽ� No �߰�)
dotnet ef migrations add AddNo --context MyDbContext --output-dir Migrations/MsSqlMigrations --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic


// ���� �� ���Ӱ� ����(�Ӽ� No ����)
dotnet ef migrations add RemoveNo --context MyDbContext --output-dir Migrations/MsSqlMigrations --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic

// ������ �߰��� migration�����ϱ�
dotnet ef migrations remove --context MyDbContext --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic

```


