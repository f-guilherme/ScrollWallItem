using Geode.Extension;
using Geode.Network;
using System;

namespace GeodeExampleCSharp
{
    [Module("ScrollWallItem", "Arckmed", "Use your scroll wheel to move wall items.")]
    public class ScrollWallItem : GService
    {
        public MainWindow MainWindowParent;
        public string itemCoords { get; private set; }
        public string rotation { get; set; }
        public int itemId { get; private set; }
        public int w1 { get; set; }
        public int w2 { get; set; }
        public int l1 { get; set; }
        public int l2 { get; set; }

        public ScrollWallItem(MainWindow MainWindowParent)
        {
            this.MainWindowParent = MainWindowParent;
            this.MainWindowParent.OnWallItemMove += On_WallItemMove;
        }

        [OutDataCapture("MoveWallItem")]
        void OnMoveWallItem(DataInterceptedEventArgs e)
        {
            itemId = e.Packet.ReadInt32();
            itemCoords = e.Packet.ReadUTF8();
            GetCoordsAndRotation(itemCoords);
        }

        public void GetCoordsAndRotation(string coords)
        {
            //structure: {s:":w=5,0 l=11,42 r"}
            coords = coords.Substring(3, coords.Length - 3);
            w1 = int.Parse(coords.Substring(0, coords.IndexOf(' ')).Split(new char[]
            {
                ','
            })[0]);
            w2 = int.Parse(coords.Substring(0, coords.IndexOf(' ')).Split(new char[]
            {
                ','
            })[1]);
            coords = coords.Substring(this.w1.ToString().Length + this.w2.ToString().Length + 4);
            l1 = int.Parse(coords.Substring(0, coords.IndexOf(' ')).Split(new char[]
            {
                ','
            })[0]);
            l2 = int.Parse(coords.Substring(0, coords.IndexOf(' ')).Split(new char[]
            {
                ','
            })[1]);
            rotation = coords.Substring(coords.Length - 1);
        }

        public async void On_WallItemMove(object sender, EventArgs e)
        {
            await SendToServerAsync(Out.MoveWallItem, itemId, Concat());
        }

        public string Concat()
        {
            return string.Concat(new string[]
            {
                    ":w=",
                    w1.ToString(),
                    ",",
                    w2.ToString(),
                    " l=",
                    l1.ToString(),
                    ",",
                    l2.ToString(),
                    " ",
                    rotation
            });
        }
    }
}
