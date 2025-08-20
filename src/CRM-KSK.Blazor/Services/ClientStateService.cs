using CRM_KSK.Application.Dtos;

public class ClientStateService
{
    public ClientDto SelectedClient { get; set; }

    public void SetClient(ClientDto client)
    {
        SelectedClient = client;
    }
}
