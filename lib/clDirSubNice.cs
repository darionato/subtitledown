using System;
using System.IO;

namespace Badlydone.Subtitledown
{
	
	public class clDirSubNice
	{
		
		private string m_Directory = string.Empty;
		private clSubtitledown m_SubDown;
        private bool m_Sincrono = false;
        private bool m_Ricorsivo = false;

		public clDirSubNice()
		{
			m_SubDown = new clSubtitledown();
		}

        public bool Sincrono
        {
            get { return m_Sincrono; }
            set { m_Sincrono = value; }
        }
		
		public string Directory
		{
			get { return m_Directory; }
			set { m_Directory = value; }
		}

        public bool Ricorsivo
        {
            get { return m_Ricorsivo; }
            set { m_Ricorsivo = value; }
        }

        public void WorkOutDir()
        {
            this.WorkOutSubDir(m_Directory);
        }

		public void WorkOutSubDir(string directory)
		{

            m_SubDown.DirDownload = directory;

			clFileTitleName Parse_video = new clFileTitleName();

            DirectoryInfo info_dir = new DirectoryInfo(directory);
			
            foreach(FileInfo sto_file in info_dir.GetFiles("*.avi"))
            {
                if (File.Exists(Path.Combine(directory, Path.GetFileNameWithoutExtension(sto_file.FullName) + ".srt")) == false)
                {

                    clInfoSerie serie = Parse_video.getInfoSerie(sto_file.Name);
                    if (serie.Telefilm.Length > 0)
                    {
                        m_SubDown.InfoSerie = serie;

                        if (m_Sincrono)
                        {
                            m_SubDown.DownloadSub();
                        }
                        else
                        {
                            m_SubDown.DownloadSubAsynch();
                            m_SubDown.WaitDone();
                        }
						
                    }

                }

            }

            if (m_Ricorsivo == true)
            {
                foreach (DirectoryInfo the_dir in info_dir.GetDirectories())
                {
                    this.WorkOutSubDir(the_dir.FullName);
                }
            }
			
			
		}
		
	}
	
}
