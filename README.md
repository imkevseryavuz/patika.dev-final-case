# Site Yönetimi Paneli
Bir site yöneticisi, sitede yer alan dairelerin aidat ve ortak kullanım elektrik, su ve doğalgaz faturalarının yönetiminin yapıldığı .Net 6 Web API projesidir.

---

### Proje Hakkında
Yönetici binada oturan kişileri `User` tablosuna ekleyip, oturan kişilere otomatik olarak oluşturduğu kullanıcı adı ve şifre sayesinde dairede oturan bina sakinleri kendilerine belirlenen faturaları `Bill` tablosundan alıp, sadece `CreditCartRequest` 'den aldığı kart bilgileriyle, ödeme yapıldığında `Payment` tablosuna ödediği tarih, ödeyen daire numarası ekleniyor ve ödeme bu şekilde sağlanıyor. Dairede oturan kullanıcı bu sistemde yöneticiye `Message` tablosunu kullanarak mesaj gönderiminde bulunabiliyor. Yapılan `Log`'lar sayesinde sistemde bulunan hatalar ve yapılan her işlem tutulmaktadır.
`UpdateBillRequest` de ödenmiş olan faturaların Id si alınarak `Bill` için güncelleme işlemi yapılıyor. Bu sayede site yöneticisi ödenen faturaların sorgusuna `GetPaidBills` buradan ulaşabilmektedir.
`ApartmentUser` ve `Building` ara katmanlardır. Many to Many ilişkisi bulunan tablolardaki ilişkiyi daha kolay yönetmek için eklenmiştir.`ApertmenUser` kısmında `Apartment` ve `User` arasında ilişki sağlarken, `Building` de içerisinde `Block` kısmını bulundurarak `Apartment` da ara katman oluşturması içindir.

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

### Örnek Kod Anlatımı
Proje içeriside JWT Token kullanıldığı için, kullanıcı adı ve şifre girerek erişim sağlanmalıdır.


### Endpoint

```
 POST  /panel/api/Message
```

```
 GET   /panel/api/Message/user/{userId}
```

```
 GET   /panel/api/Message/{messageId 
```

```
POST   /panel/api/Message/{messageId}/mark-as-read
```
 

#### IMessageService
```C#
public interface IMessageService
{
    ApiResponse<MessageResponse> SendMessage(MessageRequest messageRequest);
    ApiResponse<List<MessageResponse>> GetMessagesByUserId(int userId);
    ApiResponse<MessageResponse> GetMessageById(int messageId);
    ApiResponse MarkMessageAsRead(int messageId);
}
```
- `IMessageService`: arabirimi, mesajlaşma işlemlerini gerçekleştiren bir servisin temel metotlarını tanımlar. Servis, uygulama tarafından bu arabirimi uygulayarak mesajlaşma işlevlerini farklı biçimlerde gerçekleştirebilir (örneğin, veritabanına yazma, harici API çağrıları, vb.). İstemciler, bu arabirimi kullanarak mesajlaşma hizmetine erişebilir ve ilgili işlemleri yapabilir.
- `SendMessage(MessageRequest messageRequest)`: Bu yöntem, bir mesaj göndermek için kullanılır. İstemci tarafından `MessageRequest` türündeki bir mesaj isteği nesnesi verilir. Bu nesne, mesajı gönderecek olan kullanıcının kimliğini, hedef kullanıcının kimliğini, mesaj içeriğini ve diğer ilgili bilgileri içerir. Yöntem, verilen bilgilerle yeni bir mesaj oluşturur ve gönderir. Dönen değer ise MessageResponse türünde olur ve gönderilen mesajın bilgilerini içerir.
- `GetMessagesByUserId(int userId)`: Bu yöntem, belirli bir kullanıcıya ait mesajları getirmek için kullanılır. İstemci tarafından kullanıcının kimliği (userId) verilir ve bu kimliğe sahip kullanıcıya ait tüm mesajlar `List<MessageResponse>` olarak döndürülür. MessageResponse nesnesi, her bir mesajın bilgilerini içerir.
- `GetMessageById(int messageId)`: Bu yöntem, belirli bir mesajın detaylarını getirmek için kullanılır. İstemci tarafından mesajın kimliği (messageId) verilir ve bu kimliğe sahip mesajın detayları `MessageResponse` türünde döndürülür. Bu yöntem, genellikle bir mesajın içeriğini, gönderen ve alıcı bilgilerini, zaman damgasını vb. gibi ayrıntıları getirmek için kullanılır.
- `MarkMessageAsRead(int messageId)`: Bu yöntem, bir mesajın okundu olarak işaretlenmesi için kullanılır. İstemci tarafından mesajın kimliği (messageId) verilir ve bu kimliğe sahip mesajın `IsRead` özelliği true olarak güncellenir. Bu, mesajın okundu olarak işaretlendiğini belirtir. Bu yöntem, genellikle bir mesajın okundu olarak işaretlenmesi gereken senaryolarda kullanılır. Dönen değer ise `ApiResponse` türündedir ve başarılı bir işlem durumunda boş döner `(HTTP 204 "No Content" durum kodu)`.

  
#### IMessageService
- `MessageService`: Mesajlaşma işlemlerini gerçekleştiren bir hizmet sağlar ve IMapper ve IUnitOfWork bağımlılıklarını enjekte eder.
```C#
public class MessageService : IMessageService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public MessageService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
```

