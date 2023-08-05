# Site Yönetimi Paneli
Bir site yöneticisi, sitede yer alan dairelerin aidat ve ortak kullanım elektrik, su ve doğalgaz faturalarının yönetiminin yapıldığı .Net Core Web API'sidir.

### Kullanılan Teknolojiler
- .Net 6
- MSSQL
- .Net Entity Framework
- JWT Token
- AutoMapper
- Serilog

### Migration Ekleme
```sh
Add-Migration InitalSiteManagement

update database
```

### Projenin Katmanları

- `SiteManagementPanel.Base`: BaseModelin, JWT ve Respons'un bulunduğu katmandır.
- `SiteManagementPanel.Data`: Migration'ın bulunduğu ve Repository'lerin tanımlandığı katmandır.
- `SiteManagementPanel.Business`: Servislerin tanımlandığı ve işlemlerin yapıldığı katmandır.
- `SiteManagementPanel.Schema`: Request ve Response modellerin bulunduğu katmandır.
- `SiteManagementPanel.Service`: API'nin bulunduğu kısımdır.


### Proje Hakkında

  

