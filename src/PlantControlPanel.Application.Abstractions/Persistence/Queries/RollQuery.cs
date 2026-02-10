using System;
using SourceKit.Generators.Builder.Annotations;

namespace PlantControlPanel.Application.Abstractions.Persistence.Queries;

[GenerateBuilder]
public sealed partial record RollQuery(
    int? MinId, 
    int? MaxId,
    double? MinWeight, 
    double? MaxWeight,
    double? MinLength, 
    double? MaxLength,
    DateTime? MinAddTime, 
    DateTime? MaxAddTime,
    DateTime? MinRemoveTime, 
    DateTime? MaxRemoveTime
    );