using System;

namespace PlantControlPanel.Domain;

public class Roll(int id, double length, double weight, DateTime addTime)
{
    public int Id = id;

    public double Length = length;

    public double Weight = weight;

    public DateTime AddTime = addTime;
    
    public DateTime? RemoveTime { get; set; }
}