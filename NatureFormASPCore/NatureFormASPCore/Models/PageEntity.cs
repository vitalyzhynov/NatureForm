using System;
using Microsoft.AspNetCore.Http;

namespace NatureFormASPCore
{
	public class PageEntity
	{
		public PageEntity()
		{ 
			
		}

		public int? Id { get; set; }

		public string PageCode { get; set; }

		public string CaptionUA { get; set; }

		public string CaptionRU { get; set; }

		public string CaptionEN { get; set; }

		public string ContentUA { get; set; }

		public string ContentRU { get; set; }

		public string ContentEN { get; set; }

		public string IntroUA { get; set; }

		public string IntroRU { get; set; }

		public string IntroEN { get; set; }

		public string Image { get; set; }

		public string ImageBig { get; set; }

		public DateTime? EditDate { get; set; }

		public DateTime? CreateDate { get; set; }

		public string ParentCode { get; set; }

		public string OrderByCriteria { get; set; }

		public int? PlaceNum { get; set; }

		public bool? IsContainer { get; set; }

		public double? Price { get; set; }

		public string AliasOf { get; set; }
	}
}
