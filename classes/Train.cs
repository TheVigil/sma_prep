using System;
using System.Text.Json.Serialization;

namespace sma_prep;

[JsonPolymorphic]
[JsonDerivedType(typeof(Train), "Passenger")]
[JsonDerivedType(typeof(FreightTrain), "FreightTrain")]
public class Train
{
    public string Id { get; set; } = "";
    public string Operator { get; set; } = "";
    public List<SectionOccupation> Sections { get; set; } = new();
    public virtual double MaxSpeedKmh { get; set; } = 160;
}
