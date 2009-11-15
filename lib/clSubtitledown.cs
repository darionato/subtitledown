using System;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;
using Badlydone.Unzipper;

namespace Badlydone.Subtitledown
{
    public class clSubtitledown
    {
        private BackgroundWorker m_workback = null;
		private bool m_InProgress = false;
		
        private string m_sBaseUrl = string.Empty;
        private string m_sUserName = string.Empty;
        private string m_sPassword = string.Empty;
        private clInfoSerie m_Serie = null;
        private int m_iMainIdx = 0;

        private Regex Engine = null;
        private Regex EpisodeEngine = null;
        private Regex DownloadEngine = null;

        private String m_RegExp = String.Empty;
        private String m_RegExpEpisode = String.Empty;
        private String m_RegExpDownload = String.Empty;
		
		private String m_Dir_Download = String.Empty;
		private String m_Prc_file_down = String.Empty;

        public clSubtitledown()
        {
			
            m_sBaseUrl = @"http://www.italiansubs.net/";
            m_RegExp = "<td><h3>.*?href=\"([^\"]*?)\">([^<]*)</a>";
            //m_RegExpEpisode = "<dd><img.*?href=\"([^\\\"]*?)\">([^<]*)</a>";
            m_RegExpEpisode = "<dd><a href=\"([^\"]*?)\"><img.*?href=\"([^\"]*?)\">([^<]*)</a>";
            m_RegExpDownload = "<a href=\"(.*)\" rel=\"nofollow\">";
            //m_RegExpDownload = ".*rel=\"nofollow\"*?href=\"(.*?fname=([^\\&]*)&amp.*?)>";

            m_sUserName = "aaa";
            m_sPassword = "aaa";
            m_Serie = new clInfoSerie();

            Engine = new Regex(m_RegExp, RegexOptions.IgnoreCase);
            EpisodeEngine = new Regex(m_RegExpEpisode, RegexOptions.IgnoreCase);
            DownloadEngine = new Regex(m_RegExpDownload, RegexOptions.IgnoreCase);

            m_workback = new BackgroundWorker();
            m_workback.WorkerReportsProgress = true;
            m_workback.WorkerSupportsCancellation = true;
            m_workback.DoWork += new DoWorkEventHandler(m_workback_DoWork);
            m_workback.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_workback_RunWorkerCompleted);

        }

        void StartWatcherDirectories()
        {
			throw  new Exception("Method not implemented!");
        }

