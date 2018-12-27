using LiteDB;
using System;

namespace ScalableWeb.Diff.Data.Acess.Contexts
{
    public class DatabaseContext : IDisposable
    {
        protected LiteDatabase Database { get; private set; }

        public DatabaseContext()
        {
            Database = new LiteDatabase(@"Diff.db");
        }

        public void Dispose()
        {
            Database?.Dispose();
        }
    }
}