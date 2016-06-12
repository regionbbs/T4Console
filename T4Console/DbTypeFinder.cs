using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace T4Console
{
    public class SqlDbTypeMap
    {
        public int DbTypeNumber { get; internal set; }
        public SqlDbType SqlDbType { get; internal set; }
        public DbType DbType { get; internal set; }
        public Type ClrType { get; internal set; }
        public bool Default { get; internal set; }
    }

    public class SqlDbTypeFinder
    {
        private static List<SqlDbTypeMap> sDbTypeMaps = new List<SqlDbTypeMap>();

        static SqlDbTypeFinder()
        {
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 34,
                SqlDbType = SqlDbType.Image,
                DbType = DbType.Binary,
                ClrType = typeof(byte[]),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 35,
                SqlDbType = SqlDbType.Text,
                DbType = DbType.String,
                ClrType = typeof(string),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 36,
                SqlDbType = SqlDbType.UniqueIdentifier,
                DbType = DbType.Guid,
                ClrType = typeof(Guid),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 40,
                SqlDbType = SqlDbType.Date,
                DbType = DbType.Date,
                ClrType = typeof(DateTime),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 41,
                SqlDbType = SqlDbType.Time,
                DbType = DbType.Time,
                ClrType = typeof(TimeSpan),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 42,
                SqlDbType = SqlDbType.DateTime2,
                DbType = DbType.DateTime2,
                ClrType = typeof(DateTime),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 43,
                SqlDbType = SqlDbType.DateTimeOffset,
                DbType = DbType.DateTimeOffset,
                ClrType = typeof(DateTimeOffset),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 48,
                SqlDbType = SqlDbType.TinyInt,
                DbType = DbType.Byte,
                ClrType = typeof(byte),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 52,
                SqlDbType = SqlDbType.SmallInt,
                DbType = DbType.Int16,
                ClrType = typeof(short),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 56,
                SqlDbType = SqlDbType.Int,
                DbType = DbType.Int32,
                ClrType = typeof(int),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 58,
                SqlDbType = SqlDbType.SmallDateTime,
                DbType = DbType.DateTime,
                ClrType = typeof(DateTime),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 59,
                SqlDbType = SqlDbType.Real,
                DbType = DbType.Single,
                ClrType = typeof(float),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 60,
                SqlDbType = SqlDbType.Money,
                DbType = DbType.Decimal,
                ClrType = typeof(decimal),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 61,
                SqlDbType = SqlDbType.DateTime,
                DbType = DbType.DateTime,
                ClrType = typeof(DateTime),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 62,
                SqlDbType = SqlDbType.Float,
                DbType = DbType.Double,
                ClrType = typeof(double),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 98,
                SqlDbType = SqlDbType.Variant,
                DbType = DbType.Object,
                ClrType = typeof(object),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 99,
                SqlDbType = SqlDbType.NText,
                DbType = DbType.String,
                ClrType = typeof(string),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 104,
                SqlDbType = SqlDbType.Bit,
                DbType = DbType.Boolean,
                ClrType = typeof(bool),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 106,
                SqlDbType = SqlDbType.Decimal,
                DbType = DbType.Decimal,
                ClrType = typeof(decimal),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 108,
                SqlDbType = SqlDbType.Decimal,
                DbType = DbType.Decimal,
                ClrType = typeof(decimal),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 122,
                SqlDbType = SqlDbType.SmallMoney,
                DbType = DbType.Decimal,
                ClrType = typeof(decimal),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 127,
                SqlDbType = SqlDbType.BigInt,
                DbType = DbType.Int64,
                ClrType = typeof(long),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 165,
                SqlDbType = SqlDbType.VarBinary,
                DbType = DbType.Binary,
                ClrType = typeof(byte[]),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 167,
                SqlDbType = SqlDbType.VarChar,
                DbType = DbType.AnsiString,
                ClrType = typeof(string),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 173,
                SqlDbType = SqlDbType.Binary,
                DbType = DbType.Binary,
                ClrType = typeof(byte[]),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 175,
                SqlDbType = SqlDbType.Char,
                DbType = DbType.AnsiString,
                ClrType = typeof(char),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 189,
                SqlDbType = SqlDbType.Timestamp,
                DbType = DbType.Binary,
                ClrType = typeof(byte[]),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 231,
                SqlDbType = SqlDbType.NVarChar,
                DbType = DbType.String,
                ClrType = typeof(string),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 239,
                SqlDbType = SqlDbType.NChar,
                DbType = DbType.String,
                ClrType = typeof(char),
                Default = false
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 241,
                SqlDbType = SqlDbType.Xml,
                DbType = DbType.Xml,
                ClrType = typeof(XmlDocument),
                Default = true
            });
            sDbTypeMaps.Add(new SqlDbTypeMap()
            {
                DbTypeNumber = 256,
                SqlDbType = SqlDbType.VarChar,
                DbType = DbType.AnsiString,
                ClrType = typeof(string),
                Default = false
            });
        }

        public bool FindByDbTypeNumber(int DbTypeNumber, out SqlDbType DbType)
        {
            DbType = SqlDbType.Variant;
            var query = sDbTypeMaps.Where(t => t.DbTypeNumber == DbTypeNumber && t.Default);

            if (query.Any())
            {
                DbType = query.First().SqlDbType;
                return true;
            }
            else
                return false;
        }

        public bool FindByTypeNumber(int DbTypeNumber, bool ReturnDefaultOnly, out DbType DbType)
        {
            DbType = DbType.Object;
            IEnumerable<SqlDbTypeMap> query = null;

            if (ReturnDefaultOnly)
                query = sDbTypeMaps.Where(t => t.DbTypeNumber == DbTypeNumber && t.Default);
            else
                query = sDbTypeMaps.Where(t => t.DbTypeNumber == DbTypeNumber);

            if (query.Any())
            {
                DbType = query.First().DbType;
                return true;
            }
            else
                return false;
        }

        public bool FindByTypeNumber(int DbTypeNumber, out Type ClrType)
        {
            ClrType = null;
            var query = sDbTypeMaps.Where(t => t.DbTypeNumber == DbTypeNumber && t.Default);

            if (query.Any())
            {
                ClrType = query.First().ClrType;
                return true;
            }
            else
                return false;
        }

        public bool FindBySqlDbType(SqlDbType DbType, out int TypeNumber)
        {
            TypeNumber = 0;
            var query = sDbTypeMaps.Where(t => t.SqlDbType == DbType && t.Default);

            if (query.Any())
            {
                TypeNumber = query.First().DbTypeNumber;
                return true;
            }
            else
                return false;
        }

        public bool FindBySqlDbType(SqlDbType SqlDbType, out DbType OutDbType)
        {
            OutDbType = DbType.Object;
            var query = sDbTypeMaps.Where(t => t.SqlDbType == SqlDbType && t.Default);

            if (query.Any())
            {
                OutDbType = query.First().DbType;
                return true;
            }
            else
                return false;
        }

        public bool FindBySqlDbType(SqlDbType DbType, out Type ClrType)
        {
            ClrType = null;
            var query = sDbTypeMaps.Where(t => t.SqlDbType == DbType && t.Default);

            if (query.Any())
            {
                ClrType = query.First().ClrType;
                return true;
            }
            else
                return false;
        }

        public bool FindByDbType(DbType DbType, out int TypeNumber)
        {
            TypeNumber = 0;
            var query = sDbTypeMaps.Where(t => t.DbType == DbType && t.Default);

            if (query.Any())
            {
                TypeNumber = query.First().DbTypeNumber;
                return true;
            }
            else
                return false;
        }

        public bool FindByDbType(DbType DbType, out SqlDbType SqlDbType)
        {
            SqlDbType = SqlDbType.Variant;
            var query = sDbTypeMaps.Where(t => t.DbType == DbType && t.Default);

            if (query.Any())
            {
                SqlDbType = query.First().SqlDbType;
                return true;
            }
            else
                return false;
        }

        public bool FindByDbType(DbType DbType, out Type ClrType)
        {
            ClrType = null;
            var query = sDbTypeMaps.Where(t => t.DbType == DbType && t.Default);

            if (query.Any())
            {
                ClrType = query.First().ClrType;
                return true;
            }
            else
                return false;
        }

        public bool FindByClrType(Type ClrType, out DbType DbType)
        {
            DbType = DbType.Object;
            var query = sDbTypeMaps.Where(t => t.ClrType == ClrType && t.Default);

            if (query.Any())
            {
                if (query.Count() > 1)
                {
                    var typeQuery = query.ToList().OrderBy(t => Marshal.SizeOf(t));
                    DbType = typeQuery.First().DbType;
                    return true;
                }
                else
                    DbType = query.First().DbType;

                return true;
            }
            else
                return false;
        }

        public bool FindByClrType(Type ClrType, out SqlDbType DbType)
        {
            DbType = SqlDbType.Variant;
            var query = sDbTypeMaps.Where(t => t.ClrType == ClrType && t.Default);

            if (query.Any())
            {
                if (query.Count() > 1)
                {
                    var typeQuery = query.ToList().OrderBy(t => Marshal.SizeOf(t));
                    DbType = typeQuery.First().SqlDbType;
                    return true;
                }
                else
                    DbType = query.First().SqlDbType;

                return true;
            }
            else
                return false;
        }

        public bool FindByClrType(Type ClrType, out int TypeNumber)
        {
            TypeNumber = 0;
            var query = sDbTypeMaps.Where(t => t.ClrType == ClrType && t.Default);

            if (query.Any())
            {
                if (query.Count() > 1)
                {
                    var typeQuery = query.ToList().OrderBy(t => Marshal.SizeOf(t));
                    TypeNumber = typeQuery.First().DbTypeNumber;
                    return true;
                }
                else
                    TypeNumber = query.First().DbTypeNumber;

                return true;
            }
            else
                return false;
        }
    }
}
