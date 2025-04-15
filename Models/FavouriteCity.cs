using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace TheWeatherApp.Models
{
    [Table("favourite_cities")]
    public class FavouriteCity : BaseModel
    {
        [Column("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Column("email")]
        public string Email { get; set; }

        [Column("city_name")]
        public string CityName { get; set; }

        [Column("temperature")]
        public double Temperature { get; set; }

        [Column("condition")]
        public string Condition { get; set; }

        [Column("date_added")]
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}

