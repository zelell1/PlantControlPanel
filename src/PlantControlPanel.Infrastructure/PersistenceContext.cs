using PlantControlPanel.Application.Abstractions.Persistence;
using PlantControlPanel.Application.Abstractions.Persistence.Repositories;

namespace PlantControlPanel.Infrastructure;

public class PersistenceContext : IPersistenceContext
{
    public PersistenceContext(IRollRepository rollRepository)
    {
        RollRepository = rollRepository;
    }

    public IRollRepository RollRepository { get; }
}