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
            m_RegExpEpisode = "<dd><img.*?href=\"([^\\\"]*?)\">([^<]*)</a>";
            m_RegExpDownload = ".*?href=\"(.*?fname=([^\\&]*)&amp.*?) rel=\"nofollow\">";

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
            while (m_InProgress)
            {
                System.Windows.Forms.Application.DoEvents();
            }
		}
		
		public void DownloadSubAsynch()
		{

            if (m_workback.IsBusy) return;

            m_InProgress = true;
            m_workback.RunWorkerAsync();

            //Thread workback = new Thread(DownloadSub);
            //workback.IsBackground = true;
            //workback.Priority = ThreadPriority.Lowest;
            //workback.Start();
			
		}
		
        public void DownloadSub()
        {
			m_Prc_file_down = string.Empty;
			
            string loginUri = m_sBaseUrl + "index.php";
            string reqString = "option=com_smf&action=login2&user=" + m_sUserName + "&passwrd=" + m_sPassword;

            WebClient client = new WebClient();

            //RemositorySubtitleEpisode episode = new RemositorySubtitleEpisode(seriesName,@"c:\casa.srt", 5, 10);
            CookieContainer cc = new CookieContainer();
            HttpWebRequest loginRequest = (HttpWebRequest)WebRequest.Create(loginUri + "?" + reqString);

            loginRequest.Proxy = null;
            loginRequest.CookieContainer = cc;
            loginRequest.Method = "GET";

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


            //STEP 1: check series name
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
                Console.WriteLine("Serire TV non trovata!");
				m_InProgress = false;
                return;
            } 
            
            Console.WriteLine("Episodio trovato!");

            data = client.OpenRead(sSeriesPageURL);
            reader = new StreamReader(data);
            sPage = reader.ReadToEnd().Replace('\0', ' ');

            sSeasonPageURL = matchSeason(sPage, client);

            if (sSeasonPageURL.Length == 0)
            {
                Console.WriteLine("Stagione non trovata!");
				m_InProgress = false;
                return;
            }
			
			Console.WriteLine("Serie trovata!");

            data = client.OpenRead(sSeasonPageURL);
            reader = new StreamReader(data);
            sPage = reader.ReadToEnd().Replace('\0', ' ');

            sEpisodePageURL = matchEpisode(sSeasonPageURL, null, sPage, client);

            if (sEpisodePageURL.Length == 0)
            {
                Console.WriteLine("Puntata non trovata!");
				m_InProgress = false;
                return;
            }

			Console.WriteLine("Puntata trovata!");
			
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
            //String seasonUrl = url;
            String retValue = String.Empty;
            //Find other file version
            //MatchCollection matches = Engine.Matches(sPage);

            //Find default file version
            MatchCollection fileMatches = EpisodeEngine.Matches(sPage);
            /*Feedback.CItem selectedVersion = null;

            if (matches.Count > 0 && sSelectedVersionTag == null)
            {
                List<Feedback.CItem> Choices = new List<Feedback.CItem>();

                Feedback.ChooseFromSelectionDescriptor versionSelector = new Feedback.ChooseFromSelectionDescriptor();
                versionSelector.m_sTitle = Translation.CFS_Select_Correct_Subtitle_Version;
                versionSelector.m_sItemToMatchLabel = Translation.CFS_Subtitle_Episode;
                versionSelector.m_sItemToMatch = episode.m_sSeriesName + " " + episode.m_nSeasonIndex + "x" + episode.m_nEpisodeIndex;
                versionSelector.m_sListLabel = Translation.CFS_Select_Version;
                versionSelector.m_List = Choices;
                versionSelector.m_sbtnIgnoreLabel = String.Empty;

                //add default version if available
                if (fileMatches.Count > 0)
                {
                    Choices.Add(new Feedback.CItem("Default Version", String.Empty, seasonUrl));
                }

                //add other versions
                foreach (Match match in matches)
                {
                    Choices.Add(new Feedback.CItem(match.Groups[2].Value.Trim(), String.Empty, match.Groups[1].Value));
                }


                if (m_feedback.ChooseFromSelection(versionSelector, out selectedVersion) == Feedback.ReturnCode.OK)
                {
                    sSelectedVersionTag = selectedVersion.m_Tag as String;
                }
            }
            else
            {
                if (sSelectedVersionTag == null) sSelectedVersionTag = seasonUrl;
            }

            MPTVSeriesLog.Write("Episode Version selected: (Episode = " + sSelectedVersionTag + ")");
            if (sSelectedVersionTag != url && userSelection != null)
            {
                //load custom file version page
                Stream data = client.OpenRead(selectedVersion.m_Tag as String);
                StreamReader reader = new StreamReader(data);
                sPage = reader.ReadToEnd().Replace('\0', ' ');
                fileMatches = EpisodeEngine.Matches(sPage);
            }*/

            foreach (Match match in fileMatches)
            {
                String ep = m_Serie.Telefilm + " " + m_Serie.Serie.ToString() + "x" + String.Format("{0:00}", m_Serie.Puntata);
                if (match.Groups[2].Value.Trim().ToLower() == ep.Trim().ToLower())
                {
                    retValue = match.Groups[1].Value;
                    break;
                }
            }
            /*
            if (retValue == String.Empty && fileMatches.Count > 0)
            {
                List<Feedback.CItem> Choices = new List<Feedback.CItem>();
                foreach (Match match in fileMatches)
                {
                    Choices.Add(new Feedback.CItem(match.Groups[2].Value.Trim(), String.Empty, match.Groups[1].Value));
                }

                Feedback.ChooseFromSelectionDescriptor descriptor = new Feedback.ChooseFromSelectionDescriptor();
                descriptor.m_sTitle = Translation.CFS_Choose_Correct_Episode;
                descriptor.m_sItemToMatchLabel = Translation.CFS_Local_Episode_Index;
                descriptor.m_sItemToMatch = episode.m_sSeriesName + " " + episode.m_nSeasonIndex + "x" + episode.m_nEpisodeIndex;
                descriptor.m_sListLabel = Translation.CFS_Available_Episode_List;
                descriptor.m_List = Choices;
                descriptor.m_sbtnIgnoreLabel = String.Empty;

                Feedback.CItem Selected = null;
                if (m_feedback.ChooseFromSelection(descriptor, out Selected) == Feedback.ReturnCode.OK)
                {
                    retValue = Selected.m_Tag as String;
                }
            }
            if (retValue != String.Empty && userSelection != null)
            {
                userSelection[DBUserSelection.cUserKey] = seasonUrl;
                userSelection[DBUserSelection.cContextType] = "Remository";
                userSelection[DBUserSelection.cTags] = sSelectedVersionTag;
                userSelection.Enabled = true;
                userSelection.Commit();
            }*/
            return retValue;
        }

        private void episodeSubtitleDownload(WebClient client, String sPage)
        {
            MatchCollection matches = DownloadEngine.Matches(sPage);

            foreach (Match match in matches)
            {
                String dir = m_Dir_Download; //Path.GetDirectoryName(episode.m_sFileName);
                //String movieFileName = Path.GetFileName(episode.m_sFileName);
                String archiveFile = dir + Path.DirectorySeparatorChar + match.Groups[2].Value;

                String url = match.Groups[1].Value.Replace("&amp;", "&");
                //MPTVSeriesLog.Write("Download file : " + match.Groups[2]);
                if (System.IO.File.Exists(archiveFile))
                {
                    //MPTVSeriesLog.Write("File " + archiveFile + " found: deleting");
                    System.IO.File.Delete(archiveFile);
                }
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(archiveFile)))
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(archiveFile));
				
				Console.WriteLine("Scarico del file {0} nella cartella {1}",url ,archiveFile);
                client.DownloadFile(url, archiveFile);
				
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
				
                /*
                List<Feedback.CItem> Choices = episode.extract(match.Groups[2].Value);
                String selectedFile = String.Empty;
                if (Choices.Count == 1)
                {
                    selectedFile = Choices[0].m_Tag as String;
                }
                else
                {
                    if (Choices.Count > 0)
                    {
                        Feedback.ChooseFromSelectionDescriptor descriptor = new Feedback.ChooseFromSelectionDescriptor();
                        descriptor.m_sTitle = Translation.CFS_Select_Matching_Subitle_File;
                        descriptor.m_sItemToMatchLabel = Translation.CFS_Subtitle_Episode;
                        descriptor.m_sItemToMatch = episode.m_sSeriesName + " " + episode.m_nSeasonIndex + "x" + episode.m_nEpisodeIndex;
                        descriptor.m_sListLabel = Translation.CFS_Matching_Subtitles;
                        descriptor.m_List = Choices;
                        descriptor.m_sbtnIgnoreLabel = String.Empty;

                        Feedback.CItem Selected = null;
                        if (m_feedback.ChooseFromSelection(descriptor, out Selected) == Feedback.ReturnCode.OK)
                        {
                            selectedFile = Selected.m_Tag as String;
                        }
                    }
                    else
                    {
                        MPTVSeriesLog.Write("No files found!");
                    }
                }

                foreach (Feedback.CItem choice in Choices)
                {
                    if (choice.m_Tag as String == selectedFile)
                    {
                        String targetFile = dir + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(movieFileName) + Path.GetExtension(selectedFile);
                        if (System.IO.File.Exists(targetFile))
                        {
                            Feedback.ChooseFromYesNoDescriptor descriptor = new Feedback.ChooseFromYesNoDescriptor();
                            descriptor.m_sTitle = Translation.CYN_Subtitle_File_Replace;
                            descriptor.m_sLabel = Translation.CYN_Old_Subtitle_Replace;
                            descriptor.m_dialogButtons = Feedback.DialogButtons.YesNo;
                            descriptor.m_dialogDefaultButton = Feedback.ReturnCode.Yes;
                            if (m_feedback.YesNoOkDialog(descriptor) == Feedback.ReturnCode.Yes)
                            {
                                MPTVSeriesLog.Write("File " + targetFile + " found: deleted and replaced ");
                                System.IO.File.Delete(targetFile);
                                System.IO.File.Move(selectedFile, targetFile);
                            }
                            else
                            {
                                MPTVSeriesLog.Write("File " + targetFile + " found: NOT deleted");
                                System.IO.File.Delete(selectedFile);
                            }
                        }
                        else
                        {
                            System.IO.File.Move(selectedFile, targetFile);
                        }

                        MPTVSeriesLog.Write("Selected : " + Path.GetFileName(choice.m_Tag as String));
                        m_bSubtitleRetrieved = true;
                    }
                    else
                    {
                        System.IO.File.Delete(choice.m_Tag as String);
                    }
                    //check if dir is empty
                    if (isDirectoryEmpty(Path.GetDirectoryName(choice.m_Tag as String)))
                    {
                        System.IO.Directory.Delete(Path.GetDirectoryName(choice.m_Tag as String));
                    }
                }
                System.IO.File.Delete(archiveFile);
                */

            }
        }

    }

    

    /*
    public class RemositorySubtitleEpisode
    {
        public String m_sSeriesName = String.Empty;
        public String m_sFileName = String.Empty;
        public int m_nSeasonIndex = 0;
        public int m_nEpisodeIndex = 0;

        public RemositorySubtitleEpisode(String sSeriesName, String sFileName, int nSeasonIndex, int nEpisodeIndex)
        {
            m_sSeriesName = sSeriesName.ToLower();
            m_sFileName = sFileName;
            m_nSeasonIndex = nSeasonIndex;
            m_nEpisodeIndex = nEpisodeIndex;
        }
        public List<Feedback.CItem> extract(String subtitleFile)
        {
            String dir = Path.GetDirectoryName(m_sFileName);
            String subtitleFileName = Path.GetFileName(subtitleFile);
            String subtitleFileExtension = Path.GetExtension(subtitleFile);
            String archiveFile = dir + Path.DirectorySeparatorChar + subtitleFile;

            //load files in archive
            List<Feedback.CItem> Choices = new List<Feedback.CItem>();

            // RAR HAndling
            if (subtitleFileExtension == ".rar")
            {
                Unrar unrar = new Unrar();
                unrar.ArchiveName = archiveFile;
                unrar.ExtractAll(dir);
                List<String> fileList = unrar.FileNameList;
                foreach (String file in fileList)
                {
                    Choices.Add(new Feedback.CItem(file, String.Empty, dir + Path.DirectorySeparatorChar + file));
                }
                unrar = null;
            }
            // ZIP HAndling - Start
            else if (subtitleFileExtension == ".zip")
            {

                using (ZipInputStream s = new ZipInputStream(File.OpenRead(archiveFile)))
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (theEntry.IsFile)
                        {
                            using (FileStream streamWriter = File.Create(dir + Path.DirectorySeparatorChar + theEntry.Name))
                            {
                                String filename = Path.GetFileName(theEntry.Name);
                                if (filename.Length > 0)
                                {
                                    int size = 2048;
                                    byte[] fileData = new byte[2048];
                                    while (true)
                                    {
                                        size = s.Read(fileData, 0, fileData.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(fileData, 0, size);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    Choices.Add(new Feedback.CItem(filename, String.Empty, dir + Path.DirectorySeparatorChar + theEntry.Name));
                                }
                            }
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(dir + Path.DirectorySeparatorChar + Path.GetDirectoryName(theEntry.Name));
                        }
                    }
                }
            } // ZIP HAndling   - END
            else
                throw new Exception("Extension not supported " + subtitleFile);
            return Choices;
        }
    }
    
    public class RemositorySeriesMatchResult : IComparable<RemositorySeriesMatchResult>
    {
        public String sSubFullName = String.Empty;
        public String sPageURL = String.Empty;

        // for sorting
        public int nDistance = 0xFFFF;

        public int CompareTo(RemositorySeriesMatchResult other)
        {
            return nDistance.CompareTo(other.nDistance);
        }

        public RemositorySeriesMatchResult(String sName, String sURL)
        {
            sSubFullName = sName.ToLower();
            sPageURL = sURL;
        }

        public void ComputeDistance(RemositorySubtitleEpisode episode)
        {
            //nDistance = MediaPortal.Util.Levenshtein.Match(sSubFullName, episode.m_sSeriesName.ToLower());
        }
    };
    */
}
