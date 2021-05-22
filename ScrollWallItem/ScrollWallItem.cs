using Geode.Extension;
using Geode.Network;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace GeodeExampleCSharp
{
    [Module("ScrollWallItem", "Arckmed", "Use your scroll wheel to move wall items.")]
    public class ScrollWallItem : GService
    {
        private const int INT64_NEXT = 0;
        public string itemCoords { get; private set; }
        public int itemId { get; private set; }
        public string rotation { get; set; }
        public int w1 { get; set; }
        public int w2 { get; set; }
        public int l1 { get; set; }
        public int l2 { get; set; }

        public MainWindow MainWindowParent;

        public ScrollWallItem(MainWindow MainWindowParent)
        {
            this.MainWindowParent = MainWindowParent;
            this.MainWindowParent.OnWallItemMove += On_WallItemMove;
        }

        [OutDataCapture("MoveWallItem")]
        void OnMoveWallItem(DataInterceptedEventArgs e)
        {
            if (MainWindowParent.isFlash)
            {
                itemId = e.Packet.ReadInt32();
                itemCoords = e.Packet.ReadUTF8();
                GetCoordsAndRotation(itemCoords);
            }
            else
                _ = e.Packet.ReadInt32();
            itemId = e.Packet.ReadInt32();
            // long ^
            w1 = e.Packet.ReadInt32();
            w2 = e.Packet.ReadInt32();
            l1 = e.Packet.ReadInt32();
            l2 = e.Packet.ReadInt32();
            rotation = e.Packet.ReadUTF8();
        }

        public void GetCoordsAndRotation(string coords)
        {
            //flash structure: {s:":w=5,0 l=11,42 r"}
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
                if (MainWindowParent.isFlash)
                    await SendToServerAsync(Out.MoveWallItem, itemId, Concat());
                else
                    await SendToServerAsync(UnityOut.MoveWallItem, INT64_NEXT, itemId, w1, w2, l1, l2, rotation);
        }

        public string Concat()
        {
            return string.Concat(new string[]
            {
                    "id:",
                    itemId.ToString(),
                    ":w=",
                    w1.ToString(),
                    ",",
                    w2.ToString(),
                    " l=",
                    l1.ToString(),
                    ",",
                    l2.ToString(),
                    "r",
                    rotation
            });
        }
    }
}
