using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace NatureFormASPCore
{
	public interface IDbContext<T>
	{
		//MySqlConnection GetConnection();
		List<T> GetAllEntities();
		T GetEntity(string page);
	}
}