using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PlantControlPanel.Application.Contracts.RollService.Models;
using PlantControlPanel.Domain;

namespace PlantControlPanel.Application.Mapping;

public static class RollsCatalogMappingExtension
{
    public static RollsCatalogDto MapToDto(this IEnumerable<Roll> rolls)
        => new RollsCatalogDto(rolls.Select(x => x.MapToDto()).ToList());
}