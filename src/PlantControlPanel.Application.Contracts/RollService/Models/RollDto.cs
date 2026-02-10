using System;

namespace PlantControlPanel.Application.Contracts.RollService.Models;

public sealed record RollDto(
    int Id, 
    double Length, 
    double Weight, 
    DateTime AddTime, 
    DateTime? RemoveTime
    );