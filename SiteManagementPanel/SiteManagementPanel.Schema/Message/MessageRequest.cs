namespace SiteManagementPanel.Schema;

public class MessageRequest
{
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public string Content { get; set; }
}
