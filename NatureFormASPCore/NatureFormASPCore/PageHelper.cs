using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text;

namespace NatureFormASPCore
{
	public class PageHelper
	{
		private PageContext pContext;
		private string caption;
		private string content;
		//private string defaultChildrensSortingCriteria = "price";

		public PageHelper(HttpContext httpContext, string pageCode, string lang)
		{
			try
			{
				pContext = httpContext.RequestServices.GetService(typeof(PageContext)) as PageContext;
				PageEntity p = pContext.GetEntity(pageCode);

				if (p.AliasOf != null)
				{
					PageEntity truePage = pContext.GetEntity(p.AliasOf);
					CaptionAndContentFilling(lang, truePage);
					string redirectUrl = string.Format("/{0}/{1}",p.AliasOf, lang);
					httpContext.Response.Redirect(redirectUrl, true);
				}
				else
				{
					CaptionAndContentFilling(lang, p);

					if (p.IsContainer != null)
					{
						if (p.IsContainer == true)
						{
							string childrensContent = GetChildrensContent(p, lang);
							content = content.Replace("~1", childrensContent);
						}
					}
				}
			}
			catch(NullReferenceException ne)
			{
				caption = ne.Message + " Some of the parameters for PageHelper constructor have had empty value!";
			}
			catch(Exception ex)
			{
				//for debugging
				caption = ex.Message;
			}
		}

		public string Render()
		{
			if (caption != null)
			{
				return caption + content;
			}
			else
			{
				return content;
			}
		}

		/*private string GetChildrensContent(PageEntity p, string lang)
		{
			return GetChildrensContent(p, lang, defaultChildrensSortingCriteria);
		}*/

		private string GetChildrensContent(PageEntity p, string lang)
		{ 
			string childrensContent = string.Empty;
			if (p.PlaceNum == null)
			{
				List<PageEntity> children = pContext.GetChildEntities(p.PageCode, p.OrderByCriteria);//sortingCriteria);
				StringBuilder builder = new StringBuilder();

				foreach (PageEntity pE in children)
				{
					string buf = string.Empty;

					if (pE.AliasOf != null)
					{
						PageEntity truePage = pContext.GetEntity(pE.AliasOf);
						switch (lang)
						{
							case "UA":
								buf = truePage.ContentUA;
								break;
							case "RU":
								buf = truePage.ContentRU;
								break;
							case "EN":
								buf = truePage.ContentEN;
								break;
						}
						buf = buf.Replace("~2", truePage.Price.ToString());
					}
					else
					{
						switch (lang)
						{
							case "UA":
								buf = pE.ContentUA;
								break;
							case "RU":
								buf = pE.ContentRU;
								break;
							case "EN":
								buf = pE.ContentEN;
								break;
						}
						buf = buf.Replace("~2", pE.Price.ToString());
					}

					builder.Append(buf);
				}

				childrensContent = builder.ToString();
			}

			return childrensContent;
		}

		private void CaptionAndContentFilling(string lang, PageEntity p)
		{ 
			string buf = string.Empty;
			switch (lang)
			{
				case "UA":
					if (p.CaptionUA != null)
					{
						caption = p.CaptionUA;
					}
					buf = p.ContentUA;
					break;
				case "RU":
					if (p.ContentRU != null)
					{
						caption = p.CaptionRU;
					}
					buf = p.ContentRU;
					break;
				case "EN":
					if (p.CaptionEN != null)
					{
						caption = p.CaptionEN;
					}
					buf = p.ContentEN;
					break;
				default:
					throw new NullReferenceException("Language == null!");
			}
			buf = buf.Replace("~2", p.Price.ToString());
			content = buf;

		}


	}
}