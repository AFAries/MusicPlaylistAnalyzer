using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace MusicPlaylistAnalyzer
{

    public class Music
    {
        //Name, Artist, Album, Genre, Size, Time, Year, and Plays.
        public string Name;
        public string Artist;
        public string Album;
        public string Genre;
        public int Size;
        public int Time;
        public int Year;
        public int Plays;

        public Music(string name, string artist, string album, string genre,
            int size, int time, int year, int plays)
        {
            Name = name;
            Artist = artist;
            Album = album;
            Genre = genre;
            Size = size;
            Time = time;
            Year = year;
            Plays = plays;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                //Name, Artist, Album, Genre, Size, Time, Year, and Plays.
                string name;
                string artist;
                string album;
                string genre;
                int size;
                int time;
                int year;
                int plays;

                string MusicPlaylistFilePath = args[0];
                string ReportFilePath = args[1];

                List<Music> musicList = new List<Music>();
                List<string> Report = new List<string>();

                try
                {
                    using (StreamReader reader = new StreamReader(MusicPlaylistFilePath))
                    {
                        int FileLines = 0;

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split('\t');

                            FileLines++;

                            if (FileLines == 1) continue;

                            if (values.Length != 8)
                            {
                                Console.WriteLine("There should be 8 columns, aka values");
                                Environment.Exit(0);
                            }
                            //Name, Artist, Album, Genre, Size, Time, Year, and Plays.
                            try
                            {
                                name = values[0];
                                artist = values[1];
                                album = values[2];
                                genre = values[3];
                                size = Int32.Parse(values[4]);
                                time = Int32.Parse(values[5]);
                                year = Int32.Parse(values[6]);
                                plays = Int32.Parse(values[7]);

                                Music music = new Music(name, artist, album, genre, size, time, year, plays);
                                musicList.Add(music);
                            }

                            catch (Exception e)
                            {
                                Console.WriteLine("Exception: {0}", e);
                            }

                        }

                        //List of the questions to be answered
                        //How many songs received 200 or more plays?
                        //How many songs are in the playlist with the Genre of “Alternative”?
                        //How many songs are in the playlist with the Genre of “Hip - Hop / Rap”?
                        //What songs are in the playlist from the album “Welcome to the Fishbowl?”
                        //What are the songs in the playlist from before 1970 ?
                        //What are the song names that are more than 85 characters long?
                        //What is the longest song ? (longest in Time)

                        int songCount = 0;
                        int HsongCount = 0;
                        string musicReport = "Music Playlist Report:\n\n";

                        if (musicList.Count() < 1)
                        {
                            musicReport += "No data is available.\n";

                        }

                        //Left the .Union(*LINQ Magic*) code commented in as it was a way to get multiple things from a LINQ query
                        //But figured I would condense the code to what was specifically asked vs shown

                        musicReport += "Songs that received 200 or more plays: ";
                        var twoHundred = (from music in musicList where music.Plays > 200 select music.Name);
                        //.Union(from music in musicList where music.Plays > 200 select music.Artist);
                        if ( twoHundred.Count() > 0)
                        {
                            foreach(var names in twoHundred)
                            {
                                musicReport += "\n";
                                musicReport += names;
                            }
                        }

                        musicReport += "\n";
                        var alternativeGenre = (from music in musicList where music.Genre == "Alternative" select music.Name);
                        if (alternativeGenre.Count() > 0)
                        {
                            foreach (var names in alternativeGenre)
                            {
                                songCount +=1;
                            }
                            musicReport += "\n";
                            musicReport += $"Number of Alternative songs: {songCount}\n";
                        }

                        musicReport += "\n";
                        var HipHopRap = (from music in musicList where music.Genre == "Hip-Hop/Rap" select music.Name);
                        if (HipHopRap.Count() > 0)
                        {
                             foreach (var names in HipHopRap)
                             {
                                HsongCount += 1;
                             }
                            musicReport += $"Number of Hip Hop/Rap songs: {HsongCount}\n";
                        }

                         musicReport += "\n";
                         musicReport += "Songs from the album Welcome to the Fishbowl: ";
                        var WelcomeToTheFishbowl = (from music in musicList where music.Album == "Welcome to the Fishbowl" select music.Name);
                           //.Union(from music in musicList where music.Album == "Welcome to the Fishbowl" select music.Artist);
                         if (WelcomeToTheFishbowl.Count() > 0)
                         {
                             foreach (var names in WelcomeToTheFishbowl)
                             {
                                musicReport += "\n";
                                musicReport += names; 
                             }
                         }

                         musicReport += "\n";
                         musicReport += "\n";
                         musicReport += "Songs from before 1970: ";
                        var nineteenSeventy = (from music in musicList where music.Year < 1970 select music.Name);
                           //.Union(from music in musicList where music.Year < 1970 select music.Artist);
                         if (nineteenSeventy.Count() > 0)
                         {
                             foreach (var names in nineteenSeventy)
                             {
                                musicReport += "\n";
                                musicReport += names; 
                             }
                         }

                        musicReport += "\n";
                        musicReport += "\n";
                        musicReport += "Song names longer than 85 characters: ";
                        var EightFiveChar = (from music in musicList where music.Name.Length > 85 select music.Name);
                            //.Union(from music in musicList where music.Name.Length > 85 select music.Artist);
                         if (EightFiveChar.Count() > 0)
                         {
                             foreach (var names in EightFiveChar)
                             {
                                musicReport += "\n";
                                musicReport += names; 
                             }
                         }

                        int LongSongCalculator = (from music in musicList select music.Size).Max();

                        musicReport += "\n";
                        musicReport += "\n";
                        musicReport += "Longest song: ";
                        var longestSong = (from music in musicList where music.Size == LongSongCalculator select music.Name);
                            //.Union(from music in musicList where music.Size == LongSongCalculator select music.Artist);

                        if (longestSong.Count() > 0)
                        {
                            foreach (var names in longestSong)
                            {
                                musicReport += "\n";
                                musicReport += names; 
                            }
                           
                        }
                        musicReport += "\n";
                        musicReport += $"Longest Songs Length: {LongSongCalculator}";

                        Console.WriteLine(musicReport);
                        Console.WriteLine("");
                        File.WriteAllText(ReportFilePath, musicReport);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: {0}", e);
                }

            }

            //SampleMusicPlaylist.txt test1.txt
            //Kept this here to copy and paste into running custom config...

            else if (args.Length != 2)
            {
                Console.WriteLine("Not enough or too many arguments provided");
                Console.WriteLine("MusicPlaylistAnalyzer <music_playlist_file_path> <report_file_path>");
                Environment.Exit(0);
            }
        }
    }
}
