using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Gif.Components;

namespace Example
{
	class ExampleMain
	{
		[STAThread]
		static void Main(string[] args)
		{
			/* create Gif */
			//you should replace filepath
			string path = Directory.GetCurrentDirectory() + "\\..\\..\\..";
            path = Path.GetFullPath(path);
			String [] imageFilePaths = new String[]{ path + "\\Res\\01.png", path +"\\Res\\02.png", path + "\\Res\\03.png" }; 
			String outputFilePath = path + "\\Res\\test.gif";
			AnimatedGifEncoder e = new AnimatedGifEncoder();

			// read file as memorystream
			MemoryStream memStream = new MemoryStream();
            e.Start(memStream);
			e.SetDelay(500);
			//-1:no repeat,0:always repeat
			e.SetRepeat(0);
			foreach(var imageFilePath in imageFilePaths)
			{
				e.AddFrame( Image.FromFile(imageFilePath) );
			}
			e.Finish();
			File.WriteAllBytes(outputFilePath, memStream.ToArray());
			/* extract Gif */
			string outputPath = path;
			GifDecoder gifDecoder = new GifDecoder();
			gifDecoder.Read( path + "\\Res\\test.gif");
			for ( int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++ ) 
			{
				Image frame = gifDecoder.GetFrame( i );  // frame i
				frame.Save( outputPath + Guid.NewGuid().ToString() + ".png", ImageFormat.Png );
			}
		}
	}
}
