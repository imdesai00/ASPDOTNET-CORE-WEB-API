﻿using System;
using System.Collections.Generic;

namespace adotoaspcorewebapi.Models;

public partial class CountryMaster
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;
}