#### SendMessage
```C#
public ApiResponse<MessageResponse> SendMessage(MessageRequest messageRequest)
    {
        try
        {
            var message = new Message
            {
                FromUserId = messageRequest.FromUserId,
                ToUserId = messageRequest.ToUserId,
                Content = messageRequest.Content,
                IsRead = false
            };

            _unitOfWork.MessageRepository.Insert(message);
            _unitOfWork.Complete();

            var response = _mapper.Map<Message, MessageResponse>(message);
            return new ApiResponse<MessageResponse>(response);
        }
}
```
- `SendMessage(MessageRequest messageRequest)` yöntemi, MessageRequest türündeki bir mesaj isteği nesnesi alarak mesajın gönderilmesini gerçekleştirir.
- Yöntem içerisinde, `Message` nesnesi oluşturulur ve `MessageRequest` nesnesinden gelen verilerle doldurulur. Oluşturulan Message nesnesi, `_unitOfWork.MessageRepository.Insert(message)` ile veritabanına eklenir.
- `_unitOfWork.Complete()` yöntemi, veritabanındaki değişiklikleri onaylar ve kalıcı hale getirir.
- `_mapper.Map<Message, MessageResponse>(message)` kullanılarak oluşturulan Message nesnesi, MessageResponse türüne dönüştürülür. Bu, gönderilen mesajın bilgilerini içeren MessageResponse türündeki bir nesne elde edilmesini sağlar.
- Dönüştürülen MessageResponse nesnesi, `ApiResponse<MessageResponse>` türünde bir nesne oluşturulur ve döndürülür. Bu ApiResponse nesnesi, mesaj gönderme işleminin başarılı olup olmadığını ve olası hataları içeren bir yanıtı temsil eder.
- Eğer try bloğundaki işlemler sırasında bir hata oluşursa, catch bloğu çalışır. Hatayı loglar ve hatayı açıklayıcı bir mesajla `ApiResponse<MessageResponse>` türünde bir hata yanıtı döner.
  

