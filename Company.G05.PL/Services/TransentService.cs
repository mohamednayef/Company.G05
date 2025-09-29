namespace Company.G05.PL.Services;

public class TransentService : ITransentService
{
    public Guid Guid { get; set; }

    public TransentService()
    {
        Guid = Guid.NewGuid();
    }
    public string GetGuid()
    {
        return Guid.ToString();
    }
}