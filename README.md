# Tutorial_WebApiBasic


[ASPNET6 WebApi Basic Tutorial 1/2](https://youtu.be/2Jxu0K25Csc?si=YZUkTiSphz5ruzAI)

[ASPNET6 WebApi Basic Tutorial 2/2](https://youtu.be/OFG9cYsILWI?si=q9W4g7XcVS2WDg_9)



```bash

dotnet ef migrations add InitialCreate --context MyDbContext --output-dir Migrations/MsSqlMigrations --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic

dotnet ef migrations remove --project Tutorial_WebApiBasic

dotnet ef database update --context MyDbContext --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic


// 변경 후 새롭게 갱신(속성 No 추가)
dotnet ef migrations add AddNo --context MyDbContext --output-dir Migrations/MsSqlMigrations --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic


// 변경 후 새롭게 갱신(속성 No 삭제)
dotnet ef migrations add RemoveNo --context MyDbContext --output-dir Migrations/MsSqlMigrations --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic

// 마지막 추가된 migration삭제하기
dotnet ef migrations remove --context MyDbContext --project Tutorial_WebApiBasic --startup-project Tutorial_WebApiBasic

```


