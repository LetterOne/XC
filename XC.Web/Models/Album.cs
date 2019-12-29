using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XC.Web.Models
{
    public class Album
    {
        public virtual int AlbumID { get; set; }
        public virtual int GenreID { get; set; }
        public virtual int ArtistID { get; set; }
        public virtual string Title { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string AlbumArtUri { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
    }
    public class Genre
    {
        public virtual int GenreID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual List<Album> Albums { get; set; }
    
    }

    public class Artist
    {
        public virtual int ArtistID { get; set; }
        public virtual string Name { get; set; }
    }
}
