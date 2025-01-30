using CRM_KSK.Application.Dtos;

public class ClientStateService
{
    public ClientDto SelectedClient { get; private set; }

    public void SetClient(ClientDto client)
    {
        SelectedClient = client;
    }
}
