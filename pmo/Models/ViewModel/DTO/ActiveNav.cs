namespace dto {
    using System.Collections.Generic;

    public class ActiveNav {
        public ActiveNav() {
            this.ChildNavs = new List<ActiveNav>();
        }

        public List<ActiveNav> ChildNavs { get; set; }
        public bool? IsComplete { get; set; }
        public string Url { get; set; }
        public string Component { get; set; }
        public bool Visible { get; set; }
    }
}
