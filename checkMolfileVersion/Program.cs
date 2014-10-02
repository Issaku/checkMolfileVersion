// Xamarin Studio on MAcOSX 10.9
using System;
using System.IO;
using System.Text ;  // for Encoding
using System.Collections.Generic;
using System.Collections;

namespace checkMolfileVersion
{
	class MainClass
	{
		public static void Main(string[] s)
		{
			StringBuilder sbV2000 = new StringBuilder ();
			StringBuilder sbV3000 = new StringBuilder ();
			StringBuilder sbUn = new StringBuilder ();

			string file_tmp = String.Empty;
			string dirname = String.Empty;

			for (int N = 1; N < Environment.GetCommandLineArgs ().Length; N++) {
				file_tmp = Environment.GetCommandLineArgs () [N];

				dirname = Path.GetDirectoryName(file_tmp);
				string[] files = System.IO.Directory.GetFiles(dirname, "*.mol", System.IO.SearchOption.AllDirectories);

				foreach (var file in files) {
					if (File.Exists (file)) {
						StreamReader reader = new StreamReader (file, Encoding.Default);
						string moldata = reader.ReadToEnd();

						if (moldata.Contains ("V2000")) {
							sbV2000.AppendLine(Path.GetFileName(file));
							Console.WriteLine ("V2000: " + Path.GetFileName(file));
						} else if (moldata.Contains ("V3000")) {
							sbV3000.AppendLine(Path.GetFileName(file));
							Console.WriteLine ("V3000: " + Path.GetFileName(file));
						} else {
							sbUn.AppendLine(Path.GetFileName(file));
							Console.WriteLine ("V????: " + Path.GetFileName(file));
						}
						reader.Close ();
					} 
				}
			}

			DateTime dt = DateTime.Now;
			string dtString = dt.ToString ("yyyyMMdd");

			StreamWriter writerV2000 = new StreamWriter("checkMolfile_V2000_" + dtString + ".txt",
				false,  // 上書き （ true = 追加 ）
				Encoding.UTF8) ;
			writerV2000.Write(sbV2000.ToString()) ;
			writerV2000.Close() ;

			StreamWriter writerV3000 = new StreamWriter("checkMolfile_V3000_" + dtString + ".txt",
				false,  // 上書き （ true = 追加 ）
				Encoding.UTF8) ;
			writerV3000.Write(sbV3000.ToString()) ;
			writerV3000.Close() ;

			StreamWriter writerUn = new StreamWriter("checkMolfile_Un_" + dtString + ".txt",
				false,  // 上書き （ true = 追加 ）
				Encoding.UTF8) ;
			writerUn.Write(sbUn.ToString()) ;
			writerUn.Close() ;



			Console.WriteLine ("finished");
		}
	}
}
