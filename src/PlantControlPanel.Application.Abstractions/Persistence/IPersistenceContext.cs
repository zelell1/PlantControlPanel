using PlantControlPanel.Application.Abstractions.Persistence.Repositories;

namespace PlantControlPanel.Application.Abstractions.Persistence;

public interface IPersistenceContext
{
    IRollRepository RollRepository { get; }
}