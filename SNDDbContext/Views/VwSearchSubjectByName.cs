﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SNDDbContext
{
    public partial class VwSearchSubjectByName
    {
        public long Code { get; set; }
        public string Name { get; set; }
        public string AdminCenterName { get; set; }
        public double Population { get; set; }
        public double Square { get; set; }
        public long District { get; set; }
    }
}