using Microsoft.Extensions.Logging;

namespace VisionaryCoder.ifx;

public class Identifier
{
    public Guid Instance { get; set; } = Guid.NewGuid();
    public Guid Uuid { get; set; }
}

public class IdentifierFactory
{

    public Identifier Create()
    {
        return Create(Guid.NewGuid());
    }

    public Identifier Create(Guid uuid)
    {
        var identifier = new Identifier
        {
            Uuid = uuid
        };
        return identifier;
    }

}

public abstract class ServiceBase<T> where T : class
{

    public Guid Instance { get; } = Guid.NewGuid();
    protected internal ILogger<T> logger { get; }

    protected ServiceBase(ILogger<T> logger)
    {
        this.logger = logger;
    }


}