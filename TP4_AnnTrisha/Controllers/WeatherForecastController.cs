using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System;
using System.IO;

namespace TP4_AnnTrisha.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {


        string[,] chansons = { { "Cradle", "Exits", "What You Know" }, { "Direction", "Moth", "Dirty" }, { "99 Glooms", "Catalysta", "Lost It To Trying" } };
        string[,] artistes = { { "Silversun Pickups", "Foals", "Two Door Cinema Club" }, { "Solence", "Normandie", "grandson" }, { "Sta", "Pkch", "Son Lux" } };

        string docPath =
  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        int i = 3;

        List<string> CustomNames = new List<string>();
        List<string> CustomArtists = new List<string>();
        List<string> CustomGenres = new List<string>();

        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }



        [HttpGet(Name = "AvoirChansonsAleotoires")]
        public List<Song> AvoirChansonsAleatoires(int NombreDeChansons)
        {
            var SongList = new List<Song>();

            for (int i = 0; i < NombreDeChansons; i++)
            {
                int g = Random.Shared.Next(0, 3);
                int n = Random.Shared.Next(0, 3);
                string style;



                switch (g)
                {
                    case 0:
                        style = "Chill"; break;
                    case 1:
                        style = "Heavy"; break;
                    case 2:
                        style = "Electronic"; break;
                    default:
                        style = string.Empty; break;
                }



                SongList.Add(new Song
                {
                    Artist = artistes[g, n],
                    Name = chansons[g, n],
                    Style = style,

                });
            }
            return SongList;
        }



        [HttpGet(Name = "AvoirChansonDuGenre")]
        public IEnumerable<Song> AvoirChansonDuGenre(string genre)
        {
            
            switch (genre.ToLower())
            {
                case "chill": 
                    i = 0; break;
                case "heavy":
                    i = 1; break;
                case "electronic":
                    i = 2; break;
                default:
                    i = 3; break;
            }

            int ii = Random.Shared.Next(0, 3);

            //dans le cas ou le genre entree est invalide
            if (i == 3)
            {
                return Enumerable.Range(1, 1).Select(index => new Song
                {
                    Artist = "invalide",
                    Name = "invalide",
                    Style = "Choisir parmis les genres suivants: chill, heavy, electronic"
                })
                .ToArray();
            }

            //retourner une chanson
            return Enumerable.Range(1, 1).Select(index => new Song
            {
                Artist = artistes[i, ii],
                Name = chansons[i, ii],
                Style = genre
            })
            .ToArray();

        }


        [HttpPost(Name = "AjouterChanson")]
        public Song AjouterChanson(string Titre, string Artiste, string Genre)
        {


            CustomNames.Add(Titre);
            CustomArtists.Add(Artiste);
            CustomGenres.Add(Genre);
            

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "CustomNames.txt"), true))
            {
                foreach (string line in CustomNames)
                    outputFile.WriteLine(line);
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "CustomArtists.txt"), true))
            {
                foreach (string line in CustomArtists)
                    outputFile.WriteLine(line);
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "CustomGenres.txt"), true))
            {
                foreach (string line in CustomGenres)
                    outputFile.WriteLine(line);
            }

            return new Song
            {
                Artist = Artiste,
                Name = Titre,
                Style= Genre
            };
        }








        [HttpGet(Name = "AvoirChansonsAjoutees")]
        public List<Song> AvoirChansonsAjoutees()
        {
            var SongList = new List<Song>();

                using (var sr = new StreamReader(Path.Combine(docPath, "CustomNames.txt")))
                {
                for (string fullLine = sr.ReadLine(); fullLine != null; fullLine = sr.ReadLine())
                    {
                        string[] wholeLine = fullLine.Split('\t');
                        CustomNames.Add(wholeLine[0]);
                    }
                }
                using (var sr = new StreamReader(Path.Combine(docPath, "CustomArtists.txt")))
                {
                    for (string fullLine = sr.ReadLine(); fullLine != null; fullLine = sr.ReadLine())
                    {
                        string[] wholeLine = fullLine.Split('\t');
                        CustomArtists.Add(wholeLine[0]);
                    }
                }
                using (var sr = new StreamReader(Path.Combine(docPath, "CustomGenres.txt")))
                {
                    for (string fullLine = sr.ReadLine(); fullLine != null; fullLine = sr.ReadLine())
                    {
                        string[] wholeLine = fullLine.Split('\t');
                        CustomGenres.Add(wholeLine[0]);
                    }
                }

            if (CustomNames.Count > 0)
            {
                for (int i = 0; i < CustomNames.Count; i++)
                {

                    SongList.Add(new Song
                    {
                        Artist = CustomArtists[i],
                        Name = CustomNames[i],
                        Style = CustomGenres[i],

                    });
                }
            }
            return SongList;
        }
    }
}