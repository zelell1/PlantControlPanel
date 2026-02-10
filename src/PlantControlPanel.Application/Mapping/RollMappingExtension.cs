using System;
using PlantControlPanel.Application.Contracts.RollService.Models;
using PlantControlPanel.Domain;

namespace PlantControlPanel.Application.Mapping;

public static class RollMappingExtension
{
    public static RollDto MapToDto(this Roll roll) 
        => new RollDto(roll.Id, roll.Length, roll.Weight, roll.AddTime, roll.RemoveTime ?? DateTime.MinValue);
    
}