using System;
using MySql.Data.MySqlClient;

namespace NatureFormASPCore
{
	public static class ExtensionClass
	{
		public static int? SafeGetInt32(this MySqlDataReader reader, string columnName)
		{
			int ordinal = reader.GetOrdinal(columnName);

			if (!reader.IsDBNull(ordinal))
			{
				return reader.GetInt32(ordinal);
			}
			else
			{
				//return defaultValue;
				//throw new Exception(string.Format("column {0} has dbNull value", columnName));
				return null;
			}
		}

		public static string SafeGetString(this MySqlDataReader reader, string columnName)
		{
			int ordinal = reader.GetOrdinal(columnName);

			if (!reader.IsDBNull(ordinal))
			{
				return reader.GetString(ordinal);
			}
			else
			{
				return null;
			}
		}

		public static double? SafeGetDouble(this MySqlDataReader reader, string columnName)
		{
			int ordinal = reader.GetOrdinal(columnName);

			if (!reader.IsDBNull(ordinal))
			{
				return reader.GetDouble(ordinal);
			}
			else
			{
				return null;
			}
		}

		public static bool? SafeGetBoolean(this MySqlDataReader reader, string columnName)
		{
			int ordinal = reader.GetOrdinal(columnName);

			if (!reader.IsDBNull(ordinal))
			{
				return reader.GetBoolean(ordinal);
			}
			else
			{
				return null;
			}
		}

		public static DateTime? SafeGetDateTime(this MySqlDataReader reader, string columnName)
		{
			int ordinal = reader.GetOrdinal(columnName);

			if (!reader.IsDBNull(ordinal))
			{
				return reader.GetDateTime(ordinal);
			}
			else
			{
				return null;
			}
		}
	}
}
