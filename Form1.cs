using System.Net;

namespace Experience
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try {
                int total = 0;
                WebClient client = new WebClient();
                string reply = client.DownloadString("https://secure.runescape.com/m=hiscore_oldschool/hiscorepersonal?user1=choochish");
                String[] right = reply.Split(new String[] { "<td align=\"right\">" }, StringSplitOptions.None);
                int[] experience = new int[23];
                String level = right[5].Split('<')[0];
                level = level.Replace(",", "");
                int iLevel = Int32.Parse(level);
                for (int i = 0; i < right.Length; i++) {
                    right[i] = right[i].Split(new String[] { "</td" }, StringSplitOptions.None)[0];
                }
                for (int i = 0; i < 23; i++) {
                    experience[i] = Int32.Parse(right[10 + i * 4].Replace(",", ""));
                }
                for (int i = 0; i < experience.Length; i++) {
                    if (experience[i] < 13034431) {
                        total += (13034431 - experience[i]);
                    }
                }
                if (2277 - iLevel != 0) {
                    notifyIcon1.Text = (2277 - iLevel) + " LEVELS\n" + String.Format("{0:n0}", total) + " XP";
                } else {
                    notifyIcon1.Text = "MAXED";
                }
            } catch {
                notifyIcon1.Text = "ERROR";
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }
    }
}
