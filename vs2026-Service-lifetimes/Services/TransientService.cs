namespace vs2026_Service_lifetimes.Services;

public class TransientService
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid GetId()
    {
        return _id;
    }
}