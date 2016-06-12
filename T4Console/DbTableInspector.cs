using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace T4Console
{
    public static class DbTableInspector
    {
        public static IEnumerable<DbTableMetadata> EnumerateDbTables()
        {
            List<DbTableMetadata> tables = new List<DbTableMetadata>();
            SqlDbTypeFinder dbTypeFinder = new SqlDbTypeFinder();

            using (var connection = new SqlConnection("initial catalog=Northwind; integrated security=SSPI"))
            {
                var command = new SqlCommand(@"
                    SELECT t.name, ISNULL(ep2.value, '') AS TableDesc, c.name, 
                           dt.user_type_id, dt.name, c.max_length, c.is_nullable, 
                           c.is_identity, c.is_computed,
	                       CASE WHEN idx.key_ordinal IS NULL THEN 0 ELSE 1 END AS Idx,
	                       ISNULL((
	                          SELECT idx.is_primary_key 
                              FROM sys.index_columns idxc INNER JOIN sys.indexes idx ON idxc.index_id = idx.index_id
		                      WHERE idx.name = k.name AND idxc.object_id = t.object_id AND idxc.column_id = c.column_id
	                       ), 0) AS IsPrimaryKey,
                           ISNULL(ep.value, '') AS ColumnDesc
                    FROM sys.all_columns c 
                         INNER JOIN sys.tables t ON c.object_id = t.object_id
                         INNER JOIN sys.types dt ON c.user_type_id = dt.user_type_id
					     LEFT JOIN sys.index_columns idx ON t.object_id = idx.object_id AND c.column_id = idx.column_id
					     LEFT JOIN sys.key_constraints k ON c.object_id = k.parent_object_id
					     LEFT JOIN sys.extended_properties ep ON c.object_id = ep.major_id AND c.column_id = ep.minor_id AND ep.name = 'MS_Description'
					     LEFT JOIN sys.extended_properties ep2 ON c.object_id = ep2.major_id AND ep2.minor_id = 0 AND ep2.name = 'Schema_Description'
                    WHERE t.type = 'U'
                    ORDER BY t.name, c.column_id", connection);

                connection.Open();
                IDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.CloseConnection);
                DbTableMetadata table = null;

                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    string tableDesc = reader.GetString(1);
                    // detect table property.
                    if (table == null)
                    {
                        table = new DbTableMetadata();
                        table.TableName = tableName;
                        table.LegalEntityName = tableName.Replace(" ", "_");
                        table.Columns = new List<DbColumnMetadata>();
                        table.Description = tableDesc;
                    }
                    else if (string.Compare(table.TableName, tableName, StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        // add table into tables.
                        tables.Add(table);
                        // create new instance for next table.
                        table = new DbTableMetadata();
                        table.Columns = new List<DbColumnMetadata>();
                        table.TableName = tableName;
                        table.LegalEntityName = tableName.Replace(" ", "_");
                        table.Description = tableDesc;
                    }

                    // load column information.
                    string columnName = reader.GetString(2);
                    int dbTypeNumber = reader.GetInt32(3);
                    int length = Convert.ToInt32(reader.GetValue(5));
                    bool IsNullable = reader.GetBoolean(6);
                    bool IsIdentity = reader.GetBoolean(7);
                    bool IsComputed = reader.GetBoolean(8);
                    bool IsPrimaryKey = Convert.ToBoolean(reader.GetValue(10));
                    string columnDesc = reader.GetString(11);
                    DbType dbType;

                    if (!dbTypeFinder.FindByTypeNumber(dbTypeNumber, false, out dbType))
                        throw new InvalidOperationException("InvalidDbTypeNumber:" + dbTypeNumber.ToString());

                    // workaround: ignore column had been added.
                    if (!table.Columns.Where(c => c.ColumnName == columnName).Any())
                    {
                        table.Columns.Add(new DbColumnMetadata()
                        {
                            ColumnDbType = dbType,
                            ColumnName = columnName,
                            Description = columnDesc,
                            LegalColumnName = columnName.Replace(" ", "_"),
                            IsComputed = IsComputed,
                            IsIdentity = IsIdentity,
                            IsPrimaryKey = IsPrimaryKey,
                            IsNullable = IsNullable,
                            Length = length
                        });
                    }
                }

                reader.Close();
            }

            return tables;
        }
    }
}