        void m_workback_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_InProgress = false;
        }

        void m_workback_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DownloadSub();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                m_workback.CancelAsync();
                m_InProgress = false;
            }

        }

		public bool inProgress
		{
			get { return m_InProgress; }
		}
		
		public string DirDownload
		{
			get { return m_Dir_Download;}
			set { m_Dir_Download = value; }
		}
		
		public string FileDownloaded
		{
			get { return m_Prc_file_down; }
		}

        public clInfoSerie InfoSerie
        {
            get { return m_Serie; }
            set { m_Serie = value; }
        }
		
		public void WaitDone()
		{
#if WIN32
			while (m_InProgress)
            {
                System.Windows.Forms.Application.DoEvents();
            }
            while (m_workback.IsBusy)
            {
                System.Windows.Forms.Application.DoEvents();
            }
				
#else
	   		while (m_InProgress)
        	{
				while (GLib.MainContext.Iteration());
        	}
			while (m_workback.IsBusy)
            {
               while (GLib.MainContext.Iteration());
            }
#endif
		}
		
		public void DownloadSubAsynch()
		{

            if (m_workback.IsBusy) return;

            m_InProgress = true;
            m_workback.RunWorkerAsync();
			
		}
		
        public void DownloadSub()
        {
			m_Prc_file_down = string.Empty;

            string loginUri = m_sBaseUrl; // +"index.php";
            string reqString = string.Format("username={0}&passwd={1}&option=com_user&task=login",m_sUserName,m_sPassword);

            WebClient client = new WebClient();

            // open the request
            CookieContainer cc = new CookieContainer();
            Uri website_url = new Uri(loginUri);
            HttpWebRequest loginRequest = (HttpWebRequest)WebRequest.Create(website_url);// + "?" + reqString);

            // create post params
            UTF8Encoding enc = new UTF8Encoding();
            byte[] post = enc.GetBytes(reqString);

            //loginRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.1.3) Gecko/20090824 Firefox/3.5.3 (.NET CLR 3.5.30729)";
            loginRequest.Proxy = null;
            loginRequest.CookieContainer = cc;
            loginRequest.Method = "POST";
            loginRequest.ContentType = "application/x-www-form-urlencoded";
            loginRequest.ContentLength = post.Length;
            

            Stream pass_post = loginRequest.GetRequestStream();
            pass_post.Write(post,0,post.Length);
            pass_post.Close();

            HttpWebResponse loginResponse = (HttpWebResponse)loginRequest.GetResponse();


            WebHeaderCollection headerCookies = new WebHeaderCollection();
            String cookieHeader = "";
            foreach (Cookie c in loginResponse.Cookies)
            {
                cookieHeader += c.Name + "=" + c.Value + "; ";
            }
			
			loginResponse.Close();

            headerCookies.Add("Cookie", cookieHeader);
			
            //add login cookie to webClient
            client.Headers.Add(headerCookies);

            Stream data = null;
            StreamReader reader = null;
            String sPage = String.Empty;


            //check series name
            String seriesUrl = loginUri + "?option=com_remository&itemid=" + m_iMainIdx;
            data = client.OpenRead(seriesUrl);
            reader = new StreamReader(data);
            sPage = reader.ReadToEnd().Replace('\0', ' ');

            String sSeriesPageURL = String.Empty;
            String sSeasonPageURL = String.Empty;
            String sEpisodePageURL = String.Empty;
            
            sSeriesPageURL = matchSeries(sPage, client);

            if (sSeriesPageURL.Length == 0)
            {
                Console.WriteLine("Series not found!");
				m_InProgress = false;
                return;
            } 
            
            Console.WriteLine("Series found!");

			//check the season
            data = client.OpenRead(sSeriesPageURL);
            reader = new StreamReader(data);
            sPage = reader.ReadToEnd().Replace('\0', ' ');

            sSeasonPageURL = matchSeason(sPage, client);

            if (sSeasonPageURL.Length == 0)
            {
                Console.WriteLine("Season not found!");
				m_InProgress = false;
                return;
            }
			
			Console.WriteLine("Season found!");

			//check the episode
            data = client.OpenRead(sSeasonPageURL);
            reader = new StreamReader(data);
            sPage = reader.ReadToEnd().Replace('\0', ' ');

            sEpisodePageURL = matchEpisode(sSeasonPageURL, null, sPage, client);

            if (sEpisodePageURL.Length == 0)
            {
                Console.WriteLine("Episode not found!");
				m_InProgress = false;
                return;
            }

			Console.WriteLine("Episode found!");
			
            data = client.OpenRead(sEpisodePageURL);
            reader = new StreamReader(data);
            sPage = reader.ReadToEnd().Replace('\0', ' ');

            episodeSubtitleDownload(client, sPage);

            client.Dispose();

        }

        private String matchSeries(String sPage, WebClient client)
        {
            MatchCollection matches = Engine.Matches(sPage);
            matches = Engine.Matches(sPage);
            String retValue = String.Empty;

            //load matches in sortedMatchList
            foreach (Match match in matches)
            {
                if (match.Groups[2].Value.ToLower().Trim().CompareTo(m_Serie.Telefilm.ToLower()) == 0)
                {
                    retValue = match.Groups[1].Value;
                    break;
                }

            }
 
            return retValue;
        }

        private string matchSeason(String sPage, WebClient client)
        {
            String retValue = String.Empty;
            MatchCollection matches = Engine.Matches(sPage);

            foreach (Match match in matches)
            {
                if (match.Groups[2].Value.Trim() == "Stagione " + m_Serie.Serie.ToString())
                {
                    retValue = match.Groups[1].Value;
                    break;
                }
            }

            return retValue;
        }

        private String matchEpisode(String url, String sSelectedVersionTag, String sPage, WebClient client)
        {
            
            String retValue = String.Empty;

            //Find default file version
            MatchCollection fileMatches = EpisodeEngine.Matches(sPage);
            

            foreach (Match match in fileMatches)
            {
                String ep = m_Serie.Telefilm + " " + m_Serie.Serie.ToString() + "x" + String.Format("{0:00}", m_Serie.Puntata);
                if (match.Groups[3].Value.Trim().ToLower() == ep.Trim().ToLower())
                {
                    retValue = match.Groups[1].Value;
                    break;
                }
            }
            
            return retValue;
        }

        private void episodeSubtitleDownload(WebClient client, String sPage)
        {
            MatchCollection matches = DownloadEngine.Matches(sPage);

            foreach (Match match in matches)
            {
                String dir = m_Dir_Download;

                String archiveFile = dir + Path.DirectorySeparatorChar +
                        string.Format("{0}.S{1}E{2}.zip",
                                        m_Serie.Telefilm,
                                        m_Serie.Serie,
                                        m_Serie.Puntata).Replace(" ", ".");

                String url = match.Groups[1].Value.Replace("&amp;", "&");
                
                if (System.IO.File.Exists(archiveFile))
                {
                    System.IO.File.Delete(archiveFile);
                }
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(archiveFile)))
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(archiveFile));
				
				Console.WriteLine("Download file {0} on the directory {1}",url ,archiveFile);
                
                File.WriteAllBytes(archiveFile, client.DownloadData(new Uri(url)));

                // extract the file
                using (clUnzipper estrai = new clUnzipper())
                {
                    string[] lista = estrai.Estrai(archiveFile);

                    for (int x = 0; x < lista.Length; x++)
                    {
                        if (x == 0)
                        {

                            if (this.InfoSerie.File.Length > 0)
                            {
                                m_Prc_file_down = Path.GetFileNameWithoutExtension(this.InfoSerie.File) + ".srt";
                                File.Move(lista[x], Path.Combine(this.DirDownload, m_Prc_file_down));
                            }
                            else
                            {
                                m_Prc_file_down = lista[x];
                            }
                        }
                        else
                        {
                            File.Delete(lista[x]);
                        }
                    }
                    File.Delete(archiveFile);

                }

            }
        }

    }

}
