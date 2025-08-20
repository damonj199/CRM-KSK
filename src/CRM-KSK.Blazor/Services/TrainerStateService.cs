using CRM_KSK.Application.Dtos;

public class TrainerStateService
{
    public TrainerDto SelectedTrainer { get; set; }

    public void SetTrainer(TrainerDto trainer)
    {
        SelectedTrainer = trainer;
    }
}
