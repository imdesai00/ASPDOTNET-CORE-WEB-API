using System;
using System.Collections.Generic;

namespace adotoaspcorewebapi.Models;

public partial class CityMaster
{
    internal int CityID;

    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public int StateId { get; set; }

    public int CountryId { get; set; }

}