#### MessageController
`MessageControlle`r adında bir ASP.NET Core Web API denetleyicisini tanımlar. Denetleyici, mesajlaşma işlemlerini yönetmek için `IMessageService` arabirimini kullanır ve bağımlılıklarını enjekte eder.
```C#
public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
```
##### SendMessage [POST] Metodu
```C#
 [HttpPost]
        public IActionResult SendMessage([FromBody] MessageRequest messageRequest)
        {
            var response = _messageService.SendMessage(messageRequest);
            return Ok(new ApiResponse<MessageResponse>(response.Message));
        }
```
- `HttpPost niteliği`: Bu nitelik, bu yöntemin bir HTTP POST isteğiyle tetikleneceğini belirtir.
- `public IActionResult SendMessage([FromBody] MessageRequest messageRequest)`: MessageRequest, JSON formatında HTTP isteği gövdesinden alınır ve FromBody niteliği ile belirtilir. Bu sayede ASP.NET Core, gelen JSON verilerini MessageRequest nesnesine otomatik olarak dönüştürür.
- `MessageRequest nesnesi`, messageRequest parametresi aracılığıyla alınır.
- `var response = _messageService.SendMessage(messageRequest)`: Bu satırda, MessageRequest nesnesi `_messageService.SendMessage()` yöntemine gönderilir. Bu, IMessageService arabirimine sahip bir hizmet sınıfı olan _messageService üzerinden mesajın gönderilmesini gerçekleştirir. SendMessage yöntemi, gönderilen mesajın sonucunu ve olası hataları içeren bir `ApiResponse<MessageResponse>` nesnesi döndürür.
- `return Ok(new ApiResponse<MessageResponse>(response.Message))`: Son adımda, gönderilen mesajın sonucu, ApiResponse<MessageResponse> türündeki response nesnesinden alınır ve bu sonucu Ok yöntemi aracılığıyla HTTP 200 ("OK") yanıtı olarak döndürülür. JSON formatında dönüştürülmüş sonuç, ApiResponse<MessageResponse> içerisindeki Message özelliği kullanılarak alınır ve döndürülür.
  

##### GetMessagesByUserId [GET] Metodu
```C#
[HttpGet("user/{userId}")]
        public IActionResult GetMessagesByUserId(int userId)
        {
            var response = _messageService.GetMessagesByUserId(userId);
            return Ok(new ApiResponse<List<MessageResponse>>(response.Message));
        }
```
- `[HttpGet("user/{userId}")]`: Bu HTTP GET yönteminin çalışacağı URL rotasını tanımlar. userId, URL'de user/{userId} şeklinde bir yol parametresi olarak belirtilir. Yani, bu yöntem yalnızca /user/{userId} şeklindeki isteklerle erişilebilir. {userId} ifadesi, gelen isteğe göre değişen bir kullanıcı kimliğini belirtir.
- `public IActionResult GetMessagesByUserId(int userId)`: Bu, GetMessagesByUserId adlı bir HTTP GET isteği işlem yöntemidir. userId, URL rotasından gelen yol parametresi olarak alınır.
- İlk adımda, gelen userId değeri, userId parametresi aracılığıyla alınır.
- `var response = _messageService.GetMessagesByUserId(userId)`: Bu satırda, userId değeri _messageService.GetMessagesByUserId() yöntemine gönderilir. Bu, IMessageService arabirimine sahip bir hizmet sınıfı olan _messageService üzerinden belirli bir kullanıcıya ait tüm mesajların alınmasını gerçekleştirir. GetMessagesByUserId yöntemi, kullanıcıya ait mesajların bir listesini içeren bir `ApiResponse<List<MessageResponse>>` nesnesi döndürür
- `return Ok(new ApiResponse<List<MessageResponse>>(response.Message))`: Son adımda, kullanıcıya ait mesajların sonucu, ApiResponse<List<MessageResponse>> türündeki response nesnesinden alınır. Bu sonuç, JSON formatında HTTP 200 ("OK") yanıtı olarak döndürülür. `ApiResponse<List<MessageResponse>>` içerisindeki Message özelliği kullanılarak, mesajların listesi alınır ve döndürülür.


