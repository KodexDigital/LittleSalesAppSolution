using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace VideoPLayerTesting.CustomResult
{
	public class VideoResult : ActionResult
	{
		private readonly IWebHostEnvironment webHostEnvironment;

		public VideoResult(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}
		
		
		
		/// <summary>
	  /// The below method will respond with the Video file
	  /// </summary>
	  /// <param name="context"></param>
		//public override void ExecuteResult(ControllerContext context)
		//{
		//	//The File Path
		//	var videoFilePath = HostingEnvironment.MapPath("~/VideoFile/Win8.mp4");
		//	//The header information
		//	context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Win8.mp4");
		//	var file = new FileInfo(videoFilePath);
		//	//Check the file exist,  it will be written into the response
		//	if (file.Exists)
		//	{
		//		var stream = file.OpenRead();
		//		var bytesinfile = new byte[stream.Length];
		//		stream.Read(bytesinfile, 0, (int)file.Length);
		//		context.HttpContext.Response.BinaryWrite(bytesinfile);
		//	}
		//}

		public override void ExecuteResult(ActionContext context)
		{
			var videoFilePath = webHostEnvironment.ContentRootPath;
			var filePath = Path.Combine(videoFilePath, "/VideoFile/Win8.mp4");
			context.HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=Win8.mp4");
			var file = new FileInfo(filePath);
			if (file.Exists)
			{
				var stream = file.OpenRead();
				var bytesinfile = new byte[stream.Length];
				stream.Read(bytesinfile, 0, (int)file.Length);
				context.HttpContext.Response.WriteAsync(bytesinfile);
			}
		}


	}
}
