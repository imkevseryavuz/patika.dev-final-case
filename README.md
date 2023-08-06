# Site Yönetimi Paneli
Bir site yöneticisi, sitede yer alan dairelerin aidat ve ortak kullanım elektrik, su ve doğalgaz faturalarının yönetiminin yapıldığı .Net 6 Web API projesidir.

---

### Proje Hakkında
Yönetici binada oturan kişileri User tablosuna ekleyip, oturan kişilere otomatik olarak oluşturduğu kullanıcı adı ve şifre sayesinde dairede oturan bina sakinleri kendilerine belirlenen faturaları Bill tablosundan alıp, sadece CreditCartRequest 'den aldığı kart bilgileriyle, ödeme yapıldığında Payment tablosuna ödediği tarih, ödeyen daire numarası ekleniyor ve ödeme bu şekilde sağlanıyor. Dairede oturan kullanıcı bu sistemde yöneticiye Message tablosunu kullanarak mesaj gönderiminde bulunabiliyor. Yapılan Log'lar sayesinde sistemde bulunan hatalar ve yapılan her işlem tutulmaktadır.

---
### Kullanılan Teknolojiler
- .Net 6
- .Net Entity Framework
- MSSQL
- JWT Token
- AutoMapper
- Serilog
- Dökümantasyon için: Swagger
---
### Migration Ekleme
Nugget Package Console kısmına aşağıdaki kodu yazıp çalıştırdığınızda migration kısmı eklenecektir ve veritabanı ile bağlantı sağlanacaktır.
```sh
Add-Migration InitalSiteManagement
```
Migration başarılı bir şekilde eklendikten sonra yine Console kısmında aşağıdaki komutu yazıp çalıştırabilirsiniz.
```sh
update database
```
---
### Database Diagramı
<img src="https://github.com/imkevseryavuz/patika.dev-final-case/blob/main/images/DatabaseDiagram.PNG" style="height:500; width:600" >

---
### Projenin Katmanları

- `SiteManagementPanel.Base`: BaseModelin, JWT ve ApiResponse'un bulunduğu katmandır.
- `SiteManagementPanel.Data`: Migration'ın bulunduğu ve Repository'lerin tanımlandığı katmandır.
- `SiteManagementPanel.Business`: Servislerin tanımlandığı ve işlemlerin yapıldığı katmandır.
- `SiteManagementPanel.Schema`: Request ve Response modellerin bulunduğu katmandır.
- `SiteManagementPanel.Service`: API'nin bulunduğu kısımdır.
---

### Örnek Kullanım




