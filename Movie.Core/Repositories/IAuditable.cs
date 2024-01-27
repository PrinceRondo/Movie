using System;

namespace Movie.Core.Repositories
{
    public interface IAuditable
    {
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        bool IsDeleted { get; set; }

    }
}