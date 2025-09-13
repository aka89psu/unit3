using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Data.Access.Library.Core;

namespace unit3.Server
{
    [DataTableAccessorAttribute("unit3.articles")]
    public class Article
    {
        private int id;
        private string abstractValue;
        private string source;
        private string image;
        private int imageHeight;
        private int imageWidth;
        private string imageCaption;

        [DataFieldAccessorAttribute("id")]
        public int Id { get => id; set => id = value; }

        [DataFieldAccessorAttribute("abstract")]
        public string AbstractValue { get => abstractValue; set => abstractValue = value; }

        public string Source { get => source; set => source = value; }

        public string Image { get => image; set => image = value; }

        public int ImageHeight { get => imageHeight; set => imageHeight = value; }

        public int ImageWidth { get => imageWidth; set => imageWidth = value; }

        public string ImageCaption { get => imageCaption; set => imageCaption = value; }

        public Article() : this(-1,string.Empty, string.Empty, string.Empty, -1, -1, string.Empty) { }

        public Article(string id, string abstractValue, string source, string image, string imageHeight, string imageWidth, string imageCaption) : this(int.Parse(id), abstractValue, source, image, int.Parse(imageHeight), int.Parse(imageWidth), imageCaption) { }

        public Article(int id, string abstractValue, string source, string image, int imageHeight, int imageWidth, string imageCaption)
        {
            this.id = id;
            this.abstractValue = abstractValue;
            this.source = source;
            this.image = image;
            this.imageHeight = imageHeight;
            this.imageWidth = imageWidth;
            this.imageCaption = imageCaption;
        }
    }
}
