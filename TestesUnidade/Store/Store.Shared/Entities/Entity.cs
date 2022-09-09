using Flunt.Notifications;

namespace Store.Shared.Entities;

public class Entity: Notifiable<Notification>
{
    public Guid Id { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}