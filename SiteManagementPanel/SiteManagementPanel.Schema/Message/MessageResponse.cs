using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagementPanel.Schema;

public class MessageResponse
{
    public int Id { get; set; }
    public string FromUserName { get; set; } // Optional: Eğer FromUser nesnesinde kullanıcı adı veya isim bilgisi varsa ekleyebilirsiniz.
    public string ToUserName { get; set; } // Optional: Eğer ToUser nesnesinde kullanıcı adı veya isim bilgisi varsa ekleyebilirsiniz.
    public string Content { get; set; }
    public bool IsRead { get; set; }
}