##### GetMessagesByUserId [GET] Metodu
```C#
[HttpGet("{messageId}")]
        public IActionResult GetMessageById(int messageId)
        {
            var response = _messageService.GetMessageById(messageId);
            return response.Success ? Ok(response.Message) : NotFound(response.Message);
        }
```
- `[HttpGet("{messageId}")]`: Bu HTTP GET yönteminin çalışacağı URL rotasını tanımlar. messageId, URL'de {messageId} şeklinde bir yol parametresi olarak belirtilir. Yani, bu yöntem yalnızca `/{messageId}` şeklindeki isteklerle erişilebilir. {messageId} ifadesi, gelen isteğe göre değişen bir mesaj kimliğini belirtir.
- `public IActionResult GetMessageById(int messageId)`: Bu, GetMessageById adlı bir HTTP GET isteği işlem yöntemidir. messageId, URL rotasından gelen yol parametresi olarak alınır.
- İlk adımda, gelen `messageId değeri`, `messageId` parametresi aracılığıyla alınır.
- ` var response = _messageService.GetMessageById(messageId)`: Bu satırda, messageId değeri `_messageService.GetMessageById()` yöntemine gönderilir. Bu, IMessageService arabirimine sahip bir hizmet sınıfı olan _messageService üzerinden belirli bir mesajın detaylarını almayı gerçekleştirir. GetMessageById yöntemi, mesajın detaylarını içeren bir `ApiResponse<MessageResponse>` nesnesi döndürür.
- `return response.Success ? Ok(response.Message) : NotFound(response.Message)`: Son adımda, ApiResponse<MessageResponse> türündeki response nesnesinin Success özelliği kontrol edilir. Eğer Success özelliği true ise, mesajın detayları Ok yöntemi aracılığıyla `HTTP 200 ("OK")` yanıtı olarak döndürülür ve `response.Message` içerisindeki mesaj detayları gönderilir. Eğer Success özelliği false ise, yani mesaj bulunamazsa, `NotFound` yöntemi aracılığıyla `HTTP 404 ("Not Found")` yanıtı döndürülür ve response.Message içerisindeki hata mesajı gönderilir.

  
##### MarkMessageAsRead [POST] Metodu
```C#
[HttpPost("{messageId}/mark-as-read")]
        public IActionResult MarkMessageAsRead(int messageId)
        {
            var response = _messageService.MarkMessageAsRead(messageId);
            return response.Success ? Ok(response.Message) : NotFound(response.Message);
        }
    }
}
```
- `[HttpPost("{messageId}/mark-as-read")]`: Bu HTTP POST yönteminin çalışacağı URL rotasını tanımlar. messageId, URL'de {messageId} şeklinde bir yol parametresi olarak belirtilir ve mark-as-read kelimesi, messageId'nin sonuna eklenerek belirli bir mesajın "okundu" olarak işaretlenmesini belirten bir işlem adı eklenir. Yani, bu yöntem yalnızca `/{messageId}/mark-as-read` şeklindeki isteklerle erişilebilir.
- `public IActionResult MarkMessageAsRead(int messageId)`: Bu, MarkMessageAsRead adlı bir HTTP POST isteği işlem yöntemidir. messageId, URL rotasından gelen yol parametresi olarak alınır.
- İlk adımda, gelen messageId değeri, messageId parametresi aracılığıyla alınır.
- `var response = _messageService.MarkMessageAsRead(messageId)`: Bu satırda, messageId değeri `_messageService.MarkMessageAsRead()` yöntemine gönderilir. Bu, IMessageService arabirimine sahip bir hizmet sınıfı olan _messageService üzerinden belirli bir mesajın "okundu" olarak işaretlenmesini gerçekleştirir. MarkMessageAsRead yöntemi, işlemin sonucunu içeren bir ApiResponse nesnesi döndürür.
- `return response.Success ? Ok(response.Message) : NotFound(response.Message)`: Son adımda, ApiResponse türündeki response nesnesinin Success özelliği kontrol edilir. Eğer Success özelliği `true` ise, işlem başarılı olduğu için mesajı Ok yöntemi aracılığıyla `HTTP 200 ("OK")` yanıtı olarak döndürülür ve `response.Message` içerisindeki mesaj gönderilir. Eğer `Success` özelliği `false` ise, yani işlem başarısız olduysa, `NotFound` yöntemi aracılığıyla `HTTP 404 ("Not Found")` yanıtı döndürülür ve response.Message içerisindeki hata mesajı gönderilir.

---

## LICENSE
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

