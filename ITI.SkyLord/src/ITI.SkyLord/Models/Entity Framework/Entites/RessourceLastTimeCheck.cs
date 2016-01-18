using System;
using System.ComponentModel.DataAnnotations;


namespace ITI.SkyLord
{
    public class RessourceLastTimeCheck
    {
        [Key]
        public long IslandId { get; set; }
        public DateTime CheckTime { get; set; }
    }
}
