using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace NatureFormASPCore
{
	public class PageContext : IDbContext<PageEntity>
  	{
	    public string ConnectionString { get; set; }
	 
	    public PageContext(string connectionString)
	    {
	      this.ConnectionString = connectionString;
	    }
	 
	    private MySqlConnection GetConnection()
	    {
	      return new MySqlConnection(ConnectionString);
	    }
	 
	    public List<PageEntity> GetAllEntities()
	    {
	    	List<PageEntity> list = new List<PageEntity>();
	 
	    	using (MySqlConnection conn = GetConnection())
	    	{
	        	conn.Open();
	        	MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pages", conn);
		        using (MySqlDataReader reader = cmd.ExecuteReader())
		        {
		          while (reader.Read())
		          {
						
		            list.Add(new PageEntity()
		            {
						Id = reader.SafeGetInt32("id"),	
						PageCode = reader.SafeGetString("pageCode"),
						CaptionUA = reader.SafeGetString("captionUA"),
						CaptionRU = reader.SafeGetString("captionRU"),
						CaptionEN = reader.SafeGetString("captionEN"),
						ContentUA = reader.SafeGetString("contentUA"),	
						ContentRU = reader.SafeGetString("contentRU"),	
						ContentEN = reader.SafeGetString("contentEN"),
						IntroUA = reader.SafeGetString("introUA"),
						IntroRU = reader.SafeGetString("introRU"),
						IntroEN = reader.SafeGetString("introEN"),
						Image = reader.SafeGetString("image"),	
						ImageBig = reader.SafeGetString("imageBig"),
						EditDate = reader.SafeGetDateTime("editDate"),
						CreateDate = reader.SafeGetDateTime("createDate"),
						ParentCode = reader.SafeGetString("parentCode"),
						OrderByCriteria = reader.SafeGetString("orderByCriteria"),
						PlaceNum = reader.SafeGetInt32("placeNum"),
						IsContainer = reader.SafeGetBoolean("isContainer"),
						Price = reader.SafeGetDouble("price"),
						AliasOf = reader.SafeGetString("aliasOf")
		            });
		          }
		        }
	      	}
 
      		return list;
    	}

		public PageEntity GetEntity(string page)
		{
			PageEntity result = null;

			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				//MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pages WHERE pageCode='" + page + "'", conn);
				MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pages WHERE pageCode=@page", conn);
				cmd.Parameters.Add("@page", MySqlDbType.String);
				cmd.Parameters["@page"].Value = page;
				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					try
					{
						while (reader.Read())
						{

							result = new PageEntity()
							{
								Id = reader.SafeGetInt32("id"),
								PageCode = reader.SafeGetString("pageCode"),
								CaptionUA = reader.SafeGetString("captionUA"),
								CaptionRU = reader.SafeGetString("captionRU"),
								CaptionEN = reader.SafeGetString("captionEN"),
								ContentUA = reader.SafeGetString("contentUA"),
								ContentRU = reader.SafeGetString("contentRU"),
								ContentEN = reader.SafeGetString("contentEN"),
								IntroUA = reader.SafeGetString("introUA"),
								IntroRU = reader.SafeGetString("introRU"),
								IntroEN = reader.SafeGetString("introEN"),
								Image = reader.SafeGetString("image"),
								ImageBig = reader.SafeGetString("imageBig"),
								EditDate = reader.SafeGetDateTime("editDate"),
								CreateDate = reader.SafeGetDateTime("createDate"),
								ParentCode = reader.SafeGetString("parentCode"),
								OrderByCriteria = reader.SafeGetString("orderByCriteria"),
								PlaceNum = reader.SafeGetInt32("placeNum"),
								IsContainer = reader.SafeGetBoolean("isContainer"),
								Price = reader.SafeGetDouble("price"),
								AliasOf = reader.SafeGetString("aliasOf")
							};
						}
					}
					catch (Exception ex)
					{
						throw new Exception(string.Format("Trouble with reading data from dbtable! With parameters page: {0} " + ex.Message, page));
					}
				}
			}

			return result;
		}

		public List<PageEntity> GetChildEntities(string parentCode, string orderByCriteria)
		{
			List<PageEntity> list = new List<PageEntity>();

			using (MySqlConnection conn = GetConnection())
			{
				conn.Open();
				MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pages WHERE parentCode=@page ORDER BY " + orderByCriteria, conn);
				cmd.Parameters.Add("@page", MySqlDbType.String);
				cmd.Parameters["@page"].Value = parentCode;
				/*cmd.Parameters.Add("@criteria", MySqlDbType.String);
				cmd.Parameters["@criteria"].Value = orderByCriteria;*/

				using (MySqlDataReader reader = cmd.ExecuteReader())
				{
					try
					{
						while (reader.Read())
						{

							list.Add(new PageEntity()
							{
								Id = reader.SafeGetInt32("id"),
								PageCode = reader.SafeGetString("pageCode"),
								CaptionUA = reader.SafeGetString("captionUA"),
								CaptionRU = reader.SafeGetString("captionRU"),
								CaptionEN = reader.SafeGetString("captionEN"),
								ContentUA = reader.SafeGetString("contentUA"),
								ContentRU = reader.SafeGetString("contentRU"),
								ContentEN = reader.SafeGetString("contentEN"),
								IntroUA = reader.SafeGetString("introUA"),
								IntroRU = reader.SafeGetString("introRU"),
								IntroEN = reader.SafeGetString("introEN"),
								Image = reader.SafeGetString("image"),
								ImageBig = reader.SafeGetString("imageBig"),
								EditDate = reader.SafeGetDateTime("editDate"),
								CreateDate = reader.SafeGetDateTime("createDate"),
								ParentCode = reader.SafeGetString("parentCode"),
								OrderByCriteria = reader.SafeGetString("orderByCriteria"),
								PlaceNum = reader.SafeGetInt32("placeNum"),
								IsContainer = reader.SafeGetBoolean("isContainer"),
								Price = reader.SafeGetDouble("price"),
								AliasOf = reader.SafeGetString("aliasOf")
							});
						}
					}
					catch (Exception ex)
					{ 
						throw new Exception(string.Format("Trouble with reading data from dbtable! With parameters page: {0} criteria:D {1}" + ex.Message, parentCode, orderByCriteria));
					}

				}
			}

			return list;
		}
  	}
}
