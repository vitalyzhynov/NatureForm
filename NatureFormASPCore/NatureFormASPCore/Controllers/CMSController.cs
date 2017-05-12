using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NatureFormASPCore
{
	//[Route("render")]
	public class CMSController : Controller
    {
		private readonly IViewRenderService _viewRenderService;

		public CMSController(IViewRenderService viewRenderService)
		{
			_viewRenderService = viewRenderService;
		}

		[Route("{page}/{lang}")] //Adding ? ? for more flexible routing 
		public IActionResult Page(string page, string lang)
        {
			if (page != null)
			{
				//1)
				if (lang == null)
				{
					lang = "UA";
				}
			}
			else
			{
				page = "about";
			}

			//ViewData["PageCode"] = page;
			ViewData["Language"] = lang;

			//2)
			//PageContext pContext = HttpContext.RequestServices.GetService(typeof(PageContext)) as PageContext;
			//PageEntity p = pContext.GetEntity(page);
			PageHelper helper = new PageHelper(HttpContext, page, lang);

			return View("Page", helper.Render());
			//return View ("Page", p.ContentUA);
			//return "test";
        }

		[Route("invite")]
		public async Task<IActionResult> RenderInviteView()
		{
			ViewData["Message"] = "Your application description page.";
			var viewModel = new PageEntity
			{
				Id = 2,
				PageCode = "someCode",
				ContentUA = "fskjgsdgnkadsngkjsdkgnjkasdn"
			};

			var result = await _viewRenderService.RenderToStringAsync("Email/Invite", viewModel);
			return Content(result);
		}
    }
}
