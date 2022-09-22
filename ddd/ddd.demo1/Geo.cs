public record Geo
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    public Geo(double longitude, double latitude)
    {
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("longtitude invalid");
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("longtitude invalid");

        this.Longitude = longitude;
        this.Latitude = latitude;
    }
}