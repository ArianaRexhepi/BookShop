using System;
using System.Collections.Generic;

namespace Books.Areas.Identity.Data
{
    public partial class EfmigrationsHistory
    {
        public string MigrationId { get; set; } = null!;
        public string ProductVersion { get; set; } = null!;
    }
}
