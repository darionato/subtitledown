// MyClass.cs created with MonoDevelop
// User: dario at 3:05 PM 4/12/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Text.RegularExpressions;

namespace Badlydone.Subtitledown
{
	
	public class clFileTitleName
	{
		
		private Regex m_regExX;
		private Regex m_regExSE;
		
		public clFileTitleName()
		{
			m_regExX = new Regex("[0-9]x[0-9]{2}",RegexOptions.IgnoreCase);
			m_regExSE = new Regex("S[0-9]{2}E[0-9]{2}",RegexOptions.IgnoreCase);
		}
		
		private int getSerie(string parte)
		{
			if (parte.ToLower().Contains("x"))
			{
				return Convert.ToInt32(parte.Substring(0,parte.ToLower().IndexOf("x")));
			}
			else
			{
				return Convert.ToInt32(parte.Substring(1,parte.ToLower().IndexOf("e")-1));
			}
			
		}
		
		private int getPuntata(string parte)
		{
			if (parte.ToLower().Contains("x"))
			{
				return Convert.ToInt32(parte.Substring(parte.ToLower().IndexOf("x")+1));
			}
			else
			{
				return Convert.ToInt32(parte.Substring(parte.ToLower().IndexOf("e")+1));
			}
		}
		
		private string getTelefilm(string file,string parte)
		{
			string tmp = file.Substring(0,file.IndexOf(parte));
			tmp = tmp.Replace("_"," ").Replace("."," ").Trim();
			return tmp;
			
		}
		
		private clInfoSerie getSplitFile(string file, string parte)
		{
			
			clInfoSerie tmp = new clInfoSerie();
            tmp.File = file;
			tmp.Telefilm = getTelefilm(file, parte);
			tmp.Serie = getSerie(parte);
			tmp.Puntata = getPuntata(parte);
			return tmp;
			
		}
		
		public clInfoSerie getInfoSerie(string file)
		{
			
			clInfoSerie tmp_info = null;
			
			Console.WriteLine("File: {0}",file);
			
			MatchCollection found = m_regExX.Matches(file);
			if (found.Count > 0)
			{
				tmp_info = getSplitFile(file, found[0].Groups[0].Value);
				return tmp_info;
			}
			
			
			found = m_regExSE.Matches(file);
			if (found.Count > 0)
			{
				tmp_info = getSplitFile(file, found[0].Groups[0].Value);
				return tmp_info;
			}

			tmp_info = new clInfoSerie();
			return tmp_info;
			
		}
		
	}
	
	public class clInfoSerie
	{

        private string m_File = string.Empty;
		private string m_Telefilm = string.Empty;
		private int m_Serie = 0;
		private int m_Puntata = 0;
		
		public clInfoSerie()
		{
		}
		
		public clInfoSerie(string fileavi, string telefilm, int serie, int puntata)
		{
            m_File = fileavi;
			m_Telefilm = telefilm;
			m_Serie = serie;
			m_Puntata = puntata;
		}

        public string File
        {
            get { return m_File; }
            set { m_File = value; }
        }
		
		public string Telefilm
		{
			get { return m_Telefilm; }
			set { m_Telefilm = value; }
		}
		
		public int Serie
		{
			get {return m_Serie; }
			set {m_Serie = value; }
		}
		
		public int Puntata
		{
			get { return m_Puntata; }
			set { m_Puntata = value; }
		}
		
	}
}
