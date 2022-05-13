namespace Sonnette.chat.Models;

public class NotificationsModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TypeAppui { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Date)}: {Date}, {nameof(TypeAppui)}: {TypeAppui}";
    }
}