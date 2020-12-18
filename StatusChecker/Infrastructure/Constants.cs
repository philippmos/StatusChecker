using System;
using System.IO;

public static class Constants
{
    public const string DatabaseFilename = "StatusCheckerSQLite.sqlite";

    public const SQLite.SQLiteOpenFlags Flags =

        SQLite.SQLiteOpenFlags.ReadWrite |

        SQLite.SQLiteOpenFlags.Create |

        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath
    {
        get
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, DatabaseFilename);
        }
    }
}