using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unikktle.Models
{
    public class MindViewModel
    {
        public Mind Mind { get; set; }

        //public IEnumerable<MindRow> MindRowList { get; set; }

        public int SVG_Width { get; set; }

        public int SVG_Height { get; set; }

        public List<MindRow_Rect> RectList { get; set; }

        public List<MindRow_Line> LineList { get; set; }

        public List<MindRow_Text> TextList { get; set; }

        public List<MindRow_Link> LinkList { get; set; }
    }
}
