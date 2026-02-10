using System;

namespace PlantControlPanel.Application.Contracts.RollService.Models;

public sealed record StatisticsDto(
    int AddedCount,
    int RemovedCount,
    double AverageLength,
    double AverageWeight,
    double MinLength,
    double MaxLength,
    double MinWeight,
    double MaxWeight,
    double TotalWeight,
    TimeSpan? MinProcessingDuration,
    TimeSpan? MaxProcessingDuration,
    DateTime? DayWithMinRollsCount,
    DateTime? DayWithMaxRollsCount,
    DateTime? DayWithMinTotalWeight,
    DateTime? DayWithMaxTotalWeight
);